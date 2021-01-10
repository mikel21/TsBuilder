using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TsBuilder.TsConfigModels
{
    public class TsConfig
    {
        [DataMember(Name = "compilerOptions")]
        public CompilerOptions CompilerOptions { get; }

        [DataMember(Name = "compileOnSave")]
        public bool CompileOnSave { get; }

        [DataMember(Name = "include")]
        public string[] IncludeDirectories { get; }

        [DataMember(Name = "exclude")]
        public string[] ExcludeDirectories { get; }

        public TsConfig(EnvironmentType environment)
        {
            CompilerOptions = new CompilerOptions(environment);
            CompileOnSave = false;
            IncludeDirectories = new string[] { "wwwroot" };
            ExcludeDirectories = new string[] { "node_modules", "Content/j/ts" };
        }
    }
}
