#load nuget:?package=Cake.Recipe&version=1.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            title: "Cake.SqlPackage",
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            testDirectoryPath: "./test",
                            testFilePattern: "/**/*UnitTests.csproj",
                            solutionFilePath: "./Cake.SqlPackage.sln",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.SqlPackage",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunDupFinder: false,
                            shouldRunCodecov: false,
                            shouldRunInspectCode: false,
                            shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.SqlPackage.UnitTests/*.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[FakeItEasy]*",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
