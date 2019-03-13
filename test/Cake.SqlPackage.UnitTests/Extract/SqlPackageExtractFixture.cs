namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageExtractFixture : PackageFixture<SqlPackageExtractSettings>
    {
        public SqlPackageExtractFixture()
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override SqlPackageRunner<SqlPackageExtractSettings> CreateTool()
        {
            return new SqlPackageExtractRunner(FileSystem, Environment, ProcessRunner, Tools);
        }
    }
}
