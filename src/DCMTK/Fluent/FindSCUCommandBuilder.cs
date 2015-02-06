using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class FindSCUCommandBuilder : ICommandBuilder
    {
        private readonly string _exePath;
        private readonly string _peer;
        private readonly int _port;
        private readonly Dictionary<string, string> _keys = new Dictionary<string, string>();
        private readonly object _lock = new object();

        public FindSCUCommandBuilder(string exePath, string peer, int port)
        {
            if(!File.Exists(exePath))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", exePath));

            if(string.IsNullOrEmpty(peer))
                throw new ArgumentNullException("peer");

            if(port <= 0)
                throw new ArgumentOutOfRangeException("port");

            _exePath = exePath;
            _peer = peer;
            _port = port;
        }

        public string CallingAETitle { get; set; }

        public string CalledAETitle { get; set; }

        public FindSCUCommandBuilder AddKey(string key, string value)
        {
            lock (_lock)
            {
                if (!_keys.ContainsKey(key))
                {
                    _keys.Add(key, value);
                }
                else
                {
                    _keys[key] = value;
                }
            }

            return this;
        }

        public FindSCUInstance Build()
        {
            lock (_lock)
            {
                var commands = new List<ICommandLineOption>();

                if (!string.IsNullOrEmpty(CallingAETitle))
                    commands.Add(KeyValueOption.Build("--aetitle", CallingAETitle));
                if (!string.IsNullOrEmpty(CalledAETitle))
                    commands.Add(KeyValueOption.Build("--call", CalledAETitle));

                commands.AddRange(
                    _keys.Select(key => ValueOption.Build(string.Format("-k \"{0}={1}\"", key.Key, key.Value))));

                commands.Add(ValueOption.Build(_peer));
                commands.Add(ValueOption.Build(_port));

                return new FindSCUInstance(_exePath, commands.ToArray());
            }
        }
    }
}
