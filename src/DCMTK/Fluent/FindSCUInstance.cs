using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DCMTK.DICOM;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class FindSCUInstance : Instance
    {
        private FindResult _currentResult = null;
        private List<FindResult> _results = new List<FindResult>();
        private static Regex _dataRegex = new Regex(@"(\(.*\)) ([A-Z]{2}) (\[.*\])( *#)([ \d,]*)(.*)");
        private List<string> _errors = new List<string>();
        private List<string> _fatalErrors = new List<string>();
 
        public FindSCUInstance(string exePath, params ICommandLineOption[] options)
            : base(exePath, options)
        {
        }

        protected override void ConfigureProcess(Process process)
        {
            base.ConfigureProcess(process);
            process.OutputDataReceived += OnOutputDataReceived;
        }

        private void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(string.IsNullOrEmpty(e.Data)) return;

            if (e.Data.StartsWith("F: "))
            {
                _fatalErrors.Add(e.Data.Substring(3));
                return;
            }

            if (e.Data.StartsWith("E: "))
            {
                _errors.Add(e.Data.Substring(3));
                return;
            }
          
            if(!e.Data.StartsWith("W: ")) return;

            var line = e.Data.Substring(3);

            if (line == "---------------------------")
            {
                if (_currentResult != null)
                {
                    _results.Add(_currentResult);
                    _currentResult = null;
                }
                _currentResult = new FindResult();
                return;
            }

            if (_currentResult != null)
            {
                var match = _dataRegex.Match(line);
                if(!match.Success) return;
                var property = new FindProperty
                {
                    Tag = match.Groups[1].Value,
                    VR = match.Groups[2].Value,
                    Value = match.Groups[3].Value,
                    Name = match.Groups[6].Value
                };
                property.Value = property.Value.Substring(1, property.Value.Length - 2);
                _currentResult.Properties.Add(property);
            }
        }

        protected override void StartedProcess()
        {
            base.StartedProcess();
            _process.BeginOutputReadLine();
        }

        protected override void OnExited(object sender, EventArgs eventArgs)
        {
            base.OnExited(sender, eventArgs);
            if (_currentResult != null)
            {
                _results.Add(_currentResult);
                _currentResult = null;
            }
        }

        public List<FindResult> Result { get { return _results; } }

        public bool HasErrors { get { return _fatalErrors.Any() || _errors.Any(); } }

        public List<string> Errors { get { return _errors; } }

        public List<string> FatalErrors { get { return _errors;} } 

        public string ErrorMessage
        {
            get
            {
                var reason = _errors.FirstOrDefault(x => x.StartsWith("Reason: "));

                if (!string.IsNullOrEmpty(reason))
                    return reason.Substring(8);

                var errorMessage = new StringBuilder();

                foreach (var fatal in _fatalErrors)
                {
                    errorMessage.AppendLine("Fatal: " + fatal);
                }
                foreach (var error in _errors)
                {
                    errorMessage.AppendLine("Error: " + error);
                }

                return errorMessage.ToString();
            }
        }
    }
}
