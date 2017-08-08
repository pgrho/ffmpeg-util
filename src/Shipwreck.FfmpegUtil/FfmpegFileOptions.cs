using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public abstract class FfmpegFileOptions : BufferObject
    {
        internal FfmpegFileOptions()
        {
        }

        public override bool IsEmpty
            => base.IsEmpty && !ShouldSerializeStreams();

        /// <summary>
        /// Gets or sets a file path.
        /// </summary>
        [DefaultValue(null)]
        public string FilePath
        {
            get => GetString();
            set => SetValue(value);
        }

        [DefaultValue(null)]
        public string Format
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
             => _Streams?.Any(s => !s.IsEmpty) == true;

        public void ResetStreams()
             => _Streams?.Clear();

        #endregion Streams

        private FfmpegStreamOptions GetStreamOptions(FfmpegStreamType type)
        {
            var r = _Streams?.FirstOrDefault(s => s.StreamType == type && s.StreamIndex == null);
            if (r == null)
            {
                r = new FfmpegStreamOptions()
                {
                    StreamType = type
                };

                Streams.Add(r);
            }

            return r;
        }

        private void SetStreamOptions(FfmpegStreamOptions value, FfmpegStreamType type)
        {
            var current = _Streams?.Where(s => s.StreamType == type && s.StreamIndex == null).ToArray();

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

        public FfmpegStreamOptions AllStream
        {
            get => GetStreamOptions(FfmpegStreamType.All);
            set => SetStreamOptions(value, FfmpegStreamType.All);
        }

        public FfmpegStreamOptions VideoStream
        {
            get => GetStreamOptions(FfmpegStreamType.Video);
            set => SetStreamOptions(value, FfmpegStreamType.Video);
        }

        public FfmpegStreamOptions AudioStream
        {
            get => GetStreamOptions(FfmpegStreamType.Audio);
            set => SetStreamOptions(value, FfmpegStreamType.Audio);
        }

        #endregion ストリーム別設定

        public override void Clear()
        {
            base.Clear();
            _Streams?.Clear();
        }

        internal virtual void AppendArgs(StringBuilder builder)
        {
            builder.AppendIf("-f", Format);
            builder.AppendIf("-t", Duration);
            builder.AppendIf("-ss", SeekTo);
            builder.AppendIf("-sseof", SeekToLast);
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