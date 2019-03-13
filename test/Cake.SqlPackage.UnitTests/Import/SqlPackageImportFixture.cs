namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageImportFixture : PackageFixture<SqlPackageImportSettings>
    {
        public SqlPackageImportFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackageImportSettings> CreateTool()
        {
            return new SqlPackageImportRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
