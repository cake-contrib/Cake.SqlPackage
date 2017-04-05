////////////////////////////////////
// INSTALL TOOLS
////////////////////////////////////
#tool "nuget:https://www.nuget.org/api/v2?package=SqlPackage.CommandLine&version=13.0.3485.2"
#tool "nuget:https://www.nuget.org/api/v2?package=Microsoft.Data.Tools.Msbuild"

#addin "nuget:https://www.myget.org/F/cake-sqlpackage/api/v2?package=Cake.SqlPackage&version=0.1.0"

var target = Argument("target", "Deploy");
var configuration = Argument("configuration", "Release");

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
            Action = SqlPackageAction.Publish,
            SourceFile = File("./CoffeeHouse/bin/" + configuration + "/CoffeHouse.dacpac"),
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