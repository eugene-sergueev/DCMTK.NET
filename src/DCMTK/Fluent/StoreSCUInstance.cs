using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var error = new List<string>();
            var warning = new List<string>();
            var other = new List<string>();
            ParseOutput(_process.StandardOutput.ReadToEnd(), fatal, error, warning, other);
            if (fatal.Any() || error.Any() || warning.Any() || other.Any())
            {
                var errorMessage = new StringBuilder();
                if (other.Any())
                    errorMessage.AppendLine("Other (" + string.Join(",", other.Select(x => "[" + x + "]").ToArray()) + ")");
                if (warning.Any())
                    errorMessage.AppendLine("Warning (" + string.Join(",", warning.Select(x => "[" + x + "]").ToArray()) + ")");
                if (error.Any())
                    errorMessage.AppendLine("Error (" + string.Join(",", error.Select(x => "[" + x + "]").ToArray()) + ")");
                if (fatal.Any())
                    errorMessage.AppendLine("Fatal (" + string.Join(",", fatal.Select(x => "[" + x + "]").ToArray()) + ")");
                ErrorMessage = errorMessage.ToString();
            }
        }
    }
}
