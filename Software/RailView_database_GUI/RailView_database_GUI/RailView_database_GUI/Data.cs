using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class Data : Form
    {
        Navigation navigation = new Navigation();
        Dashboard dashboard = null;
        public string CurrentTableName;
        public string AmountRowsNew;
        public string DatabaseName;
        public string ConnectionString;
        bool IsDatabase;

        public Data(Dashboard c_dashboard)
        {
            InitializeComponent();
            dashboard = c_dashboard;
            IsDatabase = true;
        }

        private void Data_Load(object sender, EventArgs e)
        {
            bool noButtons = false;
            if(dashboard.DatabaseName == "information_schema" || dashboard.DatabaseName == "sys" || dashboard.DatabaseName == "performance_schema")
            {
                noButtons = true;
            }

            ConnectionString = "Server=192.168.161.205;Port=3306;Database=" + dashboard.DatabaseName + ";Uid=admin;Pwd=TopMaster99;Convert Zero Datetime=true;";
            ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);
            string name;
            string sql;
            string tableName;
            bool countRows;
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

            if (IsDatabase == true)
            {
                txbTableAmount.Visible = true;
                txbTableName.Visible = true;
                lblTitle.Text = "Database: " + dashboard.DatabaseName;

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

                if(noButtons == false)
                {
                    AddGridButtons();
                }

                sql = "SHOW TABLES";
                countRows = false;
                List<string> dataTables = executeQuery.GetData(sql, countRows);

                foreach (string item in dataTables)
                {
                    tableName = item.ToString();
                    countRows = true;
                    sql = "SELECT * FROM " + tableName;

                    List<string> dataAlerts = executeQuery.GetData(sql, countRows);
                    DgvFull.Rows.Add(tableName, dataAlerts.Last().ToString());
                }
            }
            else
            {
                lblAddSomething.Text = "Add entity to " + CurrentTableName;
                lblTitle.Text = "Table: " + CurrentTableName;

                txbTableAmount.Visible = false;
                txbTableName.Visible = false;


                countRows = false;
                sql = "SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '" + dashboard.DatabaseName + "' AND TABLE_NAME = '" + CurrentTableName + "' ORDER BY ORDINAL_POSITION ASC";
                List<string> columns = executeQuery.GetData(sql, countRows);


                int countColumns = 0;
                foreach (string item in columns)
                {
                    clm = new DataGridViewTextBoxColumn();
                    clm.Name = item;
                    clm.HeaderText = item;
                    this.DgvFull.Columns.Add(clm);
                    countColumns++;
                }

                if (noButtons == false)
                {
                    AddGridButtons();
                }

                countRows = false;
                sql = "SELECT * FROM " + CurrentTableName;
                List<string> dataAlerts = executeQuery.GetData(sql, countRows);

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

        private void AddGridButtons()
        {
            GridButton gridButtonShow = new GridButton("Show");
            this.DgvFull.Columns.Add(gridButtonShow.SetButton());

            GridButton gridButtonDelete = new GridButton("Delete");
            this.DgvFull.Columns.Add(gridButtonDelete.SetButton());
        }

        private void DgvFull_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);

            int rowNumber = e.RowIndex;

            if(rowNumber >= 0)
            {
                DataGridViewRow selectedRow = DgvFull.Rows[rowNumber];

                if (e.ColumnIndex == DgvFull.Columns["btnShow"].Index)
                {
                    if (IsDatabase == true)
                    {
                        CurrentTableName = selectedRow.Cells["Tables"].Value.ToString();
                        DatabaseName = dashboard.DatabaseName;

                        IsDatabase = false;

                        RefreshFrom();
                    }
                    else
                    {
                        Console.WriteLine("Show button clicked = " + selectedRow.Cells[0].Value);
                        //show/edit entity

                    }
                }

                if (e.ColumnIndex == DgvFull.Columns["btnDelete"].Index)
                {
                    if (IsDatabase == true)
                    {
                        DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete table: " + selectedRow.Cells["Tables"].Value, "Sure?", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            string sql = "DROP TABLE " + selectedRow.Cells["Tables"].Value;
                            executeQuery.SimpleExecute(sql);
                            RefreshFrom();
                        }
                    }
                    else
                    {
                        // Get the name id from table
                        DataGridViewColumn topRow = DgvFull.Columns[0];
                        string sql = "DELETE FROM " + CurrentTableName + " WHERE " + topRow.HeaderText + " = " + selectedRow.Cells[0].Value + ";";

                        DialogResult dialogResult = MessageBox.Show("Do you really want to execute: " + sql, "Confirm", MessageBoxButtons.OKCancel);
                        if (dialogResult == DialogResult.OK)
                        {
                            executeQuery.SimpleExecute(sql);
                            RefreshFrom();
                        }
                    }
                }
            }
        }

        public void lblClicked(object sender, EventArgs e)
        {
            dashboard.DatabaseName = (sender as Label).Text;

            IsDatabase = true;
            RefreshFrom();
        }

        public void RefreshFrom()
        {
            DgvFull.Rows.Clear();
            DgvFull.Columns.Clear();
            DgvFull.Refresh();

            Data_Load(null, EventArgs.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsDatabase == true)
            {

            }
            else
            {
                //add entity to current table 
                //open form with the entitys of the table
                Console.WriteLine("add entity current table: " + CurrentTableName);

                AddRowToTable addRowToTable = new AddRowToTable(this);
                addRowToTable.ShowDialog();
            }
        }
    }
}
