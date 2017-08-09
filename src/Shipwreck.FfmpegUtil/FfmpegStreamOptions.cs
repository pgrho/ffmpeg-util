using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegStreamOptions : BufferObject
    {
        #region Stream specifier

        [IgnoreBuffer]
        public FfmpegStreamType StreamType
        {
            get => StreamSpecifier.Type;
            set
            {
                var ss = StreamSpecifier;
                StreamSpecifier = new StreamSpecifier(value, ss.Index);
            }
        }

        [IgnoreBuffer]
        public byte? StreamIndex
        {
            get => StreamSpecifier.Index;
            set
            {
                var ss = StreamSpecifier;
                StreamSpecifier = new StreamSpecifier(ss.Type, value);
            }
        }

        public StreamSpecifier StreamSpecifier
        {
            get => GetStreamSpecifier();
            set => SetValue(value);
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

        [DefaultValue(0f)]
        public float FrameRate
        {
            get => GetSingle();
            set => SetValue(value);
        }

        [DefaultValue((short)0)]
        public short FrameWidth
        {
            get => GetInt16();
            set => SetValue(value);
        }

        [DefaultValue((short)0)]
        public short FrameHeight
        {
            get => GetInt16();
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

        [DefaultValue(0)]
        public int BitRate
        {
            get => GetInt32();
            set => SetValue(value);
        }

        [DefaultValue(null)]
        public string Filter
        {
            get => GetString();
            set => SetValue(value);
        }

        internal virtual void AppendArgs(StringBuilder builder)
        {
            var ss = StreamSpecifier.ToString();
            builder.AppendIfStream("-c", ss, Codec);
            builder.AppendIfStream("-frames", ss, FrameCount);
            builder.AppendIfStream("-r", ss, FrameRate);

            var w = FrameWidth;
            var h = FrameHeight;
            if (w > 0 && h > 0)
            {
                builder.AppendIfStream("-s", ss, w + "x" + h);
            }

            builder.AppendIfStream("-b", ss, BitRate);
            builder.AppendIfStream("-q", ss, QualityScale);
            builder.AppendIfStream("-filter", ss, Filter);
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