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
    [CakeAliasCategory("Sql")]
    public static class SqlPackageAliases
    {
        /// <summary>
        /// Test project with settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     var settings = new SqlPackageSettings();
        ///
        ///     SqlPackage(settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("SqlPackage")]
        public static void SqlPackage(this ICakeContext context, SqlPackageSettings settings)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var runner = new SqlPackageRunner(context.FileSystem,
                context.Environment,
                context.ProcessRunner,
                context.Tools);

            runner.Run(settings ?? new SqlPackageSettings());
        }
    }
}
