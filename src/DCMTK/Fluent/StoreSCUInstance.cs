using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class StoreSCUInstance : DCMTKInstance
    {
        public StoreSCUInstance(string exePath, IEnumerable<ICommandLineOption> options)
            : base(exePath, options.ToArray())
        {

        }

        public bool WasScoreSuccesful
        {
            get
            {
                if (!IsFinished) return false;
                return !OutputError.Any() && !OutputFatal.Any();
            }
        }
    }
}
