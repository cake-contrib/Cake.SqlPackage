namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackageScriptSettings" />.
    /// </summary>
    public class SqlPackageScriptSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageScriptSettings"/> class.
        /// </summary>
        public SqlPackageScriptSettings()
        {
            _action = SqlPackageAction.Script;
        }
    }
}
