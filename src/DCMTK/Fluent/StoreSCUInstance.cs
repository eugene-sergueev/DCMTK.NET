using System;
using System.Collections.Generic;
using System.Linq;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class StoreSCUInstance : Instance
    {
        public StoreSCUInstance(string exePath, IEnumerable<ICommandLineOption> options)
            : base(exePath, options)
        {

        }

        public string ErrorMessage { get; private set; }

        protected override void OnExited(object sender, EventArgs eventArgs)
        {
            base.OnExited(sender, eventArgs);
            var fatal = new List<string>();
            ParseOutput(_process.StandardOutput.ReadToEnd(), fatal, null, null, null);
            if (fatal.Any())
            {
                ErrorMessage = string.Join(" ", fatal.ToArray());
            }
        }
    }
}
