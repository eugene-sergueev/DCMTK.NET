using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMTK.Proc
{
    public class KeyValueOption : ICommandLineOption
    {
        private readonly string _key;
        private readonly string _value;

        public KeyValueOption(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public bool AppendOption(StringBuilder command)
        {
            command.Append(_key + " " + _value);
            return true;
        }

        public static KeyValueOption Build(string key, string value)
        {
            return new KeyValueOption(key, value);
        }
    }
}
