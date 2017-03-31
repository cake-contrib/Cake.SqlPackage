namespace Cake.SqlPackage
{
    /// <summary>
    /// Action to execute on the command line.
    /// </summary>
    public enum SqlPackageAction
    {
        Extract,
        DeployReport,
        DriftReport,
        Publish,
        Script,
        Export,
        Import
    }
}