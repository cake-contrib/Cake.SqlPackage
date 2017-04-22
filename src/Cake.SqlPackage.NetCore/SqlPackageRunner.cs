using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.SqlPackage
{
    /// <summary>
    /// SqlPackage tool execution.
    /// </summary>
    public class SqlPackageRunner : Tool<SqlPackageSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cake.Core.Tooling.Tool`1" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        public SqlPackageRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            Environment = environment;
        }

        /// <summary>
        /// The Cake environment in context.
        /// </summary>
        public ICakeEnvironment Environment { get; set; }


        /// <summary>
        /// Runs SqlPackage with the specified settings.
        /// </summary>
        /// <param name="settings"></param>
        public void Run(SqlPackageSettings settings)
        {
            if(settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(settings));
        }

        private ProcessArgumentBuilder GetArguments(SqlPackageSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append($"/Action:{settings.Action}");

            if(settings.OutputPath != null)
            {
                builder.Append($"/OutputPath:\"{settings.OutputPath.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }

            if(settings.OverwriteFiles.HasValue)
            {
                builder.Append($"/OverwriteFiles:{settings.OverwriteFiles}");
            }

            if(settings.Profile != null)
            {
                builder.Append($"/Profile:\"{settings.Profile}\"");
            }

            if(settings.Quiet.HasValue)
            {
                builder.Append($"/Quiet:{settings.Quiet}");
            }

            if(!string.IsNullOrEmpty(settings.ClientId))
            {
                builder.Append($"/ClientId:{settings.ClientId}");
            }

            if (!string.IsNullOrEmpty(settings.AzureSecret))
            {
                builder.Append($"/Secret:{settings.AzureSecret}");
            }

            if (settings.AzureKeyVaultAuthMethod.HasValue)
            {
                builder.Append($"/AzureKeyVaultAuthMethod:{settings.AzureKeyVaultAuthMethod}");
            }

            if (!string.IsNullOrEmpty(settings.SourceConnectionString))
            {
                builder.Append($"/SourceConnectionString:{settings.SourceConnectionString}");
            }
            else if (settings.SourceFile != null)
            {
                builder.Append($"/SourceFile:\"{settings.SourceFile.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }
            else
            {
                if (!string.IsNullOrEmpty(settings.SourceDatabaseName))
                {
                    builder.Append($"/SourceDatabaseName:{settings.SourceDatabaseName}");
                }

                if (settings.SourceEncryptConnection.HasValue)
                {
                    builder.Append($"/SourceEncryptConnection:{settings.SourceEncryptConnection}");
                }

                if (!string.IsNullOrEmpty(settings.SourcePassword))
                {
                    builder.Append($"/SourcePassword:{settings.SourcePassword}");
                }

                if (!string.IsNullOrEmpty(settings.SourceServerName))
                {
                    builder.Append($"/SourceServerName:{settings.SourceServerName}");
                }

                if (settings.SourceTimeout > 0)
                {
                    builder.Append($"/SourceTimeout:{settings.SourceTimeout}");
                }

                if (settings.SourceTrustServerCertificate.HasValue)
                {
                    builder.Append($"/SourceTrustServerCertificate:{settings.SourceTrustServerCertificate}");
                }

                if (!string.IsNullOrEmpty(settings.SourceUser))
                {
                    builder.Append($"/SourceUser:{settings.SourceUser}");
                }
            }

            if (!string.IsNullOrEmpty(settings.TargetConnectionString))
            {
                builder.Append($"/TargetConnectionString:{settings.TargetConnectionString}");
            }
            else if (settings.TargetFile != null)
            {
                builder.Append($"/TargetFile:\"{settings.TargetFile.MakeAbsolute(Environment.WorkingDirectory).FullPath}\"");
            }
            else
            {
                if (!string.IsNullOrEmpty(settings.TargetDatabaseName))
                {
                    builder.Append($"/TargetDatabaseName:{settings.TargetDatabaseName}");
                }

                if (settings.TargetEncryptConnection.HasValue)
                {
                    builder.Append($"/TargetEncryptConnection:{settings.TargetEncryptConnection}");
                }

                if (!string.IsNullOrEmpty(settings.TargetPassword))
                {
                    builder.Append($"/TargetPassword:{settings.TargetPassword}");
                }

                if (!string.IsNullOrEmpty(settings.TargetServerName))
                {
                    builder.Append($"/TargetServerName:{settings.TargetServerName}");
                }

                if (settings.TargetTimeout > 0)
                {
                    builder.Append($"/TargetTimeout:{settings.TargetTimeout}");
                }

                if (settings.TargetTrustServerCertificate.HasValue)
                {
                    builder.Append($"/TargetTrustServerCertificate:{settings.TargetTrustServerCertificate}");
                }

                if (!string.IsNullOrEmpty(settings.TargetUser))
                {
                    builder.Append($"/TargetUser:{settings.TargetUser}");
                }
            }

            return builder;
        }

        /// <summary>Gets the name of the tool.</summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "SqlPackage";
        }

        /// <summary>Gets the possible names of the tool executable.</summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "SqlPackage.exe" };
        }
    }
}
