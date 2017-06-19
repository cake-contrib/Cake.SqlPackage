using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.SqlPackage
{
    /// <summary>
    /// SqlPackage tool execution for script action.
    /// </summary>
    internal class SqlPackageScriptRunner : SqlPackageRunner<SqlPackageScriptSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageScriptRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public SqlPackageScriptRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            Environment = environment;
        }

        /// <summary>
        /// Runs SqlPackage with the specified publish settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Script(SqlPackageScriptSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, BuildArguments(settings));
        }

        private ProcessArgumentBuilder BuildArguments(SqlPackageScriptSettings settings)
        {
            var builder = CreateBuilder(settings);

            if (settings.OutputPath != null)
            {
                builder.Append($"/{nameof(settings.OutputPath)}:\"{settings.OutputPath.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }

            if (settings.OverwriteFiles.HasValue)
            {
                builder.Append($"/{nameof(settings.OverwriteFiles)}:{settings.OverwriteFiles}");
            }

            if (settings.Profile != null)
            {
                builder.Append($"/{nameof(settings.Profile)}:\"{settings.Profile}\"");
            }

            if (settings.Quiet.HasValue)
            {
                builder.Append($"/{nameof(settings.Quiet)}:{settings.Quiet}");
            }

            if (!string.IsNullOrEmpty(settings.SourceConnectionString))
            {
                builder.Append($"/{nameof(settings.SourceConnectionString)}:\"{settings.SourceConnectionString}\"");
            }
            else if (settings.SourceFile != null)
            {
                builder.Append($"/{nameof(settings.SourceFile)}:\"{settings.SourceFile.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }
            else
            {
                if (!string.IsNullOrEmpty(settings.SourceDatabaseName))
                {
                    builder.Append($"/{nameof(settings.SourceDatabaseName)}:{settings.SourceDatabaseName}");
                }

                if (settings.SourceEncryptConnection.HasValue)
                {
                    builder.Append($"/{nameof(settings.SourceEncryptConnection)}:{settings.SourceEncryptConnection}");
                }

                if (!string.IsNullOrEmpty(settings.SourcePassword))
                {
                    builder.Append($"/{nameof(settings.SourcePassword)}:{settings.SourcePassword}");
                }

                if (!string.IsNullOrEmpty(settings.SourceServerName))
                {
                    builder.Append($"/{nameof(settings.SourceServerName)}:{settings.SourceServerName}");
                }

                if (settings.SourceTimeout > 0)
                {
                    builder.Append($"/{nameof(settings.SourceTimeout)}:{settings.SourceTimeout}");
                }

                if (settings.SourceTrustServerCertificate.HasValue)
                {
                    builder.Append($"/{nameof(settings.SourceTrustServerCertificate)}:{settings.SourceTrustServerCertificate}");
                }

                if (!string.IsNullOrEmpty(settings.SourceUser))
                {
                    builder.Append($"/{nameof(settings.SourceUser)}:{settings.SourceUser}");
                }
            }

            if (!string.IsNullOrEmpty(settings.TargetConnectionString))
            {
                builder.Append($"/{nameof(settings.TargetConnectionString)}:\"{settings.TargetConnectionString}\"");
            }
            else if (settings.TargetFile != null)
            {
                builder.Append($"/{nameof(settings.TargetFile)}:\"{settings.TargetFile.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }
            else
            {
                if (!string.IsNullOrEmpty(settings.TargetDatabaseName))
                {
                    builder.Append($"/{nameof(settings.TargetDatabaseName)}:{settings.TargetDatabaseName}");
                }

                if (settings.TargetEncryptConnection.HasValue)
                {
                    builder.Append($"/{nameof(settings.TargetEncryptConnection)}:{settings.TargetEncryptConnection}");
                }

                if (!string.IsNullOrEmpty(settings.TargetPassword))
                {
                    builder.Append($"/{nameof(settings.TargetPassword)}:{settings.TargetPassword}");
                }

                if (!string.IsNullOrEmpty(settings.TargetServerName))
                {
                    builder.Append($"/{nameof(settings.TargetServerName)}:{settings.TargetServerName}");
                }

                if (settings.TargetTimeout > 0)
                {
                    builder.Append($"/{nameof(settings.TargetTimeout)}:{settings.TargetTimeout}");
                }

                if (settings.TargetTrustServerCertificate.HasValue)
                {
                    builder.Append($"/{nameof(settings.TargetTrustServerCertificate)}:{settings.TargetTrustServerCertificate}");
                }

                if (!string.IsNullOrEmpty(settings.TargetUser))
                {
                    builder.Append($"/{nameof(settings.TargetUser)}:{settings.TargetUser}");
                }
            }

            if (!string.IsNullOrEmpty(settings.TenantId))
            {
                builder.Append($"/{nameof(settings.TenantId)}:{settings.TenantId}");
            }

            if (settings.UniversalAuthentication.HasValue)
            {
                builder.Append($"/{nameof(settings.UniversalAuthentication)}:{settings.UniversalAuthentication}");
            }

            // Copy common settings to builder
            var commonBuilder = BuildSqlPackageArguments(settings);

            return CopyArgumentsTo(commonBuilder, builder);
        }
    }
}
