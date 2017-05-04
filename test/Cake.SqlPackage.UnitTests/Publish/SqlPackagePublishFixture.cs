namespace Cake.SqlPackage.UnitTests
{
    internal sealed class SqlPackagePublishFixture : PackageFixture<SqlPackagePublishSettings>
    {
        public SqlPackagePublishFixture()
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        /// <summary>Runs the tool.</summary>
        protected override void RunTool()
        {
            var tool = new SqlPackagePublishRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Publish(Settings);
        }
    }
}
