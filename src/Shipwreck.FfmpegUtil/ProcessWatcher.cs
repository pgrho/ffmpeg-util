using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Shipwreck.FfmpegUtil
{
    public sealed class ProcessWatcher
    {
        private readonly CancellationToken _CancellationToken;

        private ProcessWatcher(Process process, CancellationToken cancellationToken)
        {
            _CancellationToken = cancellationToken;
            Process = process;

            var psi = process.StartInfo;

            if (!psi.RedirectStandardOutput)
            {
                StandardOutput = new List<string>();
            }
            if (!psi.RedirectStandardError)
            {
                StandardError = new List<string>();
            }

            if (StandardOutput != null || StandardError != null)
            {
                Task = new Task(Watch, TaskCreationOptions.LongRunning);
                Task.Start();
            }
            else
            {
                process.EnableRaisingEvents = true;

                var tcs = new TaskCompletionSource<int>();

                process.Exited += (s, e) =>
                {
                    tcs.SetResult(((Process)s).ExitCode);
                };
            }
        }

        public Process Process { get; }

        public Task Task { get; }

        public IList<string> StandardOutput { get; }
        public IList<string> StandardError { get; }

        public static ProcessWatcher Start(ProcessStartInfo psi, CancellationToken cancellationToken = default(CancellationToken))
            => new ProcessWatcher(Process.Start(psi), cancellationToken);

        private void Watch()
        {
            for (;;)
            {
                _CancellationToken.ThrowIfCancellationRequested();

                var ol = StandardOutput != null ? Process.StandardOutput.ReadLine() : null;
                var el = StandardError != null ? Process.StandardError.ReadLine() : null;

                if (ol != null)
                {
                    StandardOutput.Add(ol);
                }
                if (el != null)
                {
                    StandardError.Add(el);
                }

                if (ol != null || el != null)
                {
                    continue;
                }

                if (Process.HasExited)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
    }
}