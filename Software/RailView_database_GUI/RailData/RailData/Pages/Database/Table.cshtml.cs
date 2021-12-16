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
        public List<string> TableContents = new List<string>();
        public List<string> Row = new List<string>();
        public List<Array> FinalRow = new List<Array>();
        public int rowLength = 0;
        public int colLength = 0;
        string newConnectionString = "";

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

        public void SelectDatabases()
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

        public void ShowTable(string databaseName, string tableName)
        {
            int z = 0;

            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sql = $"USE {databaseName}; select * from {tableName}";
            TempData["databaseName"] = databaseName;
            TempData["tableName"] = tableName;

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

            } catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            } finally
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
                    } else
                    {
                        Row.Add(TableContents[i]);
                        FinalRow.Add(Row.ToArray());
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
    }
}
