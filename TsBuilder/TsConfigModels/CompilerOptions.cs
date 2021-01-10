using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TsBuilder.TsConfigModels
{
    public class CompilerOptions
    {
        [DataMember(Name = "noImplicitAny")]
        public bool NoImplicitAny { get; }

        [DataMember(Name = "noEmitOnError")]
        public bool NoEmitOnError { get; }

        [DataMember(Name = "target")]
        public string Target { get; }

        [DataMember(Name = "sourceMap")]
        public bool SourceMap { get; }

        [DataMember(Name = "outDir")]
        public string OutDir { get; }

        [DataMember(Name = "removeComments")]
        public bool RemoveComments { get; }

        public CompilerOptions(EnvironmentType type)
        {
            if (type == EnvironmentType.Debug)
            {
                SourceMap = true;
                OutDir = "wwwroot/j";
            }
            else if (type == EnvironmentType.Prod)
            {
                RemoveComments = true;
                OutDir = "wwwroot/j/inputsForMin";
            }
            
            NoImplicitAny = false;
            NoEmitOnError = true;
            Target = "es5";
        }
    }
}
