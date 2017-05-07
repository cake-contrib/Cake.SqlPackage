using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.SqlPackage
{
    /// <summary>
    /// <para>Contains functionality related to <see href="https://github.com/dotnet/cli">SqlPackage</see>.</para>
    /// <para>
    /// In order to use the commands for this alias, the SqlPackage tool will need to be installed on the machine where
    /// the Cake script is being executed.
    /// </para>
    /// </summary>
    [CakeAliasCategory("SqlPackage")]
    public static class SqlPackageAliases
    {
        /// <summary>
        /// Creates a database snapshot (.dacpac) file from SQL Server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     var settings = new SqlPackageExtractSettings();
        ///
        ///     SqlPackageExtract(settings);
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Extract")]
        public static void SqlPackageExtract(this ICakeContext context, SqlPackageExtractSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageExtractRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Extract(settings ?? new SqlPackageExtractSettings());
        }

        /// <summary>
        /// Creates a database snapshot (.dacpac) file from SQL Server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageExtract(settings => 
        ///     {
        ///         settings.Quiet = true;
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Extract")]
        public static void SqlPackageExtract(this ICakeContext context, Action<SqlPackageExtractSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackageExtractSettings();
            configurationAction(settings);

            var runner = new SqlPackageExtractRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Extract(settings);
        }

        /// <summary>
        /// Incrementally updates a database schema to match the schema of a source .dacpac file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     var settings = new SqlPackagePublishSettings();
        ///
        ///     SqlPackagePublish(settings);
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Publish")]
        public static void SqlPackagePublish(this ICakeContext context, SqlPackagePublishSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackagePublishRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Publish(settings ?? new SqlPackagePublishSettings());
        }

        /// <summary>
        /// Incrementally updates a database schema to match the schema of a source .dacpac file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackagePublish(settings => 
        ///     {            
        ///         settings.SourceFile = File("database.dacpac");
        ///         settings.Profile = File("./profile.publish.xml");
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Publish")]
        public static void SqlPackagePublish(this ICakeContext context, Action<SqlPackagePublishSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackagePublishSettings();
            configurationAction(settings);

            var runner = new SqlPackagePublishRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Publish(settings);
        }

        /// <summary>
        /// Exports a live database to a BACPAC package (.bacpac file).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     var settings = new SqlPackageExportSettings
        ///     {
        ///         SourceConnectionString = connectionString,
        ///         Profile = File("./profile.publish.xml")
        ///         TargetFile = File("database.bacpac")
        ///     };
        ///
        ///     SqlPackageExport(settings);
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Export")]
        public static void SqlPackageExport(this ICakeContext context, SqlPackageExportSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageExportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Export(settings ?? new SqlPackageExportSettings());
        }

        /// <summary>
        /// Exports a live database to a BACPAC package (.bacpac file).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageExport(settings =>
        ///     {
        ///         settings.SourceConnectionString = connectionString;
        ///         settings.Profile = File("./profile.publish.xml");
        ///         settings.TargetFile = File("database.bacpac");
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Export")]
        public static void SqlPackageExport(this ICakeContext context, Action<SqlPackageExportSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackageExportSettings();
            configurationAction(settings);

            var runner = new SqlPackageExportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Export(settings);
        }

        /// <summary>
        /// Imports the schema and table data from a BACPAC package into a new user database
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     var settings = new SqlPackageImportSettings
        ///     {
        ///         SourceFile = File("database.bacpac")
        ///         TargetConnectionString = connectionString
        ///     };
        ///
        ///     SqlPackageImport(settings);
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Import")]
        public static void SqlPackageImport(this ICakeContext context, SqlPackageImportSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageImportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Import(settings ?? new SqlPackageImportSettings());
        }

        /// <summary>
        /// Imports the schema and table data from a BACPAC package into a new user database
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageImport(settings => 
        ///     {
        ///         settings.SourceFile = File("database.bacpac");
        ///         settings.TargetConnectionString = connectionString;
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Import")]
        public static void SqlPackageImport(this ICakeContext context, Action<SqlPackageImportSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackageImportSettings();
            configurationAction(settings);

            var runner = new SqlPackageImportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Import(settings);
        }

        /// <summary>
        /// Creates an XML report of the changes that would be made by a publish action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     var settings = new SqlPackageDeployReportSettings();
        ///
        ///     SqlPackageDeployReport(settings);
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("DeployReport")]
        public static void SqlPackageDeployReport(this ICakeContext context, SqlPackageDeployReportSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageDeployReportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.DeployReport(settings ?? new SqlPackageDeployReportSettings());
        }

        /// <summary>
        /// Creates an XML report of the changes that would be made by a publish action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageDeployReport(settings => 
        ///     {
        ///         settings.Quiet = true;
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("DeployReport")]
        public static void SqlPackageDeployReport(this ICakeContext context, Action<SqlPackageDeployReportSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackageDeployReportSettings();
            configurationAction(settings);

            var runner = new SqlPackageDeployReportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.DeployReport(settings);
        }

        /// <summary>
        /// Creates an XML report of the changes that have been made to a registered database since it was last registered.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     var settings = new SqlPackageDriftReportSettings();
        ///
        ///     SqlPackageDriftReport(settings);
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("DriftReport")]
        public static void SqlPackageDriftReport(this ICakeContext context, SqlPackageDriftReportSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageDriftReportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.DriftReport(settings ?? new SqlPackageDriftReportSettings());
        }

        /// <summary>
        /// Creates an XML report of the changes that have been made to a registered database since it was last registered.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageDriftReport(settings => 
        ///     {
        ///         settings.Quiet = true;
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("DriftReport")]
        public static void SqlPackageDriftReport(this ICakeContext context, Action<SqlPackageDriftReportSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackageDriftReportSettings();
            configurationAction(settings);

            var runner = new SqlPackageDriftReportRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.DriftReport(settings);
        }

        /// <summary>
        /// Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageScript(settings => 
        ///     {
        ///        settings.SourceFile = File("./CoffeeHouse/bin/" + configuration + "/CoffeeHouse.dacpac");
        ///        settings.Profile = File("./CoffeeHouse/publish/CoffeeHouse.publish.xml");
        ///        settings.OutputPath = File("./scripts/CoffeeHouse.sql");
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Script")]
        public static void SqlPackageScript(this ICakeContext context, SqlPackageScriptSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageScriptRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Script(settings ?? new SqlPackageScriptSettings());
        }

        /// <summary>
        /// Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="configurationAction">The configuration action.</param>
        /// <example>
        ///   <code>
        ///     SqlPackageScript(settings => 
        ///     {
        ///         settings.SourceFile = File("./CoffeeHouse/bin/" + configuration + "/CoffeeHouse.dacpac");
        ///         settings.Profile = File("./CoffeeHouse/publish/CoffeeHouse.publish.xml");
        ///         settings.OutputPath = File("./scripts/CoffeeHouse.sql");
        ///     });
        ///   </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Script")]
        public static void SqlPackageScript(this ICakeContext context, Action<SqlPackageScriptSettings> configurationAction)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (configurationAction == null)
            {
                throw new ArgumentNullException(nameof(configurationAction));
            }

            var settings = new SqlPackageScriptSettings();
            configurationAction(settings);

            var runner = new SqlPackageScriptRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Script(settings);
        }
    }
}
