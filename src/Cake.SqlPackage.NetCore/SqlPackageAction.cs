namespace Cake.SqlPackage
{
    /// <summary>
    /// Action to execute on the command line.
    /// </summary>
    public enum SqlPackageAction
    {
        Publish = 0,
        Extract = 1,
        DeployReport = 2,
        DriftReport = 3,
        Script = 4,
        Export = 5,
        Import = 6
    }
}