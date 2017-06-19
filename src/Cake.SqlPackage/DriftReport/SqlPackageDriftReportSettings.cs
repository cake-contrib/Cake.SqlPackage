namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageDriftReportSettings" />.
    /// </summary>
    public class SqlPackageDriftReportSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageDriftReportSettings"/> class.
        /// </summary>
        public SqlPackageDriftReportSettings()
        {
            _action = SqlPackageAction.DriftReport;
        }
    }
}
