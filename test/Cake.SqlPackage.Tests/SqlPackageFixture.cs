using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.SqlPackage.Tests
{
    public class SqlPackageFixture : ToolFixture<SqlPackageSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cake.Testing.Fixtures.ToolFixture`1" /> class.
        /// </summary>
        public SqlPackageFixture() 
            : base("SqlPackage.exe")
        {
            Settings.WorkingDirectory = "/Working";
        }

        /// <summary>Runs the tool.</summary>
        protected override void RunTool()
        {
            var tool = new SqlPackageRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(Settings);
        }
    }
}
