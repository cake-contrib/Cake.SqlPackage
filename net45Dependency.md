# Building a nuget for .NET Framework v4.5 on dotnet core

``` powershell
    INFO: Could not find files for the given pattern(s).
Feeds used:
  C:\Users\rlittles\.nuget\packages\
  https://www.myget.org/F/cake/api/v3/index.json
  C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\
  C:\Program Files\Microsoft SDKs\Service Fabric\packages\

Restoring NuGet package Cake.0.19.3.
Adding package 'Cake.0.19.3' to folder 'C:\Users\rlittles\Source\github\Cake.SqlPackage\example\tools'
Added package 'Cake.0.19.3' to folder 'C:\Users\rlittles\Source\github\Cake.SqlPackage\example\tools'
Analyzing build script...
Processing build script...
Installing tools...
Installing addins...
Unable to resolve dependency 'Cake.Core'. Source(s) used: 'MyGet', 'Microsoft Visual Studio Offline Packages', 'Microsoft Azure Service Fabric SDK'.
NuGet exited with 1
Could not find any assemblies compatible with .NETFramework,Version=v4.5.
Error: Failed to install addin 'Cake.SqlPackage'.
```

I tried to figure out what I was doing wrong.  I mean at this point I have published the dependencies, I have packaged said dependencies.  I have targeted a framework on build...or have I?

... Looking at the [MSBuild docs](https://docs.microsoft.com/en-us/visualstudio/msbuild/what-s-new-in-msbuild-15-0) I realized I haven't yet included cake into the new csproject as a real dependency for output?... at least that's what I think.

So now my .csproject file looks like this

``` xml
  <ItemGroup>
    <PackageReference Include="Cake.Core" Version="0.19.3" />
    <PackageReference Include="NETStandard.Library" Version="1.6.0" />
    <PackageReference Include=".NET Framework" Version="4.5" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Update="NETStandard.Library" Version="1.6.0" />
    <PackageReference Update=".NET Framework" Version="4.5" />
  </ItemGroup>
```

Which resulted in this error

``` powershell
  C:\Program Files\dotnet\sdk\1.0.0\NuGet.targets(97,5): error : Unable to resolve '.NET Framework (>= 4.5.0)' for '.NETCoreApp,Version=v1.0'. [C:\Users\rlittles\Source\github\Cake.SqlPackage\Cake.SqlPackage.sln]
```

So I flexed my google fu and found a blog post [Converting From project.json To csproj](https://dotnetcore.gaprogman.com/2017/03/09/converting-from-project-json-to-csproj/)

So now I look like.

``` xml
  <ItemGroup>
    <PackageReference Include="Cake.Core" Version="0.19.3" />
  </ItemGroup>
  <ItemGroup>
    <Reference Include="System.Data" />
    <Reference Include="System.Deployment" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Windows.Forms" />
    <Reference Include="System.Xml" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Update="NETStandard.Library" Version="1.6.0" />
    <PackageReference Update=".NET Framework" Version="4.5" />
  </ItemGroup>
```

Green lights on build ...

Attempting again ...

```xml
Error: Cake.Core.CakeException: Failed to install addin 'Cake.SqlPackage'.
   at Cake.Core.Scripting.ScriptProcessor.InstallAddins(ScriptAnalyzerResult analyzerResult, DirectoryPath installPath)
   at Cake.Core.Scripting.ScriptRunner.Run(IScriptHost host, FilePath scriptPath, IDictionary`2 arguments)
   at Cake.Commands.BuildCommand.Execute(CakeOptions options)
   at Cake.CakeApplication.Run(CakeOptions options)
   at Cake.Program.Main()
```

#### ACCESS DENIED