namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageImportSettings" />.
    /// </summary>
    /// <seealso cref="SqlPackageSettings" />
    public class SqlPackageImportSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageImportSettings"/> class.
        /// </summary>
        public SqlPackageImportSettings()
        {
            _action = SqlPackageAction.Import;
        }
    }
}
