////////////////////////////////////
// INSTALL TOOLS
////////////////////////////////////
#tool "nuget:https://www.nuget.org/api/v2?package=Microsoft.Data.Tools.Msbuild&version=10.0.61026"

////////////////////////////////////
// INSTALL ADDINS
////////////////////////////////////
#addin "nuget:https://www.nuget.org/api/v2?package=Cake.SqlPackage&version=0.1.0"

var target = Argument("target", "Deploy");
var configuration = Argument("configuration", "Release");

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

Task("Sql-Package")
	.IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new SqlPackageSettings
        {
            SourceFile = File("./CoffeeHouse/bin/" + configuration + "/CoffeeHouse.dacpac"),
            Profile = File("./CoffeeHouse/publish/CoffeeHouse.publish.xml")
        };

        SqlPackage(settings);
    });

////////////////////////////////////
// DEPENDENCIES
////////////////////////////////////
Task("Default")
    .IsDependentOn("Sql-Package");

////////////////////////////////////
// EXECUTE
////////////////////////////////////
RunTarget(target);