using QLWebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace QLWebApi.DB
{
    public class DB : IDB
    {
        public SqlConnectionStringBuilder ConnectionStringBuilder { get; private set; }

        public DB()
        {
            ConnectionStringBuilder = new SqlConnectionStringBuilder();
            ConnectionStringBuilder["Data Source"] = @"VICQL\MSSQLSERVER2016";
            ConnectionStringBuilder["Integrated Security"] = true;
        }

        public string GetQLGDBVer()
        {
            string query = "USE [QLGDB] " +
                           "SELECT [ParamValue] " +
                           "FROM [dbo].[Settings]";
            string message = "";
            using (SqlConnection connection = new SqlConnection(ConnectionStringBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // if successfully got the DB version
                                message = "<Version>" + reader["ParamValue"].ToString() + "</Version>";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = "<Error>" + ex.Message + "</Error>";
                }
            }

            return message;
        }
    }
}
