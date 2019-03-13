namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageDriftReportFixture : PackageFixture<SqlPackageDriftReportSettings>
    {
        public SqlPackageDriftReportFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackageDriftReportSettings> CreateTool()
        {
            return new SqlPackageDriftReportRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
