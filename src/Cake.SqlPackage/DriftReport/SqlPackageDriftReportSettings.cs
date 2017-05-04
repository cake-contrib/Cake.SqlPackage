namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageDriftReportSettings" />.
    /// </summary>
    /// <seealso cref="SqlPackageSettings" />
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
