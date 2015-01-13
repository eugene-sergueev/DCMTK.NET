using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMTK.Proc
{
    public class DCMTKInstanceException : Exception
    {
        public DCMTKInstanceException(string message, string output, List<string> fatal, List<string> error, List<string> warning, List<string> other)
            :base(message)
        {
            Output = output;
            Fatal = fatal != null ? fatal.ToList() : new List<string>();
            Error = error != null ? error.ToList() : new List<string>();
            Warning = warning != null ? warning.ToList() : new List<string>();
            Other = other != null ? other.ToList() : new List<string>();
        }

        public string Output { get; private set; }

        public List<string> Fatal { get; private set; }

        public List<string> Error { get; private set; }

        public List<string> Warning { get; private set; }

        public List<string> Other { get; private set; }

        public string MessagesFormated
        {
            get
            {
                if (!Fatal.Any() && !Error.Any() && !Warning.Any() && !Other.Any()) return null;

                var errorMessage = new StringBuilder();
                if (Other.Any())
                    errorMessage.AppendLine("Other (" +
                                            string.Join(",", Other.Select(x => "[" + x + "]").ToArray()) + ")");
                if (Warning.Any())
                    errorMessage.AppendLine("Warning (" +
                                            string.Join(",", Warning.Select(x => "[" + x + "]").ToArray()) +
                                            ")");
                if (Error.Any())
                    errorMessage.AppendLine("Error (" +
                                            string.Join(",", Error.Select(x => "[" + x + "]").ToArray()) + ")");
                if (Fatal.Any())
                    errorMessage.AppendLine("Fatal (" +
                                            string.Join(",", Fatal.Select(x => "[" + x + "]").ToArray()) + ")");
                return errorMessage.ToString();
            }
        }
    }
}
