// INSTALL TOOLS
#tool "nuget:https://www.nuget.org/api/v2?package=GitVersion.CommandLine&version=3.6.2"

// ARGUMENTS
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Debug");

// GLOBAL VARIABLES
var sourcePath  = Directory("./src");
var solution = File("./Cake.SqlPackage.sln");

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
            UpdateAssemblyInfoFilePath = "./src/SolutionInfo.cs",
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
        };

        var testProject = File("./test/Cake.SqlPackage.Tests/Cake.SqlPackage.Tests.csproj");
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
            Configuration = "Release",
            OutputDirectory = "./artifacts/"
        };

        DotNetCorePack(solution, settings);
    });

// DEPENDENCIES
Task("Default")
    .IsDependentOn("Pack");

// EXECUTE
RunTarget(target);