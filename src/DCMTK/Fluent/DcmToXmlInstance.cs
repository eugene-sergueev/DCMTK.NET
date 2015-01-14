using System;
using System.Collections.Generic;
using System.Linq;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class DcmToXmlInstance : DCMTKInstance
    {
        public DcmToXmlInstance(string exePath, IEnumerable<ICommandLineOption> options, string output)
            : base(exePath, options.ToArray())
        {
            OutputFile = output;
        }

        public string OutputFile { get; private set; }

        public bool WasSuccessful { get; set; }

        protected override void OnExited(object sender, EventArgs eventArgs)
        {
            base.OnExited(sender, eventArgs);
            WasSuccessful = string.IsNullOrEmpty(Output);
        }
    }
}
