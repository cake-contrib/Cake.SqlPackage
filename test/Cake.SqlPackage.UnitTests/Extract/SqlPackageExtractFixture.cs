namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageExtractFixture : PackageFixture<SqlPackageExtractSettings>
    {
        public SqlPackageExtractFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override void RunTool()
        {
            var tool = new SqlPackageExtractRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Extract(Settings);
        }
    }
}
