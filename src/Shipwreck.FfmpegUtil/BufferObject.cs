using System;
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
        private readonly List<byte> _Data;

        internal BufferObject()
        {
            _Data = new List<byte>();
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains any value.
        /// </summary>
        public virtual bool IsEmpty
            => _Data?.Count > 0;

        public virtual void Clear()
            => _Data?.Clear();

        private static bool IsSupportedType(Type type)
            => type.GetTypeInfo().IsValueType || type == typeof(string);

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

        private unsafe bool TryGetValue(byte propertyIndex, byte* buffer)
        {
            var i = 0;
            while (i < _Data.Count)
            {
                var p = _Data[i++];
                var s = _Data[i++] * 256 + _Data[i++];

                if (p == propertyIndex)
                {
                    for (var j = 0; j < s; j++)
                    {
                        buffer[j] = _Data[i++];
                    }
                    return true;
                }
                else
                {
                    i += s;
                }
            }
            return false;
        }

        private unsafe string GetStringValue(byte propertyIndex)
        {
            var i = 0;
            while (i < _Data.Count)
            {
                var p = _Data[i++];
                var s = _Data[i++] * 256 + _Data[i++];

                if (p == propertyIndex)
                {
                    var buf = new byte[s];
                    _Data.CopyTo(i, buf, 0, buf.Length);

                    return Encoding.UTF8.GetString(buf, 0, buf.Length);
                }
                else
                {
                    i += s;
                }
            }
            return null;
        }

        private unsafe void SetValue(byte propertyIndex, short size, byte* buffer)
        {
            var i = 0;
            while (i < _Data.Count)
            {
                var p = _Data[i++];
                var s = _Data[i++] * 256 + _Data[i++];

                if (p == propertyIndex)
                {
                    if (s != size)
                    {
                        _Data[i - 2] = (byte)(size >> 8);
                        _Data[i - 1] = (byte)size;

                        if (s > size)
                        {
                            _Data.RemoveRange(i, s - size);
                        }
                        else
                        {
                            _Data.InsertRange(i, Enumerable.Repeat(byte.MinValue, size - s));
                        }
                    }

                    for (var j = 0; j < size; j++)
                    {
                        _Data[i++] = buffer[j];
                    }
                    return;
                }
                else
                {
                    i += s;
                }
            }
            _Data.Insert(i++, propertyIndex);
            _Data.Insert(i++, (byte)(size >> 8));
            _Data.Insert(i++, (byte)size);

            for (var j = 0; j < size; j++)
            {
                _Data.Insert(i++, buffer[j]);
            }
        }

        private unsafe bool RemoveValue(byte propertyIndex)
        {
            var i = 0;
            while (i < _Data.Count)
            {
                var p = _Data[i++];
                var s = _Data[i++] * 256 + _Data[i++];

                if (p == propertyIndex)
                {
                    _Data.RemoveRange(i - 3, s + 3);
                    return true;
                }
                else
                {
                    i += s;
                }
            }
            return false;
        }

        private byte GetPropertyIndex(string property)
            => GetPropertyIndexes(GetType())[property];

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