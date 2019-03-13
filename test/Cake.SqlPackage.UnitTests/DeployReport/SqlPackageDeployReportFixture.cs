namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageDeployReportFixture : PackageFixture<SqlPackageDeployReportSettings>
    {
        public SqlPackageDeployReportFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackageDeployReportSettings> CreateTool()
        {
            return new SqlPackageDeployReportRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
