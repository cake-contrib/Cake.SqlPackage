namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackageImportFixture : PackageFixture<SqlPackageImportSettings>
    {
        public SqlPackageImportFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        protected override void RunTool()
        {
            var tool = new SqlPackageImportRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Import(Settings);
        }
    }
}
