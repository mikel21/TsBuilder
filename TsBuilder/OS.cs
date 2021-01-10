using System.Runtime.InteropServices;

namespace TsBuilder
{
    public static class OS
    {
        static OS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Type = OSPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Type = OSPlatform.Linux;
            }
        }

        public static OSPlatform Type { get; }
    }
}
