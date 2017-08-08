using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    /// <summary>
    /// Provides a variable length properties storage.
    /// </summary>
    public abstract partial class BufferObject
    {
        private static readonly Dictionary<Type, Dictionary<string, byte>> _PropertyIndexes = new Dictionary<Type, Dictionary<string, byte>>();

        internal BufferObject()
        {
            Data = new List<byte>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains any value.
        /// </summary>
        public virtual bool IsEmpty
            => Data?.Count > 0;

        internal List<byte> Data { get; }

        public virtual void Clear()
            => Data?.Clear();

        private static bool IsSupportedType(Type type)
            => type.GetTypeInfo().IsValueType || type == typeof(string);

        #region Property map

        protected static Dictionary<string, byte> GetPropertyIndexes(Type type)
        {
            lock (_PropertyIndexes)
            {
                if (_PropertyIndexes.TryGetValue(type, out Dictionary<string, byte> d))
                {
                    return d;
                }
                d = new Dictionary<string, byte>();

                TypeInfo info = type.GetTypeInfo();

                if (type != typeof(BufferObject) && info.BaseType != null)
                {
                    foreach (var kv in GetPropertyIndexes(info.BaseType))
                    {
                        d.Add(kv.Key, kv.Value);
                    }
                }

                foreach (var p in info.DeclaredProperties)
                {
                    if (!p.CanRead
                        || !p.CanWrite
                        || !IsSupportedType(p.PropertyType)
                        || p.GetCustomAttribute<IgnoreBufferAttribute>() != null)
                    {
                        continue;
                    }

                    if (!d.ContainsKey(p.Name))
                    {
                        if (d.Count == byte.MaxValue)
                        {
                            throw new InvalidOperationException($"Defined property count of {type} exceeds {byte.MaxValue}");
                        }
                        d.Add(p.Name, (byte)d.Count);
                    }
                }

                _PropertyIndexes[type] = d;
                return d;
            }
        }

        internal byte GetPropertyIndex(string property)
            => GetPropertyIndexes(GetType())[property];

        #endregion Property map

        #region Buffer Entry

        public struct BufferEntry
        {
            internal BufferEntry(int index, byte property, short size)
            {
                Index = index;
                Property = property;
                Size = size;
            }

            public int Index { get; }
            public byte Property { get; }
            public short Size { get; }
        }

        public struct BufferEntryEnumerator : IEnumerator<BufferEntry>
        {
            private readonly List<byte> _Data;

            private int i;

            internal BufferEntryEnumerator(List<byte> data)
            {
                _Data = data;
                i = -1;
            }

            public BufferEntry Current
                => new BufferEntry(i, _Data[i], (short)(_Data[i + 1] * 256 + _Data[i + 2]));

            object IEnumerator.Current
                => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (i < 0)
                {
                    i = 0;
                }
                else
                {
                    var c = Current;
                    i = c.Index + c.Size + 3;
                }
                return i < _Data.Count;
            }

            public void Reset()
            {
                i = -1;
            }
        }

        public BufferEntryEnumerator GetEnumerator()
            => new BufferEntryEnumerator(Data);

        internal unsafe bool TryGetValue(byte propertyIndex, byte* buffer)
        {
            foreach (var e in this)
            {
                if (e.Property > propertyIndex)
                {
                    break;
                }
                else if (e.Property == propertyIndex)
                {
                    var s = e.Size;
                    var i = e.Index + 3;

                    for (var j = 0; j < s; j++)
                    {
                        buffer[j] = Data[i++];
                    }
                    return true;
                }
            }
            return false;
        }

        internal unsafe string GetStringValue(byte propertyIndex)
        {
            foreach (var e in this)
            {
                if (e.Property > propertyIndex)
                {
                    break;
                }
                else if (e.Property == propertyIndex)
                {
                    var s = e.Size;
                    var buf = new byte[s];
                    Data.CopyTo(e.Index + 3, buf, 0, buf.Length);

                    return Encoding.UTF8.GetString(buf, 0, buf.Length);
                }
            }
            return null;
        }

        internal unsafe void SetValue(byte propertyIndex, short size, byte* buffer)
        {
            var i = 0;
            foreach (var e in this)
            {
                if (e.Property > propertyIndex)
                {
                    break;
                }
                else if (e.Property == propertyIndex)
                {
                    var j = e.Index + 3;
                    var s = e.Size;
                    if (s != size)
                    {
                        Data[j - 2] = (byte)(size >> 8);
                        Data[j - 1] = (byte)size;

                        if (s > size)
                        {
                            Data.RemoveRange(j, s - size);
                        }
                        else
                        {
                            Data.InsertRange(j, Enumerable.Repeat(byte.MinValue, size - s));
                        }
                    }

                    for (var k = 0; k < size; k++)
                    {
                        Data[j++] = buffer[k];
                    }
                    return;
                }
                i = e.Index + e.Size + 3;
            }
            Data.Insert(i++, propertyIndex);
            Data.Insert(i++, (byte)(size >> 8));
            Data.Insert(i++, (byte)size);

            for (var j = 0; j < size; j++)
            {
                Data.Insert(i++, buffer[j]);
            }
        }

        internal unsafe bool RemoveValue(byte propertyIndex)
        {
            foreach (var e in this)
            {
                if (e.Property > propertyIndex)
                {
                    break;
                }
                else if (e.Property == propertyIndex)
                {
                    Data.RemoveRange(e.Index, e.Size + 3);
                    return true;
                }
            }
            return false;
        }

        #endregion Buffer Entry

        protected string GetString([CallerMemberName]string property = null)
            => GetStringValue(GetPropertyIndex(property));

        protected unsafe void SetValue(string value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == null)
            {
                RemoveValue(p);
            }
            else
            {
                var d = Encoding.UTF8.GetBytes(value);
                fixed (byte* b = d)
                {
                    SetValue(p, (short)d.Length, b);
                }
            }
        }

        protected T GetEnum<T>([CallerMemberName]string property = null)
            where T : struct //, IConvertible
        {
            var ut = Enum.GetUnderlyingType(typeof(T));

            switch (Marshal.SizeOf(ut))
            {
                case 1:
                    return (T)(object)GetByte(property);

                case 2:
                    return (T)(object)GetInt16(property);

                case 4:
                    return (T)(object)GetInt32(property);

                case 8:
                    return (T)(object)GetInt64(property);

                default:
                    throw new NotSupportedException();
            }
        }

        protected unsafe void SetValue<T>(T value, [CallerMemberName]string property = null)
            where T : struct //, IConvertible
        {
            // TODO: use IConvertible

            var ut = Enum.GetUnderlyingType(typeof(T));

            switch (Marshal.SizeOf(ut))
            {
                case 1:
                    SetValue((byte)(object)value, property);
                    break;

                case 2:
                    SetValue((short)(object)value, property);
                    break;

                case 4:
                    SetValue((int)(object)value, property);
                    break;

                case 8:
                    SetValue((long)(object)value, property);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}