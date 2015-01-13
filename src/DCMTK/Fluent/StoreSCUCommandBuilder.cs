using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class StoreSCUCommandBuilder
    {
        private readonly string _exePath;
        private readonly string _peer;
        private readonly int _port;
        private readonly string _file;
        private string _callingAeTitle;
        private string _calledAeTitle;

        public StoreSCUCommandBuilder(string exePath, string peer, int port, string file)
        {
            if(!File.Exists(exePath))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", exePath));

            if(!File.Exists(file))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", file));

            _exePath = exePath;
            _peer = peer;
            _port = port;
            _file = file;
        }

        public string CallingAETitle
        {
            get { return _callingAeTitle; }
            set { _callingAeTitle = value; }
        }

        public string CalledAETitle
        {
            get { return _calledAeTitle; }
            set { _calledAeTitle = value; }
        }

        public StoreSCUInstance Build()
        {
            var commands = new List<ICommandLineOption>();

            if (!string.IsNullOrEmpty(CallingAETitle))
                commands.Add(KeyValueOption.Build("--aetitle", CallingAETitle));
            if (!string.IsNullOrEmpty(CalledAETitle))
                commands.Add(KeyValueOption.Build("--call", CalledAETitle));

            commands.Add(ValueOption.Build(_peer));
            commands.Add(ValueOption.Build(_port));
            commands.Add(ValueOption.Build(_file));

            return new StoreSCUInstance(_exePath, commands);
        }
    }
}
