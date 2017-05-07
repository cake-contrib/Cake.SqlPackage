namespace Cake.SqlPackage
{
    /// <summary>
    /// Action to execute on the command line.
    /// </summary>
    public enum SqlPackageAction
    {
        /// <summary>
        /// Incrementally updates a database schema to match the schema of a source .dacpac file.
        /// </summary>
        Publish = 0,

        /// <summary>
        /// Creates a database snapshot (.dacpac) file from a live SQL Server or Microsoft Azure SQL Database.
        /// </summary>
        Extract = 1,

        /// <summary>
        /// Creates an XML report of the changes that would be made by a publish action.
        /// </summary>
        DeployReport = 2,

        /// <summary>
        /// Creates an XML report of the changes that have been made to a registered database since it was last registered.
        /// </summary>
        DriftReport = 3,

        /// <summary>
        /// Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source.
        /// </summary>
        Script = 4,

        /// <summary>
        /// Exports a live database to a .bacpac file.
        /// </summary>
        Export = 5,

        /// <summary>
        /// Imports the schema and table data from a BACPAC package into a new user database.
        /// </summary>
        Import = 6
    }
}