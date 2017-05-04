namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageExportSettings" />.
    /// </summary>
    /// <seealso cref="SqlPackageSettings" />
    public class SqlPackageExportSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageExtractSettings"/> class.
        /// </summary>
        public SqlPackageExportSettings()
        {
            _action = SqlPackageAction.Export;
        }
    }
}
