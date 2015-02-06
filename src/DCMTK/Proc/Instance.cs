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
        private bool _isFinished;

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

            var startInfo = new ProcessStartInfo(exePath, command.ToString());
            ConfigureStartInfo(startInfo);

            _process = new Process { StartInfo = startInfo };
            ConfigureProcess(_process);
        }

        public bool IsStarted { get { return _isStarted; } }

        public bool IsFinished { get { return _isFinished; } }

        public void Start()
        {
            lock (_lock)
            {
                if (_isStarted) return;
                _isStarted = true;
                _process.Start();
                StartedProcess();
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                if (!_isStarted) throw new Exception("You must start before you can stop");
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
            _isFinished = true;
        }

        protected virtual void ConfigureStartInfo(ProcessStartInfo processStartInfo)
        {
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
        }

        protected virtual void ConfigureProcess(Process process)
        {
            process.EnableRaisingEvents = true;
            process.Exited += OnExited;
        }

        protected virtual void StartedProcess()
        {
            
        }
    }
}
