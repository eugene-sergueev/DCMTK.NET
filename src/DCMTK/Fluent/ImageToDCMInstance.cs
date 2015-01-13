using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class ImageToDCMInstance : DCMTKInstance
    {
        public ImageToDCMInstance(string exePath, IEnumerable<ICommandLineOption> options)
            :base(exePath, options.ToArray())
        {
            
        }

        public bool WasConversionSuccesful
        {
            get
            {
                if (!IsFinished) return false;
                return !OutputFatal.Any();
            }
        }
    }
}
