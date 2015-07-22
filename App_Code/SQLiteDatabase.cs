using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data;

public class SQLiteDatabase
{
    String dbConnection;

    /// <summary>
    ///     Default Constructor for SQLiteDatabase Class.
    /// </summary>
    public SQLiteDatabase()
    {
        string path = System.Web.HttpContext.Current.Server.MapPath("/App_Data/data.haha");
        dbConnection = "Data Source=" + path;
    }

    /// <summary>
    ///     Single Param Constructor for specifying the DB file.
    /// </summary>
    /// <param name="inputFile">The File containing the DB</param>
    public SQLiteDatabase(String inputFile)
    {
        dbConnection = String.Format("Data Source={0}", inputFile);
    }

    /// <summary>
    ///     Single Param Constructor for specifying advanced connection options.
    /// </summary>
    /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
    public SQLiteDatabase(Dictionary<String, String> connectionOpts)
    {
        String str = "";
        foreach (KeyValuePair<String, String> row in connectionOpts)
        {
            str += String.Format("{0}={1}; ", row.Key, row.Value);
        }
        str = str.Trim().Substring(0, str.Length - 1);
        dbConnection = str;
    }

    /// <summary>
    ///     Allows the programmer to run a query against the Database.
    /// </summary>
    /// <param name="sql">The SQL to run</param>
    /// <returns>A DataTable containing the result set.</returns>
    public DataTable GetDataTable(string sql)
    {
        DataTable dt = new DataTable();
        SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        try
        {
            
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            SQLiteDataReader reader = mycommand.ExecuteReader();
            dt.Load(reader);
            reader.Close();
            cnn.Close();
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(cnn.ConnectionString+"\n");
            HttpContext.Current.Response.End();
            //throw new Exception(e.Message);
        }
        return dt;
    }


    /// <summary>
    ///     Allows the programmer to interact with the database for purposes other than a query.
    /// </summary>
    /// <param name="sql">The SQL to be run.</param>
    /// <returns>An Integer containing the number of rows updated.</returns>
    public int ExecuteNonQuery(string sql)
    {
        SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        cnn.Open();
        SQLiteCommand mycommand = new SQLiteCommand(cnn);
        mycommand.CommandText = sql;
        int rowsUpdated = mycommand.ExecuteNonQuery();
        cnn.Close();
        return rowsUpdated;
    }

    /// <summary>
    ///     Allows the programmer to retrieve single items from the DB.
    /// </summary>
    /// <param name="sql">The query to run.</param>
    /// <returns>A string.</returns>
    public string ExecuteScalar(string sql)
    {
        SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        cnn.Open();
        SQLiteCommand mycommand = new SQLiteCommand(cnn);
        mycommand.CommandText = sql;
        object value = mycommand.ExecuteScalar();
        cnn.Close();
        if (value != null)
        {
            return value.ToString();
        }
        return "";
    }

    public string ExecuteScalar(string sql, Dictionary<String, object> data)
    {
        SQLiteConnection cnn = new SQLiteConnection(dbConnection);
        cnn.Open();
        SQLiteCommand mycommand = new SQLiteCommand(cnn);
        mycommand.CommandText = sql;

        foreach (KeyValuePair<String, object> val in data)
        {
            mycommand.Parameters.Add(new SQLiteParameter(val.Key, val.Value));
        }


        object value = mycommand.ExecuteScalar();
        cnn.Close();
        if (value != null)
        {
            return value.ToString();
        }
        return "";
    }

    /// <summary>
    ///     Allows the programmer to easily update rows in the DB.
    /// </summary>
    /// <param name="tableName">The table to update.</param>
    /// <param name="data">A dictionary containing Column names and their new values.</param>
    /// <param name="where">The where clause for the update statement.</param>
    /// <returns>A boolean true or false to signify success or failure.</returns>
    public bool Update(String tableName, Dictionary<String, String> data, String where)
    {
        String vals = "";
        Boolean returnCode = true;
        if (data.Count >= 1)
        {
            foreach (KeyValuePair<String, String> val in data)
            {
                vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
            }
            vals = vals.Substring(0, vals.Length - 1);
        }
        try
        {
            this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
        }
        catch
        {
            returnCode = false;
        }
        return returnCode;
    }
}
