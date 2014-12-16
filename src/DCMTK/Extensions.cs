using DCMTK.Fluent;

namespace DCMTK
{
    public static class Extensions
    {
        public static EchoCommandBuilder SetCallingAETitle(this EchoCommandBuilder echoCommandBuilder, string aeTitle)
        {
            echoCommandBuilder.CallingAETitle = aeTitle;
            return echoCommandBuilder;
        }

        public static EchoCommandBuilder SetCalledAETitle(this EchoCommandBuilder echoCommandBuilder, string aeTitle)
        {
            echoCommandBuilder.CalledAETitle = aeTitle;
            return echoCommandBuilder;
        }
    }
}
