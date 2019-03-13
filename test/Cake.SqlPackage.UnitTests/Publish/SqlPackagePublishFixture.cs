namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackagePublishFixture : PackageFixture<SqlPackagePublishSettings>
    {
        public SqlPackagePublishFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackagePublishSettings> CreateTool()
        {
            return new SqlPackagePublishRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
