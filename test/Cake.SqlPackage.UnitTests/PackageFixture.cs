using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.SqlPackage.UnitTests
{
    internal abstract class PackageFixture<TSettings> : ToolFixture<TSettings, ToolFixtureResult>
        where TSettings : SqlPackageSettings, new()
    {
        public ProcessSettings ProcessSettings { get; set; }

        protected PackageFixture() : base("SqlPackage.exe")
        {
            ProcessRunner.Process.SetStandardOutput(new string[] { });
        }

        protected override ToolFixtureResult CreateResult(FilePath path, ProcessSettings process)
        {
            return new ToolFixtureResult(path, process);
        }

        /// <summary>Runs the tool.</summary>
        protected override void RunTool()
        {
            CreateTool().Execute(Settings, ProcessSettings);
        }

        protected abstract SqlPackageRunner<TSettings> CreateTool();
    }
}
