#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            title: "Cake.SqlPackage",
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            testDirectoryPath: "./test",
							testFilePattern: "/**/*UnitTests.dll",
							solutionFilePath: "./Cake.SqlPackage.sln",
                            repositoryOwner: "RLittlesII",
                            repositoryName: "Cake.SqlPackage",
                            appVeyorAccountName: "RLittlesII",
                            shouldPostToGitter: false,
                            shouldPostToSlack: false,
                            shouldRunDupFinder: false);

BuildParameters.PrintParameters(Context);

Build.Run();