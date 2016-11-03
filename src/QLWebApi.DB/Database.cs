using QLWebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace QLWebApi.DB
{
    public class Database : IDatabase
    {
        private SqlConnectionStringBuilder ConnectionStringBuilder { get; }

        public Database(IOptions<DatabaseConfiguration> config) 
            : this(config?.Value) { }

        public Database(DatabaseConfiguration config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (string.IsNullOrEmpty(config.DataSource))
                throw new ArgumentException("DataSource must be initiated", config.DataSource);

            ConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                ["Data Source"] = config.DataSource,
                ["Integrated Security"] = config.SecurityIntegrated ?? false // true
            };
        }

        public async Task<string> GetDatabaseVersionAsync()
        {
            using (var connection = new SqlConnection(ConnectionStringBuilder.ConnectionString))
            {
                await connection.OpenAsync();

                var query = @"USE [QLGDB] 
                            SELECT [ParamValue] 
                            FROM [dbo].[Settings]";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return reader["ParamValue"].ToString();
                    }
                }
            }

            throw new Exception("Failed to fetch version from database due to an unknown error");
        }
    }

    public class DatabaseConfiguration
    {
        public string DataSource { get; set; }
        public bool? SecurityIntegrated { get; set; }
    }
}
