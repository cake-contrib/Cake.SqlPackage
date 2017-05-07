////////////////////////////////////
// INSTALL TOOLS
////////////////////////////////////
#tool "nuget:https://www.nuget.org/api/v2?package=Microsoft.Data.Tools.Msbuild&version=10.0.61026"

////////////////////////////////////
// INSTALL ADDINS
////////////////////////////////////
#addin "nuget:https://www.myget.org/F/cake-sqlpackage/api/v2?package=Cake.SqlPackage&version=0.1.1"

var target = Argument("target", "Deploy");
var configuration = Argument("configuration", "Release");

var dacpac = File("./CoffeeHouse/bin/" + configuration + "/CoffeeHouse.dacpac");
var bacpac = File("./export/CoffeeHouse.bacpac");
var publishProfile = File("./CoffeeHouse/publish/CoffeeHouse.publish.xml");
var connection = "Data Source=YOUR_SERVER;Initial Catalog=CoffeeHouse;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";

////////////////////////////////////
// SETUP/TEAR DOWN
////////////////////////////////////
Setup(context =>
{
	Information("Target Cake Task: {0}", target);
});

Teardown(context => 
{
	Information("Target Cake Task: {0}", target);
    Information("Utc Completion Time: {0}", DateTime.UtcNow);
});

////////////////////////////////////
// TASKS
////////////////////////////////////
Task("Build")
	.Does(() =>
	{
        MSBuild("./CoffeeHouse.sln", settings =>
            settings.UseToolVersion(MSBuildToolVersion.VS2015)
				.SetPlatformTarget(PlatformTarget.MSIL)
                .WithProperty("TreatWarningsAsErrors","true")
                .SetVerbosity(Verbosity.Quiet)
                .WithTarget("Build")
                .SetConfiguration(configuration));
	});

Task("Export")
    .IsDependentOn("Build")
    .Does(() =>
    {
        EnsureDirectoryExists("./export");

        SqlPackageExport(settings =>
        {
            settings.SourceConnectionString = connection;
            settings.Profile = publishProfile;
            settings.TargetFile = bacpac;
        });
    });

Task("Import")
    .IsDependentOn("Build")
    .Does(() =>
    {
        SqlPackageImport(settings => 
        {
            settings.SourceFile = bacpac;
            settings.TargetConnectionString = connection;
        });
    });

Task("Script")
    .IsDependentOn("Build")
    .Does(() =>
    {
        EnsureDirectoryExists("./scripts");

        SqlPackageScript(settings => 
        {
            settings.SourceFile = dacpac;
            settings.Profile = publishProfile;
            settings.OutputPath = File("./scripts/CoffeeHouse.sql");
        });
    });

Task("Publish")
	.IsDependentOn("Build")
    .Does(() =>
    {
        SqlPackagePublish(settings => 
        {
            settings.SourceFile = dacpac;
            settings.Profile = publishProfile;
        });
    });

////////////////////////////////////
// DEPENDENCIES
////////////////////////////////////
Task("Default")
    .IsDependentOn("Script")
    .IsDependentOn("Publish")
    .IsDependentOn("Export");

////////////////////////////////////
// EXECUTE
////////////////////////////////////
RunTarget(target);