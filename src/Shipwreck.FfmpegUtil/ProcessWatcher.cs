using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            var tasks = new List<Task>(3);
            if (psi.RedirectStandardOutput)
            {
                StandardOutput = new List<string>();
                var stdout = new Task(() => Watch(Process.StandardOutput, StandardOutput), TaskCreationOptions.LongRunning);
                stdout.Start();
                tasks.Add(stdout);
            }
            if (psi.RedirectStandardError)
            {
                StandardError = new List<string>();
                var stdout = new Task(() => Watch(Process.StandardError, StandardError), TaskCreationOptions.LongRunning);
                stdout.Start();
                tasks.Add(stdout);
            }

            if (tasks.Count == 0 && cancellationToken.CanBeCanceled)
            {
                var cancel = new Task(WatchCancel, TaskCreationOptions.LongRunning);
                cancel.Start();
                tasks.Add(cancel);
            }

            if (tasks.Count > 1)
            {
                Task = Task.WhenAll(tasks);
            }
            else if (tasks.Count == 1)
            {
                Task = tasks[0];
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

        private void Watch(StreamReader r, ICollection<string> collection)
        {
            for (var l = r.ReadLine(); l != null; l = r.ReadLine())
            {
                if (_CancellationToken.IsCancellationRequested && !Process.HasExited)
                {
                    try
                    {
                        Process.Kill();
                    }
                    catch { }
                    _CancellationToken.ThrowIfCancellationRequested();
                }

                collection.Add(l);
            }
        }

        private void WatchCancel()
        {
            for (;;)
            {
                if (_CancellationToken.IsCancellationRequested && !Process.HasExited)
                {
                    try
                    {
                        Process.Kill();
                    }
                    catch { }
                    _CancellationToken.ThrowIfCancellationRequested();
                }

                Thread.Sleep(250);
            }
        }
    }
}