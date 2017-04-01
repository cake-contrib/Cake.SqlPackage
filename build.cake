////////////////////////////////////
// INSTALL TOOLS
////////////////////////////////////
#tool "nuget:https://www.nuget.org/api/v2?package=GitVersion.CommandLine&version=3.6.5"

////////////////////////////////////
// ARGUMENTS
////////////////////////////////////
var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");

////////////////////////////////////
// GLOBAL VARIABLES
////////////////////////////////////
var sourcePath  = Directory("./src");
var solution = File("./Cake.SqlPackage.sln");
var appVeyor = AppVeyor.IsRunningOnAppVeyor;
var cleanDirectories = new string []{"./artifacts"};

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
    Information("Build Completion Time: {0}", DateTime.Now.TimeOfDay);
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
		CleanDirectories(directory);
	}
});

Task("Restore")
	.IsDependentOn("Clean")
    .Does(() =>
    {
        var settings =  new DotNetCoreRestoreSettings
        {
            Verbose = false,
            Sources = new [] 
            {
                "https://api.nuget.org/v3/index.json",
            }
        };

        DotNetCoreRestore(solution, settings);
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
        var projects = GetFiles("./**/project.json");
        var settings = new DotNetCoreBuildSettings 
        {
            Configuration = configuration,
            Verbose = false
        };

        DotNetCoreBuild(solution, settings);
    });

Task("Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./test/**/project.json");
        var settings = new DotNetCoreTestSettings 
        {
            Configuration = configuration,
            NoBuild = true
        };

        DotNetCoreTest("./test/**/", settings);
    });

Task("Pack")
    .IsDependentOn("Assembly")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./**/project.json");
        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            OutputDirectory = "./artifacts/"
        };

        DotNetCorePack(solution, settings);
    });

Task("MyGet")
    .IsDependentOn("Assembly")
    .IsDependentOn("Build")
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
        var packages = GetFiles("./artifacts/nuget/*.nupkg");

        foreach(var package in packages)
        {
            NuGetPush(package, new NuGetPushSettings {
                Source = apiUrl,
                ApiKey = apiKey
            });
        }
    })
    .OnError(exception =>
    {
        Error(exception.Message);
        Information("Publish-MyGet Task failed, but continuing with next Task...");
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