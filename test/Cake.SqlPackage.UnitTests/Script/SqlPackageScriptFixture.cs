namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageScriptFixture : PackageFixture<SqlPackageScriptSettings>
    {
        public SqlPackageScriptFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        /// <summary>Runs the tool.</summary>
        protected override void RunTool()
        {
            var tool = new SqlPackageScriptRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Script(Settings);
        }
    }
}
