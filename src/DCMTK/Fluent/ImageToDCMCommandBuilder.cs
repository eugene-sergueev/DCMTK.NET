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

        public ImageToDCMInstance Build()
        {
            var commands = new List<ICommandLineOption>();

            if (InputFormat == InputFormatEnum.Bmp)
                commands.Add(KeyValueOption.Build("-i", "BMP"));

            commands.Add(ValueOption.Build(_input));
            commands.Add(ValueOption.Build(_output));

            return new ImageToDCMInstance(_exePath, commands);
        }

        public enum InputFormatEnum
        {
            Jpeg,
            Bmp
        }
    }
}
