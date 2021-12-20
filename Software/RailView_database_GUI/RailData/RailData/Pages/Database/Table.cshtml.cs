using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using RailData.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace RailData.Pages.Database
{
    public class TableModel : PageModel
    {
        public ErrorHandling errorHandling = new ErrorHandling();
        MySqlConnection _connection;
        MySqlCommand cmd = null;

        // Objects and variables
        public List<string> Databases = new List<string>();
        public List<string> TableStruct = new List<string>();
        public List<Array> TableRecords = new List<Array>();
        public List<Array> TableValues = new List<Array>();
        string newConnectionString = "";
        string tableStructure = "";

        public void OnGet()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                try
                {
                    string getDatabaseName = Request.Query["databaseName"];
                    string getTableName = Request.Query["tableName"];
                    newConnectionString = $"Server=192.168.161.205;Port=3306;Database={getDatabaseName};Uid=admin;Pwd=TopMaster99;";

                    SelectDatabases();

                    ShowTable(getDatabaseName, getTableName);
                    AddEntry(getDatabaseName, getTableName);
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

        public void OnPost()
        {
            if (HttpContext.Session.GetString("Loggedin") != null && HttpContext.Session.GetString("connection") != null)
            {
                GetTableStructure();

                string values = "";

                try
                {
                    // namen ophalen en op volgorde zetten
                    string sql = $"INSERT INTO {Request.Query["tableName"]} ({tableStructure}) VALUES ({values})";
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
        }

        private void SelectDatabases()
        {
            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sqlDatabases = "SHOW DATABASES";
            cmd = new MySqlCommand(sqlDatabases, _connection);
            cmd.ExecuteNonQuery();

            MySqlDataReader databaseReader = cmd.ExecuteReader();

            while (databaseReader.Read())
            {
                for (int i = 0; i < databaseReader.FieldCount; i++)
                {
                    Databases.Add(databaseReader.GetString(i));
                }
            }

            _connection.Close();
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

                _connection = new MySqlConnection(HttpContext.Session.GetString("connection"));
                _connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                foreach (DataColumn column in data.Columns)
                {
                    tableStructure += column.ToString() + ", ";
                }
                Console.WriteLine(tableStructure);
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
    }
}
