using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TsBuilder
{
    static class WinConsole
    {
        public static void Setup()
        {
            const int STD_OUTPUT_HANDLE = -11;

            var hOut = GetStdHandle(STD_OUTPUT_HANDLE);
            
            if (hOut != IntPtr.Zero)
            {
                uint mode;
                if (GetConsoleMode(hOut, out mode))
                {
                    //add ENABLE_VIRTUAL_TERMINAL_PROCESSING flag which enables support for ANSI escape codes
                    mode |= 0x0004;
                    SetConsoleMode(hOut, mode);
                }
            }
        }

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll")]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
    }
}
