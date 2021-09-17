using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOLibrary
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<TEntity> ExecuteReader<TEntity>(Command cmd, Func<SqlDataReader, TEntity> convert)
        {
            using(SqlConnection c = createConnection())
            {
                using(SqlCommand command = createCommand(cmd, c))
                {
                    c.Open();
                    using(SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            yield return convert(dataReader);
                        }
                    }
                }
            }
        }

        public object ExecuteScalar(Command cmd)
        {
            using(SqlConnection c = createConnection())
            {
                using (SqlCommand command = createCommand(cmd, c))
                {
                    c.Open();
                    object O = command.ExecuteScalar();
                    return (O is DBNull) ? null : O; 
                }
            }
        }

        public int ExecuteNonQuery(Command cmd)
        {
            using (SqlConnection c = createConnection())
            {
                using (SqlCommand command = createCommand(cmd, c))
                {
                    c.Open();
                    return command.ExecuteNonQuery();  
                }
            }
        }


        private SqlConnection createConnection()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            return connection;
        }

        private SqlCommand createCommand(Command cmd, SqlConnection connection)
        {
            SqlCommand sqlCmd = connection.CreateCommand();
            sqlCmd.CommandText = cmd._query;
            sqlCmd.CommandType = cmd._isStoredProcedure ? System.Data.CommandType.StoredProcedure : System.Data.CommandType.Text;
            if (!(cmd._parameters is null))
            {
                foreach (KeyValuePair<string, object> param in cmd._parameters)
                {
                    SqlParameter parameter = sqlCmd.CreateParameter();
                    parameter.ParameterName = param.Key;
                    parameter.Value = param.Value;

                    sqlCmd.Parameters.Add(parameter);
                }
            }
            return sqlCmd;
        }
    }
}
