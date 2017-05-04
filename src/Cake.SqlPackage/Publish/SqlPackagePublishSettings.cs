namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains settings used by <see cref="SqlPackagePublishSettings" />.
    /// </summary>
    /// <seealso cref="Cake.SqlPackage.SqlPackageSettings" />
    public class SqlPackagePublishSettings : SqlPackageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackagePublishSettings"/> class.
        /// </summary>
        public SqlPackagePublishSettings()
        {
            _action = SqlPackageAction.Publish;
        }

        /// <summary>
        /// Specifies the Client Secret to be used in authenticating against Azure Key Vault.
        /// </summary>
        public string AzureSecret { get; set; }

        /// <summary>
        /// Specifies what authentication method will be used for accessing Azure Key Vault.
        /// </summary>
        public AzureKeyVaultAuthMethod? AzureKeyVaultAuthMethod { get; set; }

        /// <summary>
        /// Specifies the Client ID to be used in authenticating against Azure Key Vault.
        /// </summary>
        public string ClientId { get; set; }
    }
}
