﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class XmlToDcmCommandBuilder : ICommandBuilder
    {
        private readonly string _exePath;
        private readonly string _input;
        private readonly string _output;

        public XmlToDcmCommandBuilder(string exePath, string input, string output)
        {
            _exePath = exePath;
            _input = input;
            _output = output;
            _output = output;

            if(!File.Exists(exePath))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", exePath));

            if(string.IsNullOrEmpty(input))
                throw new ArgumentNullException("input");

            if(!File.Exists(input))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", input));

            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException("output");

            _exePath = exePath;
        }

        public XmlToDcmInstance Build()
        {
            var commands = new List<ICommandLineOption>();

            //commands.Add(ValueOption.Build("--ignore-meta-info"));

            commands.Add(ValueOption.Build("--verbose --debug --ignore-meta-info"));

            commands.Add(ValueOption.Build(_input));
            commands.Add(ValueOption.Build(_output));

            return new XmlToDcmInstance(_exePath, commands, _output);
        }
    }
}
