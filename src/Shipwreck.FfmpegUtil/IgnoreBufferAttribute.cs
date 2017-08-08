using System;

namespace Shipwreck.FfmpegUtil
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class IgnoreBufferAttribute : Attribute
    {
    }
}