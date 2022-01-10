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
        public string NewTableName;
        public string DatabaseName;
        public string ConnectionString;
        bool isDatabase;

        public Data(Dashboard c_dashboard)
        {
            InitializeComponent();
            dashboard = c_dashboard;
            isDatabase = true;
        }

        private void Data_Load(object sender, EventArgs e)
        {
            bool databaseIsGenerated = false;
            if(dashboard.DatabaseName == "information_schema" || dashboard.DatabaseName == "sys" || dashboard.DatabaseName == "performance_schema")
            {
                databaseIsGenerated = true;
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

            if (isDatabase == true)
            {
                lblTitle.Text = "Database: " + dashboard.DatabaseName;
                lblAddSomething.Text = "Add table to " + dashboard.DatabaseName;

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

                if(databaseIsGenerated == true)
                {
                    GridButton gridButtonShow = new GridButton("Show");
                    this.DgvFull.Columns.Add(gridButtonShow.SetButton());
                    MakeFormInvisable();
                }
                else
                {
                    AddGridButtons();
                    txbTableName.Visible = true;
                    lblAddSomething.Visible = true;
                    lblBorderForm.Visible = true;
                    btnAdd.Visible = true;
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

                if (databaseIsGenerated == true)
                {
                    MakeFormInvisable();
                }
                else
                {
                    AddGridButtons();
                    lblAddSomething.Visible = true;
                    lblBorderForm.Visible = true;
                    btnAdd.Visible = true;
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

        private void MakeFormInvisable()
        {
            txbTableName.Visible = false;
            lblAddSomething.Visible = false;
            lblBorderForm.Visible = false;
            btnAdd.Visible = false;
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
                    if (isDatabase == true)
                    {
                        CurrentTableName = selectedRow.Cells["Tables"].Value.ToString();
                        DatabaseName = dashboard.DatabaseName;

                        isDatabase = false;
                        RefreshFrom();
                    }
                    else
                    {
                        Console.WriteLine("Show button clicked = " + selectedRow.Cells[0].Value);
                        //show/edit entity

                    }
                }
                else if (e.ColumnIndex == DgvFull.Columns["btnDelete"].Index)
                {
                    if (isDatabase == true)
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

            isDatabase = true;
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
            if (isDatabase == true)
            {
                Console.WriteLine("add Table to: " + dashboard.DatabaseName);
                if (txbTableName.Text == "")
                {
                    MessageBox.Show("Please enter a database name!", "Error", MessageBoxButtons.OK);
                }
                else
                {
                    NewTableName = txbTableName.Text;
                    DatabaseName = dashboard.DatabaseName;

                    CreateTableForm createTableForm = new CreateTableForm(this);
                    createTableForm.ShowDialog();
                }
            }
            else
            {
                //add entity to current table 
                //open form with the entitys of the table
                Console.WriteLine("add entity current table: " + CurrentTableName);

                AddRowToTable addRowToTable = new AddRowToTable(this);
                addRowToTable.ShowDialog();
                RefreshFrom();
            }
        }
    }
}
