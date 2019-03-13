namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageScriptFixture : PackageFixture<SqlPackageScriptSettings>
    {
        public SqlPackageScriptFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackageScriptSettings> CreateTool()
        {
            return new SqlPackageScriptRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
