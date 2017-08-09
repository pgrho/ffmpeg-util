using System.ComponentModel;
using System.Text;

namespace Shipwreck.FfmpegUtil
{
    public class FfmpegArgs : CommandLineArgs
    {
        public override bool IsEmpty
            => base.IsEmpty
                && InputOptions?.IsEmpty != false
                && OutputOptions?.IsEmpty != false;

        /// <summary>
        /// Gets or sets a value indicating whether the FFmpeg will overwrite output files without asking.
        /// </summary>
        [DefaultValue(null)]
        public bool? OverwriteOutputFiles
        {
            get => GetNullableBoolean();
            set => SetValue(value);
        }

        [DefaultValue(default(VSync))]
        public VSync VSync
        {
            get => GetEnum<VSync>();
            set => SetValue(value);
        }

        #region InputOptions

        public FfmpegInputOptions InputOptions { get; set; }

        public bool ShouldSerializeInputOptions()
            => InputOptions?.IsEmpty == false;

        public void ResetInputOptions()
            => InputOptions?.Clear();

        #endregion InputOptions

        #region OutputOptions

        public FfmpegOutputOptions OutputOptions { get; set; }

        public bool ShouldSerializeOutputOptions()
            => OutputOptions?.IsEmpty == false;

        public void ResetOutputOptions()
            => OutputOptions?.Clear();

        #endregion OutputOptions

        public override void Clear()
        {
            base.Clear();

            InputOptions?.Clear();
            OutputOptions?.Clear();
        }

        internal override void AppendArgs(StringBuilder builder)
        {
            base.AppendArgs(builder);

            var oof = OverwriteOutputFiles;
            if (oof != null)
            {
                builder.Append(oof.Value ? "-y " : "-n ");
            }
            var vs = VSync;
            if (vs != VSync.Auto)
            {
                builder.AppendIf("-vsync", vs.ToArg());
            }

            InputOptions?.AppendArgs(builder);
            OutputOptions?.AppendArgs(builder);
        }
    }
}