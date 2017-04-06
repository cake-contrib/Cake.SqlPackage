////////////////////////////////////
// INSTALL TOOLS
////////////////////////////////////
#tool "nuget:https://www.nuget.org/api/v2?package=xunit.runner.console&version=2.1.0"
#tool "nuget:https://www.nuget.org/api/v2?package=GitVersion.CommandLine&version=3.6.5"

////////////////////////////////////
// ARGUMENTS
////////////////////////////////////
var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");

////////////////////////////////////
// GLOBAL VARIABLES
////////////////////////////////////
var appVeyor = AppVeyor.IsRunningOnAppVeyor;
var solution = File("./Cake.SqlPackage.sln");
var csprojFile = File ("./src/Cake.SqlPackage/Cake.SqlPackage.csproj");
var unitTestFile = File("./src/Cake.SqlPackage/Cake.SqlPackage.csproj");
var sourcePath = Directory("./src");
var nupkgDirectory = Directory("./artifacts/nuget");
var testArtifacts = Directory("./artifacts/tests");
var nupkgFiles = nupkgDirectory.Path.FullPath + "/*.nupkg";
var cleanDirectories = new DirectoryPath[] { Directory("./artifacts") };

////////////////////////////////////
// SETUP / TEARDOWN
////////////////////////////////////
Setup(context =>
{
	Information("Target Cake Task: {0}", target);
});

Teardown(context => 
{
	Information("Target Cake Task: {0}", target);
    Information("Build Completion Time: {0}", DateTime.UtcNow.TimeOfDay);
});

////////////////////////////////////
// TASKS
////////////////////////////////////
Task("Clean")
    .Does(() =>
    {
        // Clean solution directories.
        foreach(var directory in cleanDirectories)
        {
            Information("{0}", directory);
            CleanDirectories(directory.FullPath);
        }
    });

Task("Restore")
	.IsDependentOn("Clean")
    .Does(() =>
    {
        var settings =  new DotNetCoreRestoreSettings
        {
            Sources = new [] 
            {
				"https://api.nuget.org/v3/index.json",
            }
        };

        DotNetCoreRestore(solution, settings);
    });

	Task("Nuget-Restore")
		.IsDependentOn("Clean")
		.Does(() =>
		{
			NuGetRestore(solution);
		});

Task("Assembly")
    .IsDependentOn("Restore")
    .Does(() => 
    {
        var gitVersionSettings = new GitVersionSettings 
        {
            UpdateAssemblyInfoFilePath = "./SolutionInfo.cs",
            UpdateAssemblyInfo = true
        };

         GitVersion(gitVersionSettings);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings 
        {
            Configuration = configuration
        };

        DotNetCoreBuild(solution, settings);
    });

Task("Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings 
        {
            Configuration = configuration
        };

        DotNetCoreTest(unitTestFile, settings);
    });

Task("xUnit")
    .IsDependentOn("Assembly")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var files = GetFiles("./test/**/bin/**/*.UnitTests.dll");

        EnsureDirectoryExists(testArtifacts);

        XUnit2(files, new XUnit2Settings 
        {
            HtmlReport=true,
            NoAppDomain = true,
            OutputDirectory = testArtifacts
        });
    });

Task("Pack")
    .IsDependentOn("Assembly")
    .IsDependentOn("Build")
    .IsDependentOn("xUnit")
    .Does(() =>
    {
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = nupkgDirectory
        };

        DotNetCorePack(csprojFile, settings);
    });

Task("MyGet")
	.IsDependentOn("Nuget-Restore")
    .IsDependentOn("Pack")
	.WithCriteria(appVeyor)
    .Does(() =>
    {
        // Retrieve the API key.
        var apiKey = EnvironmentVariable("MYGET_API_KEY");
        if(string.IsNullOrEmpty(apiKey)) 
        {
            throw new InvalidOperationException("Could not retrieve MyGet API key.");
        }

        // Resolve the API url.
        var apiUrl = EnvironmentVariable("MYGET_API_URL");
        if(string.IsNullOrEmpty(apiUrl)) 
        {
            throw new InvalidOperationException("Could not retrieve MyGet API url.");
        }

        // Push the package.
        var packages = GetFiles(nupkgFiles);

        foreach(var package in packages)
        {
			Information("{0}", package);

            NuGetPush(package, new NuGetPushSettings 
            {
                Source = apiUrl,
                ApiKey = apiKey
            });
        }
    })
    .OnError(exception =>
    {
        Error(exception.Message);
        Information("MyGet Task failed.");
    });

////////////////////////////////////
// DEPENDENCIES
////////////////////////////////////
Task("Default")
    .IsDependentOn("Pack");

Task("Deliver")
	.IsDependentOn("MyGet");

////////////////////////////////////
// EXECUTE
////////////////////////////////////
RunTarget(target);