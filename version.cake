public static class BuildVersion
{
    public static string Version { get; private set; }
    public static string SemVersion { get; private set; }
    public static string Milestone { get; private set; }
    public static string InformationalVersion { get; private set; }
    public static string NuGetVersion { get; private set; }

    public static void SetVersion(
        string version,
        string semVersion, 
        string milestone, 
        string informationalVersion,
		string nugetVersion)
        {
            Version = version;
            SemVersion = semVersion;
            Milestone = milestone;
            InformationalVersion = informationalVersion;
			NuGetVersion = nugetVersion;
        }
}