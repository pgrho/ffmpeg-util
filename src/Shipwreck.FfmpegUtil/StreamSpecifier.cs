using System;
using System.ComponentModel;

namespace Shipwreck.FfmpegUtil
{
    public struct StreamSpecifier : IEquatable<StreamSpecifier>
    {
        private byte _Index;

        public StreamSpecifier(FfmpegStreamType type)
        {
            _Index = 0;
            Type = type;
        }

        public StreamSpecifier(byte? index)
            : this(FfmpegStreamType.All, index)
        {
        }

        public StreamSpecifier(FfmpegStreamType type, byte? index)
        {
            _Index = index == null ? (byte)0 : (byte)(index.Value + 1);
            Type = type;
        }

        public bool IsEmpty
            => Type == FfmpegStreamType.All && Index == null;

        [DefaultValue(FfmpegStreamType.All)]
        public FfmpegStreamType Type { get; set; }

        [DefaultValue(null)]
        public byte? Index
        {
            get => _Index == 0 ? (byte?)null : (byte)(_Index - 1);
            set
            {
                if (value == null)
                {
                    _Index = 0;
                }
                else if (value == byte.MaxValue)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    _Index = (byte)(value.Value + 1);
                }
            }
        }

        public override string ToString()
        {
            var si = Index;
            return Type == FfmpegStreamType.All
                    ? (si == null ? string.Empty : si.Value.ToString())
                    : (si == null ? Type.ToArg().ToString() : (Type.ToArg().ToString() + ":" + si.Value));
        }

        public override bool Equals(object obj)
            => obj is StreamSpecifier && this == (StreamSpecifier)obj;

        public bool Equals(StreamSpecifier other)
            => this == other;

        public override int GetHashCode()
            => ((int)Type << 8) + _Index;

        public static bool operator ==(StreamSpecifier left, StreamSpecifier right)
            => left.Type == right.Type && left._Index == right._Index;

        public static bool operator !=(StreamSpecifier left, StreamSpecifier right)
            => left.Type != right.Type || left._Index != right._Index;
    }
}