using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

public class SqlDB
{
    private string connectionString;

    public SqlDB(string connectionString)
    {
        this.connectionString = connectionString;
    }

    /// <summary>
    /// Executes a stored procedure.
    /// </summary>
    /// <param name="spName">
    /// The name of the stored procedure to execute.
    /// </param>
    /// <param name="paramList">
    /// A dictionary of named parameters and their associated 
    /// values expected by the stored procedure. If no parameters are required then 
    /// the dictionary may be omitted (or be empty).
    /// </param>
    public void ExecuteSP(string spName, Dictionary<string, object> paramList = null)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();

                if (paramList != null)
                {
                    SqlCommandBuilder.DeriveParameters(sqlCommand);

                    foreach (SqlParameter p in sqlCommand.Parameters)
                    {
                        object paramValue;
                        if (paramList.TryGetValue(p.ParameterName.Replace("@", ""), out paramValue))
                            p.Value = paramValue;
                    }
                }
                sqlCommand.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Gets the rows returned by a stored procedure query and return as a dataset.
    /// </summary>
    /// <param name="spName">
    /// The name of the stored procedure to execute.
    /// </param>
    /// <param name="paramList">
    /// A dictionary of named parameters and their associated 
    /// values expected by the stored procedure. If no parameters are required then 
    /// the dictionary may be omitted (or be empty).
    /// </param>
    /// <returns>
    /// A dataset containing the results of the stored procedure query.
    /// </returns>
    public DataSet GetDataSet(string spName, Dictionary<string, object> paramList = null)
    {
        DataSet ds = new DataSet();
        DataTable dt = GetDataTable(spName, paramList);
        ds.Tables.Add(dt);
        return ds;
    }

    /// <summary>
    ///  Gets the rows returned by a stored procedure query and return as a datatable.
    /// </summary>
    /// <param name="spName">
    /// The name of the stored procedure to execute.
    /// </param>
    /// <param name="paramList">
    /// A dictionary of named parameters and their associated 
    /// values expected by the stored procedure. If no parameters are required then 
    /// the dictionary may be omitted (or be empty).
    /// </param>
    /// <returns>
    /// A datatable containing the results of the stored procedure query.
    /// </returns>
    public DataTable GetDataTable(string spName, Dictionary<string, object> paramList = null)
    {
        DataTable dt = new DataTable();
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
            {
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (paramList != null)
                    {
                        foreach (var param in paramList)
                        {
                            sqlCommand.Parameters.Add(param.Key, GetSqlDBType(param.Value)).Value = param.Value;
                        }
                    }
                    sqlAdapter.Fill(dt);
                }
            }
        }
        return dt;
    }

    /// <summary>
    /// Get a list of objects by converting each row of a datatable into a specific class/type T.
    /// </summary>
    /// <typeparam name="T">The type to convert rows into.</typeparam>
    /// <param name="spName">The stored procedure used to poulate the datatable.</param>
    /// <param name="paramList">
    /// A dictionary of named parameters and their associated 
    /// values expected by the stored procedure. If no parameters are required then 
    /// the dictionary may be omitted (or be empty).
    /// </param>
    /// <returns></returns>
    public List<T> GetList<T>(string spName, Dictionary<string, object> paramList = null) where T : new()
    {
        return ToList<T>(GetDataTable(spName, paramList));
    }

    /// <summary>
    /// Get a single item of type T.
    /// </summary>
    /// <typeparam name="T">The type of object to return.</typeparam>
    /// <param name="spName">The stored procedure to execute.</param>
    /// <param name="paramList">
    /// A dictionary of named parameters and their associated 
    /// values expected by the stored procedure. If no parameters are required then 
    /// the dictionary may be omitted (or be empty).
    /// </param>
    /// <returns>A single object of type T</returns>
    public T GetItem<T>(string spName, Dictionary<string, object> paramList = null) where T : new()
    {
        DataTable dataTable = GetDataTable(spName, paramList);
        return CreateItem<T>(dataTable.Rows[0], typeof(T).GetProperties());
    }

    /// <summary>
    /// Persist the item into the database. Extract the object fields/properties and convert to 
    /// appropriate sql type - insert in to store procedure parameters and write to database.
    /// </summary>
    /// <typeparam name="T">Generic object of any class</typeparam>
    /// <param name="spName"></param>
    public void CreateNewItem<T>(string spName, T item)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
            {
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    var props = typeof(T).GetProperties();

                    foreach (var property in props)
                    {
                        sqlCommand.Parameters.AddWithValue("@" + property.Name, property.GetValue(item));
                    }
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }

    /// <summary>
    /// Executes a stored procedure that returns a single value (of any type)
    /// </summary>
    /// <typeparam name="T">the expected type of the result</typeparam>
    /// <param name="spName">the name of the stored procedure - must return a single scalar value</param>
    /// <param name="paramList">any parameters expected by the stored procedure, can be empty or omitted</param>
    /// <returns>a value of type T</returns>
    public T GetValue<T>(string spName, Dictionary<string, object> paramList = null)
    {
        T t;

        using (SqlConnection sqlConnection = new SqlConnection())
        {
            using (SqlCommand sqlCommand = new SqlCommand(spName, sqlConnection))
            {
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (paramList != null)
                    {
                        foreach (var param in paramList)
                        {
                            sqlCommand.Parameters.Add(param.Key, GetSqlDBType(param.Value)).Value = param.Value;
                        }
                    }
                    t = (T)sqlCommand.ExecuteScalar();
                }
            }
        }
        return t;
    }

    /// <summary>
    /// simple mapping of CLR type to corresponding SQLDBType
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public SqlDbType GetSqlDBType(object param)
    {
        switch (Type.GetTypeCode(param.GetType()))
        {
            case TypeCode.Boolean:
                return SqlDbType.Bit;

            case TypeCode.Byte:
                return SqlDbType.Binary;

            case TypeCode.Char:
                return SqlDbType.Char;

            case TypeCode.DateTime:
                return SqlDbType.DateTime;

            case TypeCode.Decimal:
                return SqlDbType.Decimal;

            case TypeCode.Double:
                return SqlDbType.Float;

            case TypeCode.Int16:
            case TypeCode.Int32:
            case TypeCode.Int64:
                return SqlDbType.Int;

            case TypeCode.Object:
                return SqlDbType.VarBinary;

            case TypeCode.Single:
                return SqlDbType.Float;

            case TypeCode.String:
                return SqlDbType.NVarChar;

            default:
                throw new ArgumentOutOfRangeException("Undefined Type");
        }
    }

    public T CreateItem<T>(DataRow row, IList<PropertyInfo> properties) where T : new()
    {
        T item = new T();
        foreach (var property in properties)
        {
            property.SetValue(item, Convert.ChangeType(row[property.Name], property.PropertyType), null);
        }
        return item;
    }

    public List<T> ToList<T>(DataTable dataTable) where T : new()
    {
        IList<PropertyInfo> properties = typeof(T).GetProperties();
        List<T> result = new List<T>();

        foreach (DataRow row in dataTable.Rows)
        {
            var item = CreateItem<T>(row, properties);
            result.Add(item);
        }
        return result;
    }
}