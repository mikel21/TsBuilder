using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TsBuilder
{
    public static class Cmd
    {
        public static async Task<string> ExecuteAsync(string command)
        {
            string result;
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = GetExecuter(),
                    Arguments = GetCommand(command),
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true//,
                    //WorkingDirectory = directory
                }
            };
            process.Start();
            result = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();

            return result;
        }
        
        private static string GetExecuter()
        {
            if (OS.Type == OSPlatform.Windows)
            {
                return "cmd.exe";
            }
            else if (OS.Type == OSPlatform.Linux)
            {
                return "/bin/bash";
            }

            return "";
        }

        private static string GetCommand(string inputCommand)
        {
            if (OS.Type == OSPlatform.Windows)
            {
                return $"/c {inputCommand}";
            }
            else if (OS.Type == OSPlatform.Linux)
            {
                return $"-c \"{inputCommand}\"";
            }

            return "";
        }
    }
}
