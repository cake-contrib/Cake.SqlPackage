namespace Cake.SqlPackage
{
    /// <summary>
    /// Contains properties specific to the Extract action.
    /// </summary>
    public class SqlPackageExtractProperties : SqlPackageProperties
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageExtractProperties"/> class.
        /// </summary>
        public SqlPackageExtractProperties()
        {
            DacMajorVersion = 1;
            DacMinorVersion = 0;
            ExtractReferencedServerScopedElements = true;
            ExtractApplicationScopedObjectsOnly = true;
            IgnorePermissions = true;
        }

        /// <summary>
        /// Defines the Application description to be stored in the DACPAC metadata
        /// </summary>
        public string DacApplicationDescription { get; set; }

        /// <summary>
        /// Defined the Application name to be stored in the DACPAC metadata. The default value is the database name.
        /// </summary>
        public string DacApplicationName { get; set; }

        /// <summary>
        /// Defines the major version to be stored in the DACPAC metadata.
        /// </summary>
        public int DacMajorVersion { get; set; }

        /// <summary>
        /// Defines the minor version to be stored in the DACPAC metadata.
        /// </summary>
        public int DacMinorVersion { get; set; }

        /// <summary>
        /// Indicates whether data from all user tables is extracted.
        /// </summary>
        public bool? ExtractAllTableData { get; set; }

        /// <summary>
        /// If true, only extract application-scoped objects for the specified source. If false, extract all objects for the specified source.
        /// </summary>
        public bool ExtractApplicationScopedObjectsOnly { get; set; }

        /// <summary>
        /// If true, extract login, server audit and credential objects referenced by source database objects.
        /// </summary>
        public bool ExtractReferencedServerScopedElements { get; set; }

        /// <summary>
        /// Specifies whether usage properties, such as table row count and index size, will be extracted from the database
        /// </summary>
        public bool? ExtractUsageProperties { get; set; }

        /// <summary>
        /// Specifies whether extended properties should be ignored.
        /// </summary>
        public bool? IgnoreExtendedProperties { get; set; }

        /// <summary>
        /// Specifies the type of backing storage for the schema model used during extraction.
        /// </summary>
        public ExtractStorageType Storage { get; set; }

        /// <summary>
        /// Gets or sets the table data.
        /// </summary>
        public string TableData { get; set; }

        /// <summary>
        /// Specifies whether the extracted dacpac should be verified.
        /// </summary>
        public bool? VerifyExtraction { get; set; }
    }

    /// <summary>
    /// The type of backing storage for the schema model used during extraction.
    /// </summary>
    public enum ExtractStorageType
    {
        /// <summary>
        /// Use file backing storage during extraction.
        /// </summary>
        File = 0,

        /// <summary>
        /// Use memory backing storage during extraction.
        /// </summary>
        Memory = 1
    }
}
