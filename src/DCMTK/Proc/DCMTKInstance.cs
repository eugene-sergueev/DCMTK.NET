using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCMTK.Proc
{
    public class DCMTKInstance : Instance
    {
        public DCMTKInstance(string exePath, params ICommandLineOption[] options)
            : base(exePath, options)
        {
            OutputFatal = new List<string>();
            OutputError = new List<string>();
            OutputWarning = new List<string>();
            OutputOther = new List<string>();
        }

        protected override void OnExited(object sender, EventArgs eventArgs)
        {
            base.OnExited(sender, eventArgs);

            Output = _process.StandardOutput.ReadToEnd();

            if (!string.IsNullOrEmpty(Output))
            {
                foreach (var value in Output.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                {
                    if (string.IsNullOrEmpty(value))
                        continue;

                    if (value.StartsWith("F: "))
                        OutputFatal.Add(value.Substring(3));
                    else if (value.StartsWith("E: "))
                        OutputError.Add(value.Substring(3));
                    else if (value.StartsWith("W: "))
                        OutputWarning.Add(value.Substring(3));
                    else
                        OutputOther.Add(value);
                }
            }
        }

        public string Output { get; private set; }

        public List<string> OutputFatal { get; private set; }

        public List<string> OutputError { get; private set; }

        public List<string> OutputWarning { get; private set; }

        public List<string> OutputOther { get; private set; }

        public void ThrowException(string message)
        {
            throw new DCMTKInstanceException(message, Output, OutputFatal, OutputError, OutputWarning, OutputOther);
        }
    }
}
