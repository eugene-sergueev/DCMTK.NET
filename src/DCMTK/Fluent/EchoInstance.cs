using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class EchoInstance : Instance
    {
        private static readonly Regex ReasonRegex = new Regex("F: Reason: (.*)");

        public EchoInstance(string exePath, IEnumerable<ICommandLineOption> options)
            : base(exePath, options.ToArray())
        {

        }

        public bool? Result { get; private set; }

        public string Reason { get; private set; }

        protected override void OnExited(object sender, EventArgs eventArgs)
        {
            base.OnExited(sender, eventArgs);
            var output = _process.StandardOutput.ReadToEnd();
            if (string.IsNullOrEmpty(output))
            {
                // success! no errors outputed!
                Result = true;
            }
            else
            {
                Result = false;
                var match = ReasonRegex.Match(output);
                Reason = match.Success ? Regex.Replace(match.Groups[1].Value, @"\r\n?|\n", "") : "Unknown";
            }
        }
    }
}
