using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;
using Xunit;

namespace Cake.SqlPackage.UnitTests
{
    public sealed class SqlPackageScriptRunnerTests
    {
        public sealed class TheRunMethod
        {
            private SqlPackageScriptFixture fixture;

            public TheRunMethod()
            {
                fixture = new SqlPackageScriptFixture();
            }

            [Fact]
            public void Should_Throw_If_Settings_Is_Null()
            {
                // Given
                fixture.Settings = null;

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<ArgumentNullException>(result);
            }

            [Fact]
            public void Should_Throw_If_Sql_Package_Runner_Was_Not_Found()
            {
                // Given
                fixture.GivenDefaultToolDoNotExist();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("SqlPackage: Could not locate executable.", result?.Message);
            }

            [Theory]
            [InlineData("/bin/tools/SqlPackage/SqlPackage.exe", "/bin/tools/SqlPackage/SqlPackage.exe")]
            [InlineData("./tools/SqlPackage/SqlPackage.exe", "/Working/tools/SqlPackage/SqlPackage.exe")]
            public void Should_Use_Sql_Package_Runner_From_Tool_Path_If_Provided(string toolPath, string expected)
            {
                // Given
                fixture.Settings.ToolPath = toolPath;
                fixture.GivenSettingsToolPathExist();

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal(expected, result.Path.FullPath);
            }

            [Fact]
            public void Should_Find_Sql_Package_Runner_If_Tool_Path_Not_Provided()
            {
                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working/tools/SqlPackage.exe", result.Path.FullPath);
            }

            [Fact]
            public void Should_Set_Working_Directory()
            {
                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Working", result.Process.WorkingDirectory.FullPath);
            }

            [Fact]
            public void Should_Throw_If_Process_Was_Not_Started()
            {
                // Given
                fixture.GivenProcessCannotStart();

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("SqlPackage: Process was not started.", result?.Message);
            }

            [Fact]
            public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
            {
                // Given
                fixture.GivenProcessExitsWithCode(1);

                // When
                var result = Record.Exception(() => fixture.Run());

                // Then
                Assert.IsType<CakeException>(result);
                Assert.Equal("SqlPackage: Process returned an error (exit code 1).", result?.Message);
            }

            [Fact]
            public void Should_Add_Action_If_Provided()
            {
                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script", result.Args);
            }

            [Fact]
            public void Should_Add_OutputPath_If_Provided()
            {
                // Given
                fixture.Settings.OutputPath = "./artifacts";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /OutputPath:\"/Working/artifacts\"", result.Args);
            }

            [Fact]
            public void Should_Add_Overwrite_Files_If_Provided()
            {
                // Given
                fixture.Settings.OverwriteFiles = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /OverwriteFiles:True", result.Args);
            }

            [Fact]
            public void Should_Add_Profile_If_Provided()
            {
                // Given
                fixture.Settings.Profile = "./profile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /Profile:\"profile.pubxml\"", result.Args);
            }

            [Fact]
            public void Should_Add_Quiet_If_Provided()
            {
                // Given
                fixture.Settings.Quiet = true;

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /Quiet:True", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Quiet_If_Not_Provided()
            {
                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script", result.Args);
            }

            [Fact]
            public void Should_Add_Source_Connection_String_If_Provided()
            {
                // Given
                fixture.Settings.SourceConnectionString =
                    "Data Source=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /SourceConnectionString:\"Data Source=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Source_File_If_Source_Connection_Provided()
            {
                // Given
                fixture.Settings.SourceConnectionString =
                    "Data Source=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/SourceFile:", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Source_Properties_If_Source_Connection_Provided()
            {
                // Given
                fixture.Settings.SourceConnectionString =
                    "Data Source=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/SourceFile:", result.Args);
                Assert.DoesNotContain("/SourceDatabaseName:", result.Args);
                Assert.DoesNotContain("/SourceEncryptConnection:", result.Args);
                Assert.DoesNotContain("/SourcePassword:", result.Args);
                Assert.DoesNotContain("/SourceServerName:", result.Args);
                Assert.DoesNotContain("/SourceTimeout:", result.Args);
                Assert.DoesNotContain("/SourceTrustServerCertificate:", result.Args);
                Assert.DoesNotContain("/SourceUser:", result.Args);
            }

            [Fact]
            public void Should_Add_Source_File_If_Provided()
            {
                // Given
                fixture.Settings.SourceFile = "./sqlpublishprofile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /SourceFile:\"/Working/sqlpublishprofile.pubxml\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Source_Connection_If_Source_File_Provided()
            {
                // Given
                fixture.Settings.SourceFile = "./sqlpublishprofile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/SourceConnectionString:", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Source_Properties_If_Source_File_Provided()
            {
                // Given
                fixture.Settings.SourceFile = "./sqlpublishprofile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/SourceConnectionString:", result.Args);
                Assert.DoesNotContain("/SourceDatabaseName:", result.Args);
                Assert.DoesNotContain("/SourceEncryptConnection:", result.Args);
                Assert.DoesNotContain("/SourcePassword:", result.Args);
                Assert.DoesNotContain("/SourceServerName:", result.Args);
                Assert.DoesNotContain("/SourceTimeout:", result.Args);
                Assert.DoesNotContain("/SourceTrustServerCertificate:", result.Args);
                Assert.DoesNotContain("/SourceUser:", result.Args);
            }

            [Fact]
            public void Should_Add_Target_Connection_String_If_Provided()
            {
                // Given
                fixture.Settings.TargetConnectionString =
                    "Data Target=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /TargetConnectionString:\"Data Target=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Target_File_If_Target_Connection_Provided()
            {
                // Given
                fixture.Settings.TargetConnectionString =
                    "Data Target=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/TargetFile:", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Target_Properties_If_Target_Connection_Provided()
            {
                // Given
                fixture.Settings.TargetConnectionString =
                    "Data Target=(LocalDB)\\v11.0;AttachDbFileName=|DataDirectory|\\DatabaseFileName.mdf;InitialCatalog=DatabaseName;Integrated Security=True;MultipleActiveResultSets=True";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/TargetFile:", result.Args);
                Assert.DoesNotContain("/TargetDatabaseName:", result.Args);
                Assert.DoesNotContain("/TargetEncryptConnection:", result.Args);
                Assert.DoesNotContain("/TargetPassword:", result.Args);
                Assert.DoesNotContain("/TargetServerName:", result.Args);
                Assert.DoesNotContain("/TargetTimeout:", result.Args);
                Assert.DoesNotContain("/TargetTrustServerCertificate:", result.Args);
                Assert.DoesNotContain("/TargetUser:", result.Args);
            }

            [Fact]
            public void Should_Add_Target_File_If_Provided()
            {
                // Given
                fixture.Settings.TargetFile = "./sqlpublishprofile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.Equal("/Action:Script /TargetFile:\"/Working/sqlpublishprofile.pubxml\"", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Target_Connection_If_Target_File_Provided()
            {
                // Given
                fixture.Settings.TargetFile = "./sqlpublishprofile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/TargetConnectionString:", result.Args);
            }

            [Fact]
            public void Should_Not_Add_Most_Target_Properties_If_Target_File_Provided()
            {
                // Given
                fixture.Settings.TargetFile = "./sqlpublishprofile.pubxml";

                // When
                var result = fixture.Run();

                // Then
                Assert.DoesNotContain("/TargetConnectionString:", result.Args);
                Assert.DoesNotContain("/TargetEncryptConnection:", result.Args);
                Assert.DoesNotContain("/TargetPassword:", result.Args);
                Assert.DoesNotContain("/TargetServerName:", result.Args);
                Assert.DoesNotContain("/TargetTimeout:", result.Args);
                Assert.DoesNotContain("/TargetTrustServerCertificate:", result.Args);
                Assert.DoesNotContain("/TargetUser:", result.Args);
            }

            [Fact]
            public void Should_Still_Add_TargetDatabaseName_If_Target_File_Provided()
            {
                // Given
                fixture.Settings.TargetFile = "./sqlpublishprofile.pubxml";
                fixture.Settings.TargetDatabaseName = "mongoose";

                // When
                var result = fixture.Run();

                // Then
                Assert.Contains("/TargetDatabaseName:mongoose", result.Args);
            }

            [Theory]
            [InlineData("CommandTimeout", "120")]
            [InlineData("CreateNewDatabase", "True")]
            [InlineData("TableData", "schema_name.table_identifier")]
            [InlineData("ScriptDatabaseOptions", "True")]
            public void Should_Add_Properties_If_Provided(string key, string value)
            {
                // Given
                fixture.Settings.Properties.Add(key, value);

                // When
                var result = fixture.Run();

                //Then
                Assert.Equal($"/Action:Script /p:{key}={value}", result.Args);
            }

            [Fact]
            public void Should_Add_Properties_After_Settings_If_Provided()
            {
                // Given
                fixture.Settings.OverwriteFiles = true;
                fixture.Settings.Properties.Add("CommandTimeout", "120");

                // When
                var result = fixture.Run();

                //Then
                Assert.Equal($"/Action:Script /OverwriteFiles:True /p:CommandTimeout=120", result.Args);
            }

            [Theory]
            [InlineData("CommandTimeout", "120")]
            [InlineData("CreateNewDatabase", "True")]
            [InlineData("TableData", "schema_name.table_identifier")]
            [InlineData("ScriptDatabaseOptions", "True")]
            public void Should_Add_Variables_If_Provided(string key, string value)
            {
                // Given
                fixture.Settings.Variables.Add(key, value);

                // When
                var result = fixture.Run();

                //Then
                Assert.Equal($"/Action:Script /v:{key}={value}", result.Args);
            }

            [Fact]
            public void Should_Add_Variables_After_Settings_If_Provided()
            {
                // Given
                fixture.Settings.OverwriteFiles = true;
                fixture.Settings.Variables.Add("CommandTimeout", "120");

                // When
                var result = fixture.Run();

                //Then
                Assert.Equal($"/Action:Script /OverwriteFiles:True /v:CommandTimeout=120", result.Args);
            }

            [Fact]
            public void Should_Use_Process_Settings_If_Provided()
            {
                // Given
                fixture.ProcessSettings = new ProcessSettings {RedirectStandardOutput = true};

                // When
                var result = fixture.Run();

                //Then
                Assert.Equal(fixture.ProcessSettings, result.Process);
            }
        }
    }
}
