using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegStreamOptions : BufferObject
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
            var ss = StreamSpecifier.ToString();
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