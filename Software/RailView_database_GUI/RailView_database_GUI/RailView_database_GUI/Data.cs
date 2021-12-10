using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class Data : Form
    {
        ExecuteQuery executeQuery = new ExecuteQuery();
        GridButton gridButton = new GridButton();
        Navigation navigation = new Navigation();
        Dashboard dashboard = null;
        public string CurrentTableName;
        public string AmountRowsNew;
        public string DatabaseName;
        bool IsDatabase;

        public Data(Dashboard c_dashboard)
        {
            InitializeComponent();
            dashboard = c_dashboard;
            IsDatabase = true;
        }

        private void Data_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.161.205;Port=3306;Database=" + dashboard.DatabaseName + ";Uid=admin;Pwd=TopMaster99;Convert Zero Datetime=true;";
            string name;
            string sql;
            string tableName;
            bool countRows;
            DataGridViewButtonColumn btn;
            DataGridViewTextBoxColumn clm;

            if (sender != null)
            {
                List<Control> myControls = navigation.AddNaviagtion();
                foreach (Control c in myControls)
                {
                    c.Click += new EventHandler(lblClicked);
                    this.Controls.Add(c);
                    c.BringToFront();
                }
            }


            //If stetement en laat daarin zien wat er te zien moet zijn 
            if(IsDatabase == true)
            {
                lblTitle.Text = "Database: " + dashboard.DatabaseName;

                if(IsDatabase == true)
                {
                    name = "Tables";
                    clm = new DataGridViewTextBoxColumn();
                    clm.Name = name;
                    clm.HeaderText = name;
                    this.DgvFull.Columns.Add(clm);

                    name = "Rows";
                    clm = new DataGridViewTextBoxColumn();
                    clm.Name = name;
                    clm.HeaderText = name;
                    this.DgvFull.Columns.Add(clm);


                    name = "Show";
                    btn = gridButton.SetButton(name);
                    this.DgvFull.Columns.Add(btn);

                    name = "Delete";
                    btn = gridButton.SetButton(name);
                    this.DgvFull.Columns.Add(btn);

                }

                sql = "SHOW TABLES";
                countRows = false;
                List<string> dataTables = executeQuery.GetData(sql, countRows, connectionString);

                foreach (string item in dataTables)
                {
                    tableName = item.ToString();
                    countRows = true;
                    sql = "SELECT * FROM " + tableName;

                    List<string> dataAlerts = executeQuery.GetData(sql, countRows, connectionString);
                    DgvFull.Rows.Add(tableName, dataAlerts.Last().ToString());
                }
            }
            else
            {
                lblTitle.Text = "Table: " + CurrentTableName;

                countRows = false;
                sql = "SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '" + dashboard.DatabaseName + "' AND TABLE_NAME = '" + CurrentTableName + "' ORDER BY ORDINAL_POSITION ASC";
                List<string> columns = executeQuery.GetData(sql, countRows, connectionString);


                int countColumns = 0;
                foreach (string item in columns)
                {
                    clm = new DataGridViewTextBoxColumn();
                    clm.Name = item;
                    clm.HeaderText = item;
                    this.DgvFull.Columns.Add(clm);
                    countColumns++;
                }

                name = "Show";
                btn = gridButton.SetButton(name);
                this.DgvFull.Columns.Add(btn);

                name = "Delete";
                btn = gridButton.SetButton(name);
                this.DgvFull.Columns.Add(btn);

                countRows = false;
                sql = "SELECT * FROM " + CurrentTableName;
                List<string> dataAlerts = executeQuery.GetData(sql, countRows, connectionString);

                int i = 0;

                List<string> listRow = new List<string> { };

                for (int x = 0; x < dataAlerts.Count; x++)
                {
                    if ((i % countColumns) != (countColumns - 1))
                    {
                        listRow.Add(dataAlerts[x].ToString());
                    }
                    else
                    {
                        //Voeg toe aan lijst maar reset hem daarna. 
                        listRow.Add(dataAlerts[x].ToString());
                        DgvFull.Rows.Add(listRow.ToArray());
                        listRow.Clear();
                    }
                    i++;
                }
            }

        }

        private void DgvFull_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowNumber = e.RowIndex;
            DataGridViewRow selectedRow = DgvFull.Rows[rowNumber];

            if (e.ColumnIndex == DgvFull.Columns["btnShow"].Index)
            {
                if(IsDatabase == true)
                {
                    Console.WriteLine("Show button clicked = " + selectedRow.Cells["Tables"].Value);
                    CurrentTableName = selectedRow.Cells["Tables"].Value.ToString();
                    DatabaseName = dashboard.DatabaseName;

                    IsDatabase = false;

                    DgvFull.Rows.Clear();
                    DgvFull.Columns.Clear();
                    DgvFull.Refresh();

                    Data_Load(null, EventArgs.Empty);
                }
                else
                {
                    Console.WriteLine("Show button clicked = " + selectedRow.Cells[0].Value);

                }

            }

            //if (e.ColumnIndex == DgvFull.Columns["btnDelete"].Index)
            //{
            //    Console.WriteLine("Delete button clicked = " + selectedRow.Cells["Tables"].Value);
            //}
        }

        public void lblClicked(object sender, EventArgs e)
        {
            dashboard.DatabaseName = (sender as Label).Text;

            DgvFull.Rows.Clear();
            DgvFull.Columns.Clear();
            DgvFull.Refresh();

            IsDatabase = true;

            Data_Load(null, EventArgs.Empty);
        }
    }
}
