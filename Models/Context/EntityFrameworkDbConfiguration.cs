using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.SqlClient;


namespace AlpataProje.Models.Context
{
    internal sealed class EntityFrameworkDbConfiguration : DbConfiguration
    {
        /// <summary>
        /// The provider manifest token to use for SQL Server.
        /// </summary>
        private const string SqlServerManifestToken = @"2012";

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkDbConfiguration"/> class.
        /// </summary>
        public EntityFrameworkDbConfiguration()
        {
            this.AddDependencyResolver(new SingletonDependencyResolver<IManifestTokenResolver>(new ManifestTokenService()));
        }

        /// <inheritdoc />
        private sealed class ManifestTokenService : IManifestTokenResolver
        {
            /// <summary>
            /// The default token resolver.
            /// </summary>
            private static readonly IManifestTokenResolver DefaultManifestTokenResolver = new DefaultManifestTokenResolver();

            /// <inheritdoc />
            public string ResolveManifestToken(DbConnection connection)
            {
                if (connection is SqlConnection)
                {
                    return SqlServerManifestToken;
                }

                return DefaultManifestTokenResolver.ResolveManifestToken(connection);
            }
        }
    }
}