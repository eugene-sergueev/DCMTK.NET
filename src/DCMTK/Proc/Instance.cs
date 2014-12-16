using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            for (var x = 0; x < options.Length - 1; x++)
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
                if(_isStarted) return;
                _isStarted = true;
                _process.Start();
            }
        }

        public void Wait()
        {
            lock (_lock)
            {
                if(!_isStarted) throw new Exception("The instance must be started before you can wait on it");
                if(_process.HasExited) return;
                _process.WaitForExit();
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if(_isDisposed) return;
                _isDisposed = true;
                _process.Exited -= OnExited;
                _process.Dispose();
            }
        }

        protected virtual void OnExited(object sender, EventArgs eventArgs)
        {

        }
    }
}
