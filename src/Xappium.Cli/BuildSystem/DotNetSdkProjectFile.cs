﻿using System.IO;
using System.Threading.Tasks;
using Xappium.Tools;

namespace Xappium.BuildSystem
{
    internal class DotNetSdkProjectFile : CSProjFile
    {
        public DotNetSdkProjectFile(FileInfo projectFile, DirectoryInfo outputDirectory)
            : base(projectFile, outputDirectory)
        {
        }

        public override string Platform => ".NET";

        public override Task Build(string configuration)
        {
            if (string.IsNullOrEmpty(configuration))
                configuration = "Release";

            // dotnet build HelloForms -t:Run -f net6.0-ios
            return DotNetTool.Build(b =>
                    b.Add("build")
                     .Add($"{ProjectFile.FullName}")
                     .Add($"--output={OutputDirectory.FullName}")
                     .Add($"--configuration={configuration}"));
        }

        public override Task<bool> IsSupported()
        {
            return Task.FromResult(true);
        }
    }
}
