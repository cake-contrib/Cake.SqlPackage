namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageDriftReportFixture : PackageFixture<SqlPackageDriftReportSettings>
    {
        public SqlPackageDriftReportFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override void RunTool()
        {
            var tool = new SqlPackageDriftReportRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.DriftReport(Settings);
        }
    }
}
