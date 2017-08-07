using System;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegStreamOptions : BufferObject
    {
        private byte _StreamIndex = byte.MaxValue;

        [DefaultValue(FfmpegStreamType.All)]
        public FfmpegStreamType StreamType { get; set; }

        [DefaultValue(null)]
        public byte? StreamIndex
        {
            get => _StreamIndex == byte.MaxValue ? (byte?)null : _StreamIndex;
            set
            {
                if (value == null)
                {
                    _StreamIndex = byte.MaxValue;
                }
                else if (value == byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _StreamIndex = value.Value;
                }
            }
        }

        protected string StreamSpecifier
        {
            get
            {
                if (StreamType == FfmpegStreamType.All)
                {
                    if (_StreamIndex == byte.MaxValue)
                    {
                        return null;
                    }
                    else
                    {
                        return _StreamIndex.ToString();
                    }
                }
                else
                {
                    char ts;
                    switch (StreamType)
                    {
                        case FfmpegStreamType.Video:
                            ts = 'V';
                            break;

                        case FfmpegStreamType.AllVideo:
                            ts = 'v';
                            break;

                        case FfmpegStreamType.Audio:
                            ts = 'a';
                            break;

                        case FfmpegStreamType.Subtitle:
                            ts = 's';
                            break;

                        case FfmpegStreamType.Data:
                            ts = 'd';
                            break;

                        case FfmpegStreamType.Attachments:
                        default:
                            ts = 't';
                            break;
                    }

                    if (_StreamIndex == byte.MaxValue)
                    {
                        return ts.ToString();
                    }
                    else
                    {
                        return ts + ":" + _StreamIndex;
                    }
                }
            }
        }

        public string Codec
        {
            get => GetString();
            set => SetValue(value);
        }

        internal virtual void AppendArgs(StringBuilder builder)
        {
            var ss = StreamSpecifier;
            builder.AppendIfStream("-c", ss, Codec);
        }
    }
}