using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class StoreSCUCommandBuilder : ICommandBuilder
    {
        private readonly string _exePath;
        private readonly string _peer;
        private readonly int _port;
        private readonly string _file;

        public StoreSCUCommandBuilder(string exePath, string peer, int port, string file)
        {
            if(!File.Exists(exePath))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", exePath));

            if(!File.Exists(file))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", file));

            if(string.IsNullOrEmpty(peer))
                throw new ArgumentNullException("peer");

            if(port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _exePath = exePath;
            _peer = peer;
            _port = port;
            _file = file;
        }

        public string CallingAETitle { get; set; }

        public string CalledAETitle { get; set; }

        public bool ProposeUncompressed { get; set; }

        public bool ProposeLittle { get; set; }

        public bool ProposeBig { get; set; }

        public bool ProposeImplicit { get; set; }

        public bool ProposeLossless { get; set; }

        public bool ProposeJpeg8 { get; set; }

        public bool ProposeJpeg12 { get; set; }

        public bool ProposeJ2KLossless { get; set; }

        public bool ProposeJ2KLossy { get; set; }

        public bool ProposeJLSLossless { get; set; }

        public bool ProposeJLSLossy { get; set; }

        public bool ProposeMpeg2 { get; set; }

        public bool ProposeMpeg2High { get; set; }

        public bool ProposeRle { get; set; }

        public bool ProposeDeflated { get; set; }

        public bool ProposeRequired { get; set; }

        public bool ProposeCombine { get; set; }

        public StoreSCUInstance Build()
        {
            var commands = new List<ICommandLineOption>();

            if (!string.IsNullOrEmpty(CallingAETitle))
                commands.Add(KeyValueOption.Build("--aetitle", CallingAETitle));
            if (!string.IsNullOrEmpty(CalledAETitle))
                commands.Add(KeyValueOption.Build("--call", CalledAETitle));

            if(ProposeUncompressed)
                commands.Add(ValueOption.Build("--propose-uncompr"));
            if (ProposeLittle)
                commands.Add(ValueOption.Build("--propose-little"));
            if (ProposeBig)
                commands.Add(ValueOption.Build("--propose-big"));
            if (ProposeImplicit)
                commands.Add(ValueOption.Build("--propose-implicit"));
            if (ProposeLossless)
                commands.Add(ValueOption.Build("--propose-lossless"));
            if (ProposeJpeg8)
                commands.Add(ValueOption.Build("--propose-jpeg8"));
            if (ProposeJpeg12)
                commands.Add(ValueOption.Build("--propose-jpeg12"));
            if (ProposeJ2KLossless)
                commands.Add(ValueOption.Build("--propose-j2k-lossless"));
            if (ProposeJ2KLossy)
                commands.Add(ValueOption.Build("--propose-j2k-lossy"));
            if (ProposeJLSLossless)
                commands.Add(ValueOption.Build("--propose-jls-lossless"));
            if (ProposeJ2KLossy)
                commands.Add(ValueOption.Build("--propose-jls-lossy"));
            if (ProposeMpeg2)
                commands.Add(ValueOption.Build("--propose-mpeg2")); 
            if (ProposeMpeg2High)
                commands.Add(ValueOption.Build("--propose-mpeg2-high")); 
            if (ProposeRle)
                commands.Add(ValueOption.Build("--propose-rle"));
            if (ProposeDeflated)
                commands.Add(ValueOption.Build("--propose-deflated"));
            if (ProposeRequired)
                commands.Add(ValueOption.Build("--required"));
            if (ProposeCombine)
                commands.Add(ValueOption.Build("--combine")); 
          
            commands.Add(ValueOption.Build(_peer));
            commands.Add(ValueOption.Build(_port));
            commands.Add(ValueOption.Build(_file));

            return new StoreSCUInstance(_exePath, commands);
        }
    }
}
