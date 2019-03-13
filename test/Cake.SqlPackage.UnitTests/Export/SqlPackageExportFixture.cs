namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageExportFixture : PackageFixture<SqlPackageExportSettings>
    {
        public SqlPackageExportFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackageExportSettings> CreateTool()
        {
            return new SqlPackageExportRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
