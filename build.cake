////////////////////////////////////
// INSTALL TOOLS
////////////////////////////////////
#tool "nuget:https://www.nuget.org/api/v2?package=GitVersion.CommandLine&version=3.6.5"
#tool "nuget:https://www.nuget.org/api/v2?package=xunit.runner.console&version=2.2.0"

#load "version.cake"

////////////////////////////////////
// ARGUMENTS
////////////////////////////////////
var target = Argument("target", "Pack");
var configuration = Argument("configuration", "Release");

////////////////////////////////////
// GLOBAL VARIABLES
////////////////////////////////////
var sourcePath  = Directory("./src");
var artifacts = Directory("./artifacts");
var artifactNet452 = Directory("./artifacts/net452");
var solutionPath = File("./Cake.SqlPackage.sln");
var solution = ParseSolution(solutionPath);
var projects = solution.Projects;
var projectPaths = projects.Select(p => p.Path.GetDirectory());
var testAssemblies = projects.Where(p => p.Name.Contains(".UnitTests")).Select(p => p.Path.GetDirectory() + "/bin/" + configuration + "/" + p.Name + ".dll");
var appVeyor = AppVeyor.IsRunningOnAppVeyor;
var nupkgDirectory = Directory("./artifacts/nuget");
var csProject = File("./src/Cake.SqlPackage/Cake.SqlPackage.csproj");
var testResultsPath = MakeAbsolute(Directory(artifacts.Path.FullPath + "./test-results"));
var cleanDirectories = new DirectoryPath []{ artifacts, artifactNet452 };
GitVersion versionInfo = null;

////////////////////////////////////
// SETUP / TEARDOWN
////////////////////////////////////
Setup(context =>
{
    versionInfo = GitVersion();
    Information("Target Cake Task: {0}", target);
    Information("Build Version: {0}", versionInfo.FullSemVer);
});

Teardown(context => 
{
	Information("Target Cake Task: {0}", target);
    Information("Build Version: {0}", versionInfo.FullSemVer);
    Information("Utc Completion Time: {0}", DateTime.UtcNow);
});

////////////////////////////////////
// TASKS
////////////////////////////////////
Task("Clean")
    .Does(() =>
    {
        // Clean solutionPath directories.
        foreach(var directory in cleanDirectories)
        {
            var fullPath = MakeAbsolute(directory);
            Information("{0}", fullPath.FullPath);
            CleanDirectories(fullPath.FullPath);
        }
    });

Task("Restore")
	.IsDependentOn("Clean")
    .Does(() =>
    {
        NuGetRestore(solutionPath);
    });

Task("Version")
	.Does(() =>
	{
		string version = null;
		string semVersion = null;
		string milestone = null;
		string informationalVersion = null;
		string nugetVersion = null;

		if(IsRunningOnWindows())
		{
			if(AppVeyor.IsRunningOnAppVeyor)
			{
				GitVersion(new GitVersionSettings
				{
					UpdateAssemblyInfoFilePath = "../SolutionInfo.cs",
					UpdateAssemblyInfo = true,
					OutputType = GitVersionOutput.BuildServer
                });

				version = EnvironmentVariable("GitVersion_MajorMinorPatch");
				semVersion = EnvironmentVariable("GitVersion_LegacySemVerPadded");
				informationalVersion = EnvironmentVariable("GitVersion_InformationalVersion");
				milestone = string.Concat(version);
				nugetVersion = EnvironmentVariable("GitVersion_NuGetVersion");
			}

			GitVersion assertedVersions = GitVersion(new GitVersionSettings
			{
				UpdateAssemblyInfoFilePath = "SolutionInfo.cs",
				UpdateAssemblyInfo = true,
				OutputType = GitVersionOutput.Json,
			});

			version = assertedVersions.MajorMinorPatch;
			semVersion = assertedVersions.LegacySemVerPadded;
			informationalVersion = assertedVersions.InformationalVersion;
			milestone = string.Concat(version);
			nugetVersion = assertedVersions.NuGetVersion;
		}

		BuildVersion.SetVersion(version, semVersion, milestone, informationalVersion, nugetVersion);
	});

Task("Build")
	.IsDependentOn("Restore")
    .IsDependentOn("Version")
    .Does(() =>
    {
        MSBuild(solutionPath, settings =>
            settings.SetPlatformTarget(PlatformTarget.MSIL)
                .WithProperty("TreatWarningsAsErrors","true")
                .SetVerbosity(Verbosity.Quiet)
                .WithTarget("Build")
                .SetConfiguration(configuration));
    });

Task("Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
    {

        EnsureDirectoryExists(testResultsPath);

        var settings = new XUnit2Settings 
        {
            NoAppDomain = true,
            XmlReport = true,
            HtmlReport = true,
            OutputDirectory = testResultsPath,
        };

        XUnit2(testAssemblies, settings);
    });

Task("Copy-Files")
	.IsDependentOn("Build")
	.Does(() =>
    {
        EnsureDirectoryExists(artifactNet452);
        
        foreach (var project in projects.Where(x => x.Name.Contains("Cake.SqlPackage") && !x.Name.Contains("Test")))
        {
            var files = GetFiles(project.Path.GetDirectory() + "/bin/" + configuration + "/Cake*.*");
            CopyFiles(files, artifactNet452);
        }
    });

Task("Pack")
    .IsDependentOn("Build")
    .IsDependentOn("Unit-Tests")
    .IsDependentOn("Copy-Files")
    .Does(() =>
    {
        EnsureDirectoryExists(artifactNet452);

        // .NET 4.5
        NuGetPack("./.nuspec/Cake.SqlPackage.nuspec", new NuGetPackSettings {
            Version = BuildVersion.NuGetVersion,
            BasePath = artifactNet452,
            OutputDirectory = nupkgDirectory.Path.FullPath,
            Symbols = false,
            NoPackageAnalysis = true
        });
    });

Task("MyGet")
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
        var packages = GetFiles(nupkgDirectory.Path.FullPath + "/*.nupkg");

        foreach(var package in packages)
        {
			Information("{0}", package);

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