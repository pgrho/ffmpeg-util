using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegFileOptions : BufferObject
    {
        internal FfmpegFileOptions()
        {
        }

        public string FilePath
        {
            get => GetString();
            set => SetValue(value);
        }

        public TimeSpan Duration
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        public TimeSpan SeekTo
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        public TimeSpan SeekToLast
        {
            get => GetTimeSpan();
            set => SetValue(value);
        }

        #region ストリーム別設定

        #region Streams

        private Collection<FfmpegStreamOptions> _Streams;

        public Collection<FfmpegStreamOptions> Streams
            => _Streams ?? (_Streams = new Collection<FfmpegStreamOptions>());

        public bool ShouldSerializeStreams()
             => _Streams?.Count > 0;

        public void ResetStreams()
             => _Streams?.Clear();

        #endregion Streams

        private T GetStreamOptions<T>(FfmpegStreamType type)
            where T : FfmpegStreamOptions, new()
        {
            var r = _Streams?.OfType<T>().FirstOrDefault(s => s.StreamType == type && s.StreamIndex == null);
            if (r == null)
            {
                r = new T()
                {
                    StreamType = type
                };

                Streams.Add(r);
            }

            return r;
        }

        private void SetStreamOptions<T>(T value, FfmpegStreamType type)
            where T : FfmpegStreamOptions, new()
        {
            var current = _Streams?.OfType<T>().Where(s => s.StreamType == type && s.StreamIndex == null).ToArray();

            if (current != null)
            {
                if (Array.IndexOf(current, value) > 0)
                {
                    return;
                }

                foreach (var e in current)
                {
                    _Streams.Remove(e);
                }
            }

            if (value != null)
            {
                Streams.Add(value);
            }
        }

        public FfmpegVideoStreamOptions AllStream
        {
            get => GetStreamOptions<FfmpegVideoStreamOptions>(FfmpegStreamType.All);
            set => SetStreamOptions(value, FfmpegStreamType.All);
        }

        public FfmpegVideoStreamOptions VideoStream
        {
            get => GetStreamOptions<FfmpegVideoStreamOptions>(FfmpegStreamType.Video);
            set => SetStreamOptions(value, FfmpegStreamType.Video);
        }

        public FfmpegAudiotreamOptions AudioStream
        {
            get => GetStreamOptions<FfmpegAudiotreamOptions>(FfmpegStreamType.Audio);
            set => SetStreamOptions(value, FfmpegStreamType.Audio);
        }

        #endregion ストリーム別設定

        internal abstract void AppendArgs(StringBuilder builder);

        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendArgs(sb);

            sb.TrimEnd();
            return sb.ToString();
        }
    }
}