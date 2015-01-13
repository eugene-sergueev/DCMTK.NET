using DCMTK.Fluent;

namespace DCMTK
{
    public static class Extensions
    {
        public static EchoCommandBuilder SetCallingAETitle(this EchoCommandBuilder builder, string aeTitle)
        {
            builder.CallingAETitle = aeTitle;
            return builder;
        }

        public static EchoCommandBuilder SetCalledAETitle(this EchoCommandBuilder builder, string aeTitle)
        {
            builder.CalledAETitle = aeTitle;
            return builder;
        }

        public static ImageToDCMCommandBuilder SetInputFormat(this ImageToDCMCommandBuilder builder, ImageToDCMCommandBuilder.InputFormatEnum inputFormat)
        {
            builder.InputFormat = inputFormat;
            return builder;
        }

        public static StoreSCUCommandBuilder SetCallingAETitle(this StoreSCUCommandBuilder builder, string aeTitle)
        {
            builder.CallingAETitle = aeTitle;
            return builder;
        }

        public static StoreSCUCommandBuilder SetCalledAETitle(this StoreSCUCommandBuilder builder, string aeTitle)
        {
            builder.CalledAETitle = aeTitle;
            return builder;
        }
    }
}
