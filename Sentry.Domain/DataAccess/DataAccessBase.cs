using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Sentry.Domain.DataAccess.Entities
{
    /// <summary>
    /// common database methods: connect, execute procedure, etc.
    /// </summary>
    public class DataAccessBase
    {
        public string ConnectionString { protected get; set; }
        protected int CommandTimeout { get; set; }

        protected SqlConnection GetDbConnection()
        {

            SqlConnection result = new SqlConnection(ConnectionString);
            result.Open();
            return result;
        }

        protected SqlCommand CreateCommand(SqlConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                SqlCommand result = conn.CreateCommand();

                result.CommandTimeout = CommandTimeout;

                return result;
            }

            return null;
        }

        protected string ExecuteProcedure(string procedureName, Dictionary<string, object> parameters)
        {
            var status = GetProcedureResults<ResultWithStatus>(procedureName, parameters).FirstOrDefault();

            if (status != null)
                return status.Status;
            else
                return string.Empty;
        }

        protected int? ExecuteProcedure_ReturnIdentity(string procedureName, Dictionary<string, object> parameters)
        {
            var status = GetProcedureResults<ResultWithStatus>(procedureName, parameters).FirstOrDefault();

            if (status != null)
                return status.NewRecordId;
            else
                return null;
        }

        protected IEnumerable<T> GetProcedureResults<T>(string procedureName, Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
                parameters = new Dictionary<string, object>();

            var result = new Collection<T>();

            var constructorParameterTypes = new Type[] { typeof(IDataRecord) };
            var constructorInfo = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, constructorParameterTypes, null);

            using (SqlConnection conn = GetDbConnection())
            {
                SqlCommand cmd = CreateCommand(conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                foreach (var p in parameters)
                {
                    if (p.Value is IEnumerable<SqlDataRecord>)
                    {
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = p.Key;
                        param.SqlDbType = SqlDbType.Structured;
                        param.Value = p.Value;
                        param.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param);
                    }
                    else
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                }

                // if p.Value is 

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    var constuctorParameters = new object[] { reader };
                    while (reader.Read())
                        result.Add((T)constructorInfo.Invoke(constuctorParameters));
                }
            }

            return result;
        }

 
    }
}
