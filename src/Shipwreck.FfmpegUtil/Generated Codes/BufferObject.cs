using System;
using System.Runtime.CompilerServices;

namespace Shipwreck.FfmpegUtil
{
	partial class BufferObject
	{
        protected unsafe Boolean GetBoolean([CallerMemberName]string property = null)
        {
            Boolean r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Boolean);
        }

        protected unsafe void SetValue(Boolean value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Boolean))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Boolean), (byte*)&value);
            }
        }

        protected unsafe Byte GetByte([CallerMemberName]string property = null)
        {
            Byte r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Byte);
        }

        protected unsafe void SetValue(Byte value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Byte))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Byte), (byte*)&value);
            }
        }

        protected unsafe SByte GetSByte([CallerMemberName]string property = null)
        {
            SByte r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(SByte);
        }

        protected unsafe void SetValue(SByte value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(SByte))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(SByte), (byte*)&value);
            }
        }

        protected unsafe Int16 GetInt16([CallerMemberName]string property = null)
        {
            Int16 r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Int16);
        }

        protected unsafe void SetValue(Int16 value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Int16))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Int16), (byte*)&value);
            }
        }

        protected unsafe UInt16 GetUInt16([CallerMemberName]string property = null)
        {
            UInt16 r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(UInt16);
        }

        protected unsafe void SetValue(UInt16 value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(UInt16))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(UInt16), (byte*)&value);
            }
        }

        protected unsafe Int32 GetInt32([CallerMemberName]string property = null)
        {
            Int32 r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Int32);
        }

        protected unsafe void SetValue(Int32 value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Int32))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Int32), (byte*)&value);
            }
        }

        protected unsafe UInt32 GetUInt32([CallerMemberName]string property = null)
        {
            UInt32 r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(UInt32);
        }

        protected unsafe void SetValue(UInt32 value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(UInt32))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(UInt32), (byte*)&value);
            }
        }

        protected unsafe Int64 GetInt64([CallerMemberName]string property = null)
        {
            Int64 r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Int64);
        }

        protected unsafe void SetValue(Int64 value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Int64))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Int64), (byte*)&value);
            }
        }

        protected unsafe UInt64 GetUInt64([CallerMemberName]string property = null)
        {
            UInt64 r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(UInt64);
        }

        protected unsafe void SetValue(UInt64 value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(UInt64))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(UInt64), (byte*)&value);
            }
        }

        protected unsafe Single GetSingle([CallerMemberName]string property = null)
        {
            Single r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Single);
        }

        protected unsafe void SetValue(Single value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Single))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Single), (byte*)&value);
            }
        }

        protected unsafe Double GetDouble([CallerMemberName]string property = null)
        {
            Double r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Double);
        }

        protected unsafe void SetValue(Double value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Double))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Double), (byte*)&value);
            }
        }

        protected unsafe Decimal GetDecimal([CallerMemberName]string property = null)
        {
            Decimal r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(Decimal);
        }

        protected unsafe void SetValue(Decimal value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(Decimal))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(Decimal), (byte*)&value);
            }
        }

        protected unsafe DateTime GetDateTime([CallerMemberName]string property = null)
        {
            DateTime r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(DateTime);
        }

        protected unsafe void SetValue(DateTime value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(DateTime))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(DateTime), (byte*)&value);
            }
        }

        protected unsafe DateTimeOffset GetDateTimeOffset([CallerMemberName]string property = null)
        {
            DateTimeOffset r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(DateTimeOffset);
        }

        protected unsafe void SetValue(DateTimeOffset value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(DateTimeOffset))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(DateTimeOffset), (byte*)&value);
            }
        }

        protected unsafe TimeSpan GetTimeSpan([CallerMemberName]string property = null)
        {
            TimeSpan r;
            return TryGetValue(GetPropertyIndex(property), (byte*)&r) ? r : default(TimeSpan);
        }

        protected unsafe void SetValue(TimeSpan value, [CallerMemberName]string property = null)
        {
            var p = GetPropertyIndex(property);
            if (value == default(TimeSpan))
            {
                RemoveValue(p);
            }
            else
            {
                SetValue(p, (short)sizeof(TimeSpan), (byte*)&value);
            }
        }

	}
}