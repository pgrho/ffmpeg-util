namespace Shipwreck.FfmpegUtil
{
    public enum VSync : byte
    {
        Auto = 0,
        Passthrough = 1,
        ConstantFrameRate = 2,
        VariableFrameRate = 3,
        Drop = 4,
    }
}