using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.SqlPackage
{
    /// <summary>
    /// SqlPackage tool execution.
    /// </summary>
    public abstract class SqlPackageRunner<T> : Tool<T>
        where T : SqlPackageSettings
    {
        /// <summary>
        /// The Cake environment in context.
        /// </summary>
        public ICakeEnvironment Environment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cake.Core.Tooling.Tool`1" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator.</param>
        protected SqlPackageRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            Environment = environment;
        }

        /// <summary>
        /// Builds the common SqlPackage arguments.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        protected ProcessArgumentBuilder BuildSqlPackageArguments(T settings)
        {
            var builder = new ProcessArgumentBuilder();

            var properties = BuildProperties(settings);
            properties.CopyTo(builder);

            switch (settings.Action)
            {
                case SqlPackageAction.Publish:
                case SqlPackageAction.DeployReport:
                case SqlPackageAction.Script:
                    var variables = BuildVariables(settings);
                    variables.CopyTo(builder);
                    break;
                case SqlPackageAction.Extract:
                case SqlPackageAction.DriftReport:
                case SqlPackageAction.Export:
                case SqlPackageAction.Import:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return builder;
        }

        /// <summary>
        /// Builds the <see cref="SqlPackageAction"/>.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        protected ProcessArgumentBuilder CreateBuilder(T settings)
        {
            var builder = new ProcessArgumentBuilder();

            builder.Append($"/Action:{settings.Action}");

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

        private ProcessArgumentBuilder BuildVariables(SqlPackageSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            if (settings.Variables.Any())
            {
                foreach (var variable in settings.Variables)
                {
                    builder.Append($"/p:{variable.Key}={variable.Value}");
                }
            }

            return builder;
        }

        private ProcessArgumentBuilder BuildProperties(SqlPackageSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            if (!settings.Properties.Any())
            {
                return builder;
            }

            foreach (var property in settings.Properties)
            {
                builder.Append($"/p:{property.Key}={property.Value}");
            }

            return builder;
        }
    }
}
