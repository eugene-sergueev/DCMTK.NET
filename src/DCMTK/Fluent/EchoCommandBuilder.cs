using System;
using System.Collections.Generic;
using System.IO;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class EchoCommandBuilder : ICommandBuilder
    {
        private readonly string _exePath;
        private readonly string _peer;
        private readonly int _port;
        private string _callingAeTitle;
        private string _calledAeTitle;

        public EchoCommandBuilder(string exePath, string peer, int port)
        {
            if(!File.Exists(exePath))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", exePath));

            _exePath = exePath;
            _peer = peer;
            _port = port;
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

        public EchoInstance Build()
        {
            var commands = new List<ICommandLineOption>();

            commands.Add(ValueOption.Build(_peer));
            commands.Add(ValueOption.Build(_port));

            if(!string.IsNullOrEmpty(CallingAETitle))
                commands.Add(KeyValueOption.Build("--aetitle", CallingAETitle));
            if(!string.IsNullOrEmpty(CalledAETitle))
                commands.Add(KeyValueOption.Build("--call", CalledAETitle));

            return new EchoInstance(_exePath, commands);
        }
    }
}
