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
                // Check if the login sessions exist.
                try
                {
                    // Request the database name and the table name from the query inside the URL.
                    string getDatabaseName = Request.Query["databaseName"];
                    string getTableName = Request.Query["tableName"];

                    var connect = _configuration.GetSection("Database");
                    // Set a new connection string with the correct database selected.
                    newConnectionString = $"Server={connect.GetSection("Server").Value};Port={connect.GetSection("Port").Value};Database={getDatabaseName};Uid={HttpContext.Session.GetString("Loggedin")};Pwd={HttpContext.Session.GetString("Username")};";

                    // Re-initialize the connection session.
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
            // Use the ExecuteQuery Class tho show all databases in the side navigation.
            ExecuteQuery executeQuery = new ExecuteQuery(newConnectionString);

            string sqlDatabases = "SHOW DATABASES";
            Databases = executeQuery.SimpleExecute(sqlDatabases);
        }

        private void ShowTable(string databaseName, string tableName)
        {
            // Set and open a new connection with the newly set connection string.
            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sql = $"USE {databaseName}; select * from {tableName}";

            // Set tempdata to show on front-end.
            TempData["databaseName"] = databaseName;
            TempData["tableName"] = tableName;

            // Initialize reusable lists.
            List<string> TableContents = new List<string>();
            List<string> Row = new List<string>();

            // Counters to correctly display the records inside a table.
            int z = 0;
            int rowLength = 0;
            int colLength = 0;

            try
            {
                // Execute the SQL statement from above and put them in a temporary list.
                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery();

                MySqlDataReader databaseReader = cmd.ExecuteReader();

                while (databaseReader.Read())
                {
                    rowLength += 1; // Update the row length for further use.

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
                // Close the connection after the try is complete.
                _connection.Close();
            }

            try
            {
                // Open a new connection with the DataTable reader.
                _connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connection);
                DataTable data = new DataTable();
                adapter.Fill(data);

                foreach (DataColumn column in data.Columns)
                {
                    // Get the columns from the table and put them in a temporary list.
                    // Update the column length for further use.
                    colLength += 1;
                    TableStruct.Add(column.ToString());
                }

                for (int i = 0; i < TableContents.Count; i++)
                {
                    if ((z % colLength) != (colLength - 1))
                    {
                        // If the remainder between z and colLength is not equal to colLength minus 1,
                        // Add the counter inside the TableContents list into the new temporary Row list so that a correct sorted Row is generated
                        Row.Add(TableContents[i]);
                    }
                    else
                    {
                        // If the remainder between z and colLength are equal to colLength minus 1,
                        // Add the counter inside the TableContents list into the new temporary Row list so that a correct sorted Row is generated.
                        // Then add the Row (converted to an Array type) to the TableRecords list.
                        Row.Add(TableContents[i]);
                        TableRecords.Add(Row.ToArray());
                        Row.Clear(); // Clear the row so it can go again with the next row in line of the SQL query.
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
            // Set and open a new connection with the newly set connection string.
            _connection = new MySqlConnection(newConnectionString);
            _connection.Open();

            string sql = $"SELECT COLUMN_NAME, COLUMN_TYPE FROM information_schema.COLUMNS WHERE TABLE_SCHEMA='{databaseName}' AND TABLE_NAME='{tableName}' ORDER BY ORDINAL_POSITION ASC;";

            // Initialize reusable lists.
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
                        // Get the table contents and put them in a list for further use.
                        TableContents.Add(databaseReader.GetString(i));
                    }

                    for (int i = 0; i < databaseReader.FieldCount; i++)
                    {
                        if (i + 1 < databaseReader.FieldCount)
                        {
                            // If the counter + 1 is smaller than the total of the database query, define a temporary value.
                            stripTimestamp = databaseReader.GetString(i + 1);
                        }

                        if (stripTimestamp != "timestamp")
                        {
                            if (i == i % 1)
                            {
                                // If the temporary value doesn't contains 'timestamp', add the rest to the FinalTableContents list.
                                FinalTableContents.Add(databaseReader.GetString(i));
                            }
                        }
                    }
                }

                foreach (var item in FinalTableContents)
                {
                    tableStructure += item + ",";
                }
                // Add the table structure in a tempdata for viewing in the front-end.
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
                    colLength += 1; // Count the columns in the SQL query
                }

                for (int i = 0; i < TableContents.Count; i++)
                {
                    if ((z % colLength) != (colLength - 1))
                    {
                        // If the remainder between z and colLength is not equal to colLength minus 1,
                        // Add the counter inside the TableContents list into the new temporary Row list so that a correct sorted Row is generated.
                        Row.Add(TableContents[i]);
                    }
                    else
                    {
                        // If the remainder between z and colLength are equal to colLength minus 1,
                        // Add the counter inside the TableContents list into the new temporary Row list so that a correct sorted Row is generated.
                        // Then add the Row (converted to an Array type) to the TableValues list.
                        Row.Add(TableContents[i]);
                        TableValues.Add(Row.ToArray());
                        Row.Clear(); // Clear the row so it can go again with the next row in line of the SQL query.
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
            // Get the table structure on page load.
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
            // !! Parameters defined in this method are defined in the site.js !!

            // If the FormHandler is called by an OnPost event.
            // Trim the chars defined from the JSON object given and put them into an array split by an comma.
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
                // Trim the global doublequotes from the layout and values given.
                string TrimmedLayout = layout.Remove(layout.Length - 1, 1);
                string TrimmedValues = values.Remove(values.Length - 1, 1);

                string sql = $"INSERT INTO {tableName} ({TrimmedLayout}) VALUES ({TrimmedValues})";

                _connection = new MySqlConnection(HttpContext.Session.GetString("connection"));
                _connection.Open();
                cmd = new MySqlCommand(sql, _connection);
                cmd.ExecuteNonQuery(); // Execute insert query.
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
            // !! Parameters defined in this method are defined in the site.js !!

            // Use the ExecuteQuery Class to delete the selected record from a specific table.
            ExecuteQuery executeQuery = new ExecuteQuery(HttpContext.Session.GetString("connection"));

            string sql = $"DELETE FROM {tableName} WHERE {firstCol}={id}";
            executeQuery.SimpleExecute(sql);
        }
    }
}
