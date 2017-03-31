namespace Cake.SqlPackage
{
    /// <summary>
    /// Enumeration for Azure key vault authentication method
    /// </summary>
    public enum AzureKeyVaultAuthMethod
    {
        /// <summary>
        /// Authentication using interactive.
        /// </summary>
        Interactive = 0,

        /// <summary>
        /// Authentication using client shared secret.
        /// </summary>
        ClientIdSecret = 1
    }
}