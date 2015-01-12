using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

namespace DCMTK.Proc
{
    public class Instance : IDisposable
    {
        // ReSharper disable InconsistentNaming
        protected readonly Process _process;
        // ReSharper restore InconsistentNaming
        private readonly object _lock = new object();
        private bool _isDisposed;
        private bool _isStarted;

        public Instance(string exePath, params ICommandLineOption[] options)
        {
            if (!File.Exists(exePath))
                throw new Exception(string.Format("The exe path '{0}' doesn't exist.", exePath));

            var command = new StringBuilder();

            for (var x = 0; x < options.Length; x++)
            {
                if (options[x].AppendOption(command) && x < options.Length)
                    command.Append(" ");
            }

            _process = new Process
            {
                StartInfo = new ProcessStartInfo(exePath, command.ToString())
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                }
            };
            _process.EnableRaisingEvents = true;
            _process.Exited += OnExited;
        }

        public Instance(string exePath, IEnumerable<ICommandLineOption> options)
            : this(exePath, options.ToArray())
        {

        }

        public void Start()
        {
            lock (_lock)
            {
                if (_isStarted) return;
                _isStarted = true;
                _process.Start();
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if(!_isStarted) throw new Exception("You must start before you can stop");
                _process.Kill();
            }
        }

        public void Wait()
        {
            lock (_lock)
            {
                if (!_isStarted) throw new Exception("The instance must be started before you can wait on it");
                if (_process.HasExited) return;
                _process.WaitForExit();
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_isDisposed) return;
                _isDisposed = true;
                _process.Exited -= OnExited;
                _process.Dispose();
            }
        }

        protected virtual void OnExited(object sender, EventArgs eventArgs)
        {

        }

        protected void ParseOutput(string output, List<string> fatal, List<string> error, List<string> warning, List<string> other)
        {
            if (string.IsNullOrEmpty(output))
                return;

            foreach (var value in output.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (string.IsNullOrEmpty(value))
                    continue;

                if (value.StartsWith("F: ") && fatal != null)
                    fatal.Add(value.Substring(3));
                else if (value.StartsWith("E: ") && error != null)
                    error.Add(value.Substring(3));
                else if (value.StartsWith("W: ") && warning != null)
                    warning.Add(value.Substring(3));
                else if (other != null)
                    other.Add(value);
            }

        }
    }
}
