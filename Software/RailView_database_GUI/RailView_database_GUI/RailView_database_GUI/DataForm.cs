using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class DataForm : Form
    {
        // Fields & global variables
        Navigation navigation = new Navigation();
        private readonly string username;
        private readonly string password;
        private string databaseName;
        private string currentTableName;
        private string amountRowsNew;
        private string newTableName;
        private string connectionString;
        private string clmNameShowEdit;
        private string primaryKeyDataForSQL;
        private bool isEditEntity = false;
        bool isDatabase;

        // Properties
        public string Username { get { return username; } }
        public string Password { get { return password; } }
        public string DatabaseName { get { return databaseName; } set { databaseName = value; } }
        public string CurrentTableName { get { return currentTableName; } set { currentTableName = value; } } 
        public string AmountRowsNew { get { return amountRowsNew; } set { amountRowsNew = value; } }
        public string NewTableName { get { return newTableName; } set { newTableName = value; } }
        public string ConnectionString { get { return connectionString; } set { connectionString = value; } }
        public string ClmNameShowEdit { get { return clmNameShowEdit; } set { clmNameShowEdit = value; } }
        public string PrimaryKeyDataForSQL { get { return primaryKeyDataForSQL; } set { primaryKeyDataForSQL = value; } }
        public bool IsEditEntity { get { return isEditEntity; } set { isEditEntity = value; } }

        public DataForm(string username, string password, string databaseName)
        {
            InitializeComponent();
            isDatabase = true;
            this.username = username;
            this.password = password;
            this.databaseName = databaseName;
        }

        private void Data_Load(object sender, EventArgs e)
        {
            bool databaseIsGenerated = false;
            if (DatabaseName == "information_schema" || DatabaseName == "sys" || DatabaseName == "performance_schema")
            {
                databaseIsGenerated = true;
            }

            ConnectionString = "Server=192.168.161.205;Port=3306;Database=" + DatabaseName + ";Uid=" + Username + ";Pwd=" + Password + ";Convert Zero Datetime=true;";
            ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);
            string name;
            string sql;
            string tableName;
            bool countRows;
            DataGridViewTextBoxColumn clm;

            if (sender != null)
            {
                List<Control> myControls = navigation.AddNaviagtion(Username, Password);
                foreach (Control c in myControls)
                {
                    c.Click += new EventHandler(lblClicked);
                    this.Controls.Add(c);
                    c.BringToFront();
                }
            }

            if (isDatabase)
            {
                lblTitle.Text = "Database: " + DatabaseName;
                lblAddSomething.Text = "Add table to " + DatabaseName;

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

                if (databaseIsGenerated)
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
                sql = "SELECT COLUMN_NAME, COLUMN_KEY FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '" + DatabaseName + "' AND TABLE_NAME = '" + CurrentTableName + "' ORDER BY ORDINAL_POSITION ASC";
                List<string> columns = executeQuery.GetData(sql, countRows);


                int countColumns = 0;
                string prev = null;
                bool checkIfPriExists = false;
                foreach (string item in columns)
                {
                    string clmText = item;
                    if (item == "PRI")
                    {
                        DgvFull.Columns.RemoveAt(countColumns - 1);
                        clmText = prev + " " + item;
                        checkIfPriExists = true;
                    }
                    else if (item != "")
                    {
                        countColumns++;
                    }

                    clm = new DataGridViewTextBoxColumn();
                    clm.Name = clmText;
                    clm.HeaderText = clmText;
                    this.DgvFull.Columns.Add(clm);

                    prev = clmText;
                }

                for (int k = 0; k < DgvFull.Columns.Count; k++)
                {
                    if (DgvFull.Columns[k].HeaderText == "")
                    {
                        DgvFull.Columns.RemoveAt(k);
                    }
                }

                if (databaseIsGenerated)
                {
                    MakeFormInvisable();
                }
                else
                {
                    if (checkIfPriExists)
                    {
                        AddGridButtons();
                    }

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

            if (rowNumber >= 0)
            {
                DataGridViewRow selectedRow = DgvFull.Rows[rowNumber];

                if(DgvFull.Columns[DgvFull.ColumnCount - 1].HeaderText == "Delete" || DgvFull.Columns[DgvFull.ColumnCount - 2].HeaderText == "Show")
                {

                    if (e.ColumnIndex == DgvFull.Columns["btnDelete"].Index)
                    {
                        if (isDatabase)
                        {
                            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete table: " + selectedRow.Cells["Tables"].Value, "Sure?", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                string sql = "DROP TABLE " + selectedRow.Cells["Tables"].Value;
                                executeQuery.SimpleExecute(sql);
                                RefreshForm();
                            }
                        }
                        else
                        {
                            int selectedPKColumn = 0;

                            for (int k = 0; k < DgvFull.Columns.Count; k++)
                            {
                                if (DgvFull.Columns[k].HeaderText.Contains("PRI"))
                                {
                                    selectedPKColumn = k;
                                }
                            }

                            DataGridViewColumn clmPK = DgvFull.Columns[selectedPKColumn];
                            string headerText = clmPK.HeaderText.Remove(clmPK.HeaderText.Length - 4, 4);

                            string sql = "DELETE FROM " + CurrentTableName + " WHERE " + headerText + " = " + selectedRow.Cells[selectedPKColumn].Value + ";";

                            DialogResult dialogResult = MessageBox.Show("Do you really want to execute: " + sql, "Confirm", MessageBoxButtons.OKCancel);
                            if (dialogResult == DialogResult.OK)
                            {
                                executeQuery.SimpleExecute(sql);
                                RefreshForm();
                            }
                        }
                    }
                    else if (e.ColumnIndex == DgvFull.Columns["btnShow"].Index)
                    {
                        if (isDatabase)
                        {
                            CurrentTableName = selectedRow.Cells["Tables"].Value.ToString();
                            isDatabase = false;
                            RefreshForm();
                        }
                        else
                        {
                            int selectedPKColumn = 0;

                            for (int k = 0; k < DgvFull.Columns.Count; k++)
                            {
                                if (DgvFull.Columns[k].HeaderText.Contains("PRI"))
                                {
                                    selectedPKColumn = k;

                                }
                            }

                            ClmNameShowEdit = DgvFull.Columns[selectedPKColumn].HeaderText;
                            ClmNameShowEdit = ClmNameShowEdit.Remove(ClmNameShowEdit.Length - 4, 4);
                            PrimaryKeyDataForSQL = selectedRow.Cells[selectedPKColumn].Value.ToString();
                            IsEditEntity = true;

                            AddOrEditEntityForm editEntity = new AddOrEditEntityForm(Username, Password, ClmNameShowEdit, PrimaryKeyDataForSQL, IsEditEntity, DatabaseName, CurrentTableName, ConnectionString);
                            editEntity.ShowDialog();
                            RefreshForm();
                        }
                    }
                }
            }
        }

        public void lblClicked(object sender, EventArgs e)
        {
            DatabaseName = (sender as Label).Text;
            isDatabase = true;
            RefreshForm();
        }

        public void RefreshForm()
        {
            DgvFull.Rows.Clear();
            DgvFull.Columns.Clear();
            DgvFull.Refresh();

            Data_Load(null, EventArgs.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isDatabase)
            {
                if (txbTableName.Text == "")
                {
                    MessageBox.Show("Please enter a database name!", "Error", MessageBoxButtons.OK);
                }
                else
                {
                    bool isError = false;
                    ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);

                    string sql = "SHOW TABLES";
                    bool countRows = false;
                    List<string> dataTables = executeQuery.GetData(sql, countRows);

                    foreach (string item in dataTables)
                    {
                        if (item.ToString() == txbTableName.Text)
                        {
                            isError = true;
                        }
                    }

                    if (isError)
                    {
                        MessageBox.Show("This database name already exists!", "Error", MessageBoxButtons.OK);
                    }
                    else
                    {
                        NewTableName = txbTableName.Text;

                        CreateTableForm createTableForm = new CreateTableForm(Username, Password, NewTableName, ConnectionString);
                        createTableForm.ShowDialog();
                        RefreshForm();
                    }
                }
            }
            else
            {
                IsEditEntity = false;
                AddOrEditEntityForm addRowToTable = new AddOrEditEntityForm(Username, Password, ClmNameShowEdit, PrimaryKeyDataForSQL, IsEditEntity, DatabaseName, CurrentTableName, ConnectionString);
                addRowToTable.ShowDialog();
                RefreshForm();
            }
        }

        private void pibLogo_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashboardForm dashboard = new DashboardForm(Username, Password);
            dashboard.ShowDialog();
        }
    }
}