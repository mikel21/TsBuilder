using System;
using System.Collections.Generic;
using System.Text;

namespace TsBuilder
{
    public static class Environment
    {
        public static EnvironmentType Type { get; private set; }

        public static void SetEnvironmentType(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Type = EnvironmentType.Debug;
                return;
            }

            if (args[0] == "--production")
                Type = EnvironmentType.Prod;
            else
                throw new Exception("environment is not defined");
        }
    }
}
