using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shipwreck.FfmpegUtil
{
    public static class FfprobeUtility
    {
        #region Setters

        #region FormatSetters

        private static Dictionary<string, Action<FfprobeFormatInfo, string>> _FormatSetters;

        private static Dictionary<string, Action<FfprobeFormatInfo, string>> FormatSetters
            => _FormatSetters ?? (_FormatSetters = new Dictionary<string, Action<FfprobeFormatInfo, string>>()
            {
                ["format"] = (s, v) => s.Format = v,
                ["size"] = (s, v) => s.FileSize = GetInt64(v),
            });

        #endregion FormatSetters

        #region StreamSetters

        private static Dictionary<string, Action<FfprobeStreamInfo, string>> _StreamSetters;

        private static Dictionary<string, Action<FfprobeStreamInfo, string>> StreamSetters
            => _StreamSetters ?? (_StreamSetters = new Dictionary<string, Action<FfprobeStreamInfo, string>>()
            {
                ["index"] = (s, v) => s.StreamIndex = GetNullableByte(v),
                ["codec_type"] = (s, v) => s.StreamType = Enum.TryParse(v, true, out FfmpegStreamType t)
                                    ? (t == FfmpegStreamType.Video ? FfmpegStreamType.AllVideo : t)
                                    : FfmpegStreamType.All,
                ["codec_name"] = (s, v) => s.Codec = v,
                ["codec_long_name"] = (s, v) => s.CodecLongName = v,
                ["width"] = (s, v) => s.FrameWidth = GetInt16(v),
                ["height"] = (s, v) => s.FrameHeight = GetInt16(v),
                ["avg_frame_rate"] = (s, v) => s.FrameRate = (float)GetFraction(v),
                ["duration"] = (s, v) => s.Duration = TimeSpan.FromSeconds(GetDouble(v)),
                ["bit_rate"] = (s, v) => s.BitRate = GetInt32(v),
                ["DISPOSITION:default"] = (s, v) => SetStreamDisposition(s, v, "default"),
                ["DISPOSITION:dub"] = (s, v) => SetStreamDisposition(s, v, "dub"),
                ["DISPOSITION:original"] = (s, v) => SetStreamDisposition(s, v, "original"),
                ["DISPOSITION:comment"] = (s, v) => SetStreamDisposition(s, v, "comment"),
                ["DISPOSITION:lyrics"] = (s, v) => SetStreamDisposition(s, v, "lyrics"),
                ["DISPOSITION:karaoke"] = (s, v) => SetStreamDisposition(s, v, "karaoke"),
                ["DISPOSITION:forced"] = (s, v) => SetStreamDisposition(s, v, "forced"),
                ["DISPOSITION:hearing_impaired"] = (s, v) => SetStreamDisposition(s, v, "hearing_impaired"),
                ["DISPOSITION:visual_impaired"] = (s, v) => SetStreamDisposition(s, v, "visual_impaired"),
                ["DISPOSITION:clean_effects"] = (s, v) => SetStreamDisposition(s, v, "clean_effects"),
                ["DISPOSITION:captions"] = (s, v) => SetStreamDisposition(s, v, "captions"),
                ["DISPOSITION:descriptions"] = (s, v) => SetStreamDisposition(s, v, "descriptions"),
                ["DISPOSITION:metadata"] = (s, v) => SetStreamDisposition(s, v, "metadata"),
                ["DISPOSITION:attached_pic"] = (s, v) => SetStreamDisposition(s, v, "attached_pic"),
                ["DISPOSITION:timed_thumbnails"] = (s, v) => SetStreamDisposition(s, v, "timed_thumbnails")
            });

        #endregion StreamSetters

        private static byte? GetNullableByte(string v)
            => byte.TryParse(v, out byte i) ? i : (byte?)null;

        private static short GetInt16(string v)
            => short.TryParse(v, out short i) ? i : (short)0;

        private static int GetInt32(string v)
            => int.TryParse(v, out int i) ? i : 0;

        private static long GetInt64(string v)
            => long.TryParse(v, out long i) ? i : 0;

        private static float GetSingle(string v)
            => float.TryParse(v, out float f) ? f : 0;

        private static double GetDouble(string v)
            => double.TryParse(v, out double f) ? f : 0;

        private static double GetFraction(string v)
        {
            var si = v.IndexOf('/');
            if (si >= 0)
            {
                return double.TryParse(v.Substring(0, si), out double en)
                        && double.TryParse(v.Substring(si + 1), out double de)
                        && de != 0 ? en / de : 0;
            }
            else
            {
                return double.TryParse(v, out double f) ? f : 0;
            }
        }

        private static void SetStreamDisposition(FfprobeStreamInfo s, string value, string key)
        {
            if (byte.TryParse(value, out byte b) && b != 0)
            {
                s.Disposition = key;
            }
        }

        #endregion Setters

        public static async Task<FfprobeFormatInfo> GetFormatInfoAsync(string fileName, string executable)
        {
            var psi = new ProcessStartInfo(executable ?? "ffprobe");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = false;
            psi.Arguments = new FfprobeArgs()
            {
                HideBanner = true,
                ShowFormat = true,
                ShowStreams = true,
                FilePath = Path.GetFullPath(fileName),
            }.ToString();

            var po = ProcessWatcher.Start(psi);

            await po.Task.ConfigureAwait(false);

            if (po.Process.ExitCode != 0 || !po.StandardOutput.Any())
            {
                throw new Exception();
            }

            return ParseFormatInfo(po.StandardOutput);
        }

        public static FfprobeFormatInfo ParseFormatInfo(IEnumerable<string> standardOutput)
        {
            var format = new FfprobeFormatInfo();

            FfprobeStreamInfo stream = null;
            var isRoot = true;

            foreach (var l in standardOutput)
            {
                if (Regex.IsMatch(l, @"^\[/?(STREAM|FORMAT)\]$"))
                {
                    if (l[1] == '/')
                    {
                        if (stream?.StreamIndex != null)
                        {
                            format.Streams.Add(stream);
                        }
                        stream = null;
                        isRoot = true;
                    }
                    else if (l[1] == 'S')
                    {
                        stream = new FfprobeStreamInfo()
                        {
                            StreamIndex = byte.MaxValue
                        };
                        isRoot = false;
                    }
                    else
                    {
                        isRoot = false;
                    }
                    continue;
                }

                if (stream != null)
                {
                    var i = l.IndexOf('=');
                    if (i > 0)
                    {
                        if (StreamSetters.TryGetValue(l.Substring(0, i), out Action<FfprobeStreamInfo, string> a))
                        {
                            a(stream, l.Substring(i + 1));
                        }
                    }
                }
                else if (!isRoot)
                {
                    var i = l.IndexOf('=');
                    if (i > 0)
                    {
                        if (FormatSetters.TryGetValue(l.Substring(0, i), out Action<FfprobeFormatInfo, string> a))
                        {
                            a(format, l.Substring(i + 1));
                        }
                    }
                }
            }

            return format;
        }
    }
}