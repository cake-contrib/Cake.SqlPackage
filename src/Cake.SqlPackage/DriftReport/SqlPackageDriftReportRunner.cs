using System;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.SqlPackage
{
    /// <summary>
    /// SqlPackage tool execution for driftreport action.
    /// </summary>
    /// <seealso cref="SqlPackageRunner{SqlPackageDriftReportSettings}" />
    public class SqlPackageDriftReportRunner : SqlPackageRunner<SqlPackageDriftReportSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlPackageDriftReportRunner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tools.</param>
        public SqlPackageDriftReportRunner(IFileSystem fileSystem,
            ICakeEnvironment environment,
            IProcessRunner processRunner,
            IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        /// <summary>
        /// Runs SqlPackage with the specified extract settings.
        /// </summary>
        /// <param name="settings">The SQL package extract settings.</param>
        public void DriftReport(SqlPackageDriftReportSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, BuildArguments(settings));
        }

        private ProcessArgumentBuilder BuildArguments(SqlPackageDriftReportSettings settings)
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

            if (settings.Quiet.HasValue)
            {
                builder.Append($"/{nameof(settings.Quiet)}:{settings.Quiet}");
            }

            if (!string.IsNullOrEmpty(settings.TargetConnectionString))
            {
                builder.Append($"/{nameof(settings.TargetConnectionString)}:\"{settings.TargetConnectionString}\"");
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
            commonBuilder.CopyTo(builder);

            return builder;
        }
    }
}
