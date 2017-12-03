#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            title: "Cake.SqlPackage",
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            testDirectoryPath: "./test",
							testFilePattern: "/**/*UnitTests.csproj",
							solutionFilePath: "./Cake.SqlPackage.sln",
                            repositoryOwner: "RLittlesII",
                            repositoryName: "Cake.SqlPackage",
                            appVeyorAccountName: "RLittlesII",
                            shouldRunDupFinder: false,
							shouldRunCodecov: false,
							shouldRunInspectCode: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.SqlPackage.UnitTests/*.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[FakeItEasy]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();