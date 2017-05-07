using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.SqlPackage.UnitTests
{
    internal abstract class PackageFixture<TSettings> : ToolFixture<TSettings, ToolFixtureResult>
        where TSettings : SqlPackageSettings, new()
    {
        protected PackageFixture(string toolName) : base("SqlPackage.exe")
        {
            ProcessRunner.Process.SetStandardOutput(new string[] { });
        }

        protected override ToolFixtureResult CreateResult(FilePath path, ProcessSettings process)
        {
            return new ToolFixtureResult(path, process);
        }
    }
}
