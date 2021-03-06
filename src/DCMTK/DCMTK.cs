﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Fluent;

namespace DCMTK
{
    public class DCMTKContext
    {
        private readonly string _dcmtkBinDirectory;

        public DCMTKContext(string dcmtkBinDirectory)
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

        public StoreSCUCommandBuilder StoreSCU(string peer, int port, string file)
        {
            return new StoreSCUCommandBuilder(GetExePath("storescu"), peer, port, file);
        }

        public DcmToXmlCommandBuilder DcmToXml(string input, string output)
        {
            return new DcmToXmlCommandBuilder(GetExePath("dcm2xml"), input, output);
        }

        public XmlToDcmCommandBuilder XmlToDcm(string input, string output)
        {
            return new XmlToDcmCommandBuilder(GetExePath("xml2dcm"), input, output);
        }

        public FindSCUCommandBuilder Find(string peer, int port)
        {
            return new FindSCUCommandBuilder(GetExePath("findscu"), peer, port);
        }

        private string GetExePath(string exeName)
        {
            return Path.Combine(_dcmtkBinDirectory, exeName + ".exe");
        }
    }
}
