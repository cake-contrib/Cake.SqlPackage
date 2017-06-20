namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageExtractSettings" />.
    /// </summary>
    public class SqlPackageExtractSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageExtractSettings"/> class.
        /// </summary>
        public SqlPackageExtractSettings()
        {
            _action = SqlPackageAction.Extract;
        }
    }
}
