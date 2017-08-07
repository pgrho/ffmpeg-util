using System;
using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegStreamOptions : BufferObject
    {
        #region Stream specifier

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
                    var ts = StreamType.ToArg();

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

        #endregion Stream specifier

        [DefaultValue(null)]
        public string Codec
        {
            get => GetString();
            set => SetValue(value);
        }

        /// <summary>
        /// Gets or sets the disposition for a stream.
        /// </summary>
        [DefaultValue(null)]
        public string Disposition
        {
            get => GetString();
            set => SetValue(value);
        }

        [DefaultValue(0)]
        public int FrameCount
        {
            get => GetInt32();
            set => SetValue(value);
        }

        [DefaultValue(0)]
        public byte QualityScale
        {
            get => GetByte();
            set => SetValue(value);
        }

        internal virtual void AppendArgs(StringBuilder builder)
        {
            var ss = StreamSpecifier;
            builder.AppendIfStream("-c", ss, Codec);
            builder.AppendIfStream("-frames", ss, FrameCount);
            builder.AppendIfStream("-q", ss, QualityScale);

            builder.AppendIfStream("-disposition", ss, Disposition);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendArgs(sb);

            sb.TrimEnd();
            return sb.ToString();
        }
    }
}