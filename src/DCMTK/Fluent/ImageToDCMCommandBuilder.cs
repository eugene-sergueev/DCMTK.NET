using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCMTK.Proc;

namespace DCMTK.Fluent
{
    public class ImageToDCMCommandBuilder : ICommandBuilder
    {
        private readonly string _exePath;
        private readonly string _input;
        private readonly string _output;
        private readonly Dictionary<string, string> _keys = new Dictionary<string, string>();
        private readonly object _lock = new object();

        public ImageToDCMCommandBuilder(string exePath, string input, string output)
        {
            if(!File.Exists(exePath))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", exePath));

            if(!File.Exists(input))
                throw new Exception(string.Format("The path '{0}' doesn't exist.", input));

            if(string.IsNullOrEmpty(output))
                throw new Exception("You must provide an output location.");

            _exePath = exePath;
            _input = input;
            _output = output;
        }

        public InputFormatEnum InputFormat { get; set; }

        public ImageToDCMCommandBuilder AddKey(string key, string value)
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

        public ImageToDCMInstance Build()
        {
            lock (_lock)
            {
                var commands = new List<ICommandLineOption>();

                if (InputFormat == InputFormatEnum.Bmp)
                    commands.Add(KeyValueOption.Build("-i", "BMP"));

                commands.AddRange(_keys.Select(key => ValueOption.Build(string.Format("-k {0}=\"{1}\"", key.Key, key.Value))));

                commands.Add(ValueOption.Build(_input));
                commands.Add(ValueOption.Build(_output));

                return new ImageToDCMInstance(_exePath, commands);
            }
        }

        public enum InputFormatEnum
        {
            Jpeg,
            Bmp
        }
    }
}
