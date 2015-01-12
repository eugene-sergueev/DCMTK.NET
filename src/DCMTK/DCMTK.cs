using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Fluent;

namespace DCMTK
{
    public class DCMTK
    {
        private readonly string _dcmtkBinDirectory;

        public DCMTK(string dcmtkBinDirectory)
        {
            if(!Directory.Exists(dcmtkBinDirectory))
                throw new Exception(string.Format("The bin directory '{0}' for dcmtk doesn't exist.", dcmtkBinDirectory));

            _dcmtkBinDirectory = dcmtkBinDirectory;
        }

        public EchoCommandBuilder Echo(string peer, int port)
        {
            return new EchoCommandBuilder(GetExePath("echoscu"), peer, port);
        }

        public ImageToDCMCommandBuilder ImageToDCM(string input, string output)
        {
            return new ImageToDCMCommandBuilder(GetExePath("img2dcm"), input, output);
        }

        private string GetExePath(string exeName)
        {
            return Path.Combine(_dcmtkBinDirectory, exeName + ".exe");
        }
    }
}
