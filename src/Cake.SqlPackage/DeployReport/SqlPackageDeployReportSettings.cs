namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageDeployReportSettings" />.
    /// </summary>
    /// <seealso cref="SqlPackageSettings" />
    public class SqlPackageDeployReportSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageDeployReportSettings"/> class.
        /// </summary>
        public SqlPackageDeployReportSettings()
        {
            _action = SqlPackageAction.DeployReport;
        }
    }
}
