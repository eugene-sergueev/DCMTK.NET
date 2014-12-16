using System;
using System.Text;

namespace DCMTK.Proc
{
    public class ValueOption : ICommandLineOption
    {
        private readonly object _flag;

        public ValueOption(object flag)
        {
            _flag = flag;
        }

        public bool AppendOption(StringBuilder command)
        {
            if (_flag == null) return false;
            if (_flag is string && string.IsNullOrEmpty((string) _flag)) return false;
            command.Append(_flag);
            return true;
        }

        public static ValueOption Build(object value)
        {
            return new ValueOption(value);
        }
    }
}
