namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageExportFixture : PackageFixture<SqlPackageExportSettings>
    {
        public SqlPackageExportFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override void RunTool()
        {
            var tool = new SqlPackageExportRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Export(Settings);
        }
    }
}
