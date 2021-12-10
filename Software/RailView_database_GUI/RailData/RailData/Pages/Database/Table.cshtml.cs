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
        public int rowLength = 0;
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
            _connection.Open();

            string sql = $"USE {databaseName}; select * from {tableName}";
            TempData["databaseName"] = databaseName;
            TempData["tableName"] = tableName;

            //cmd = new MySqlCommand(sql, _connection);
            //cmd.ExecuteNonQuery();

            //MySqlDataReader tableReader = cmd.ExecuteReader();

            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                foreach (DataColumn column in data.Columns)
                {
                    TableStruct.Add(column.ToString());

                    foreach (DataRow row in data.Rows)
                    {
                        TableContents.Add(row[column].ToString());
                    }
                }

                foreach (DataRow row in data.Rows)
                {
                    rowLength += 1;
                }

            } catch (Exception ex)
            {
                errorHandling.ErrorMessage = ex.ToString();
            } finally
            {
                _connection.Close();
            }



            //while (tableReader.Read())
            //{
            //    Console.WriteLine(tableReader["COLUMN_NAME"]);
            //    for (int i = 0; i < tableReader.FieldCount; i++)
            //    {
            //        TableContents.Add(tableReader.GetString(i));
            //    }
            //}

            //_connection.Close();
        }
    }
}
