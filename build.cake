// INSTALL TOOLS
#tool "nuget:https://www.nuget.org/api/v2?package=GitVersion.CommandLine&version=3.6.2"
#tool "nuget:https://www.nuget.org/api/v2?package=xunit.runner.console&version=2.2.0"

// ARGUMENTS
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

// GLOBAL VARIABLES
var sourcePath  = Directory("./src");
var solution = File("./Cake.SqlPackage.sln");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////
Setup(() =>
{
	Information("Target Cake Task: {0}", target);
});

Teardown(() => 
{
	Information("Target Cake Task: {0}", target);
    Information("Build Completion Time: {0}", DateTime.Now.TimeOfDay);
});

// TASKS
Task("Restore")
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

        var testProject = File("./test/Cake.SqlPackage.Tests/");
        DotNetCoreTest(testProject, settings);
    });

Task("Pack")
    .IsDependentOn("Build")
    .IsDependentOn("Unit-Tests")
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

// DEPENDENCIES
Task("Default")
    .IsDependentOn("Pack");

// EXECUTE
RunTarget(target);