namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageDeployReportFixture : PackageFixture<SqlPackageDeployReportSettings>
    {
        public SqlPackageDeployReportFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        /// <summary>Runs the tool.</summary>
        protected override void RunTool()
        {
            var tool = new SqlPackageDeployReportRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.DeployReport(Settings);
        }
    }
}
