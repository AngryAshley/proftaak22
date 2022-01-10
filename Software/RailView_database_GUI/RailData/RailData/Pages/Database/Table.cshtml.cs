using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RailData.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RailData.Pages.Database
{
    public class TableModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ErrorHandling errorHandling = new ErrorHandling();
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        // Objects and variables
        public List<string> Databases = new List<string>();
        public List<string> TableStruct = new List<string>();
        public List<Array> TableRecords = new List<Array>();
        public List<Array> TableValues = new List<Array>();
        public string tableStructure = "";
        string newConnectionString = "";
        string values = "";

        public TableModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                try
                {
                    string getDatabaseName = Request.Query["databaseName"];
                    string getTableName = Request.Query["tableName"];
                    var connect = _configuration.GetSection("Database");
                    newConnectionString = $"Server={connect.GetSection("Server").Value};Port={connect.GetSection("Port").Value};Database={getDatabaseName};Uid={HttpContext.Session.GetString("Loggedin")};Pwd={HttpContext.Session.GetString("Username")};";
                    HttpContext.Session.Remove("connection");
                    HttpContext.Session.SetString("connection", newConnectionString);

                    SelectDatabases();

                    ShowTable(getDatabaseName, getTableName);
                    AddEntry(getDatabaseName, getTableName);
                    GetTableStructure();
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            errorHandling.ErrorMessage = "Invalid session, please try to log in";
                            break;
                        case 1045:
                            errorHandling.ErrorMessage = "Cannot connect to server";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    errorHandling.ErrorMessage = "Could not connect to server: " + ex;
                }
            }
        }

        private void SelectDatabases()
        {
            ExecuteQuery executeQuery = new ExecuteQuery(newConnectionString);

            string sqlDatabases = "SHOW DATABASES";
            Databases = executeQuery.SimpleExecute(sqlDatabases);
        }

        private void ShowTable(string databaseName, string tableName)
        {
            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sql = $"USE {databaseName}; select * from {tableName}";
            TempData["databaseName"] = databaseName;
            TempData["tableName"] = tableName;

            List<string> TableContents = new List<string>();
            List<string> Row = new List<string>();
            int z = 0;
            int rowLength = 0;
            int colLength = 0;

            try
            {

                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader databaseReader = cmd.ExecuteReader();

                while (databaseReader.Read())
                {
                    rowLength += 1;

                    for (int i = 0; i < databaseReader.FieldCount; i++)
                    {
                        TableContents.Add(databaseReader.GetString(i));
                    }
                }

            }
            catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }

            try
            {
                _connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                foreach (DataColumn column in data.Columns)
                {
                    colLength += 1;
                    TableStruct.Add(column.ToString());
                }

                for (int i = 0; i < TableContents.Count; i++)
                {
                    if ((z % colLength) != (colLength - 1))
                    {
                        Row.Add(TableContents[i]);
                    }
                    else
                    {
                        Row.Add(TableContents[i]);
                        TableRecords.Add(Row.ToArray());
                        Row.Clear();
                    }
                    z++;
                }
            }
            catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
        }

        private void AddEntry(string databaseName, string tableName)
        {
            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sql = $"SELECT COLUMN_NAME, COLUMN_TYPE FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='{databaseName}' AND TABLE_NAME='{tableName}' ORDER BY ORDINAL_POSITION ASC;";

            List<string> TableContents = new List<string>();
            List<string> Row = new List<string>();
            List<string> FinalTableContents = new List<string>();
            int z = 0;
            int rowLength = 0;
            int colLength = 0;
            string stripTimestamp = "";

            try
            {
                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader databaseReader = cmd.ExecuteReader();

                while (databaseReader.Read())
                {
                    rowLength += 1;

                    for (int i = 0; i < databaseReader.FieldCount; i++)
                    {
                        TableContents.Add(databaseReader.GetString(i));
                    }

                    for (int i = 0; i < databaseReader.FieldCount; i++)
                    {
                        if (i + 1 < databaseReader.FieldCount)
                        {
                            stripTimestamp = databaseReader.GetString(i + 1);
                        }

                        if (stripTimestamp != "timestamp")
                        {
                            if (i == i % 1)
                            {
                                FinalTableContents.Add(databaseReader.GetString(i));
                            }
                        }
                    }
                }

                foreach (var item in FinalTableContents)
                {
                    tableStructure += item + ",";
                }
                TempData["tableStructure"] = tableStructure;

            }
            catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }

            try
            {
                _connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                foreach (DataColumn column in data.Columns)
                {
                    colLength += 1;
                }

                for (int i = 0; i < TableContents.Count; i++)
                {
                    if ((z % colLength) != (colLength - 1))
                    {
                        Row.Add(TableContents[i]);
                    }
                    else
                    {
                        Row.Add(TableContents[i]);
                        TableValues.Add(Row.ToArray());
                        Row.Clear();
                    }
                    z++;
                }
            }
            catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
        }

        private void GetTableStructure()
        {
            try
            {
                string sql = $"select * from {Request.Query["tableName"]};";

                _connection = new MySqlConnection(newConnectionString);
                _connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
        }

        public void OnPostFormHandler(string inputs, string layout, string tableName)
        {
            string enumValue = inputs;
            char[] charsToTrim = { '(', ')', ',', '"', '[', ']' };
            enumValue = enumValue.Trim(charsToTrim);
            var enumArr = enumValue.Split(',');

            foreach (var item in enumArr)
            {
                values += item + ",";
            }

            try
            {
                string TrimmedLayout = layout.Remove(layout.Length - 1, 1);
                string TrimmedValues = values.Remove(values.Length - 1, 1);

                string sql = $"INSERT INTO {tableName} ({TrimmedLayout}) VALUES ({TrimmedValues})";

                _connection = new MySqlConnection(HttpContext.Session.GetString("connection"));
                _connection.Open();
                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            }
            finally
            {
                _connection.Close();
            }
        }

        public void OnPostDeleteRecord(string id, string tableName, string firstCol)
        {
            ExecuteQuery executeQuery = new ExecuteQuery(HttpContext.Session.GetString("connection"));

            string sql = $"DELETE FROM {tableName} WHERE {firstCol}={id}";
            executeQuery.SimpleExecute(sql);
        }
    }
}
