using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class DatabaseSelected : Form
    {
        ExecuteQuery executeQuery = new ExecuteQuery();
        GridButton gridButton = new GridButton();
        Navigation navigation = new Navigation();
        Dashboard dashboard = null;
        public string Table;
        public string NewTableName;
        public string AmountRowsNew;
        public string DatabaseName;

        public DatabaseSelected(Dashboard c_dashboard)
        {
            InitializeComponent();
            dashboard = c_dashboard;
        }

        private void DatabaseSelected_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.161.205;Port=3306;Database=" + dashboard.DatabaseName + ";Uid=admin;Pwd=TopMaster99;Convert Zero Datetime=true;";
            string name;
            string tableName;
            bool countRows;
            DataGridViewButtonColumn btn;

            if (sender != null)
            {
                List<Control> myControls = navigation.AddNaviagtion();
                foreach (Control c in myControls)
                {
                    c.Click += new EventHandler(lblClicked);
                    this.Controls.Add(c);
                    c.BringToFront();
                }

                name = "Show";
                btn = gridButton.SetButton(name);
                this.DgvFull.Columns.Add(btn);

                name = "Delete";
                btn = gridButton.SetButton(name);
                this.DgvFull.Columns.Add(btn);
            }

            string sql = "SHOW TABLES";
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

        private void DgvFull_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowNumber = e.RowIndex;
            DataGridViewRow selectedRow = DgvFull.Rows[rowNumber];

            if (e.ColumnIndex == DgvFull.Columns["btnShow"].Index)
            {
                Console.WriteLine("Show button clicked = " + selectedRow.Cells["clmTables"].Value);
                string tableName = selectedRow.Cells["clmTables"].Value.ToString();
                string databaseName = dashboard.DatabaseName;
                TableSelected(tableName, databaseName);
            }

            if (e.ColumnIndex == DgvFull.Columns["btnDelete"].Index)
            {
                Console.WriteLine("Delete button clicked = " + selectedRow.Cells["clmTables"].Value);
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            //string sql = "CREATE TABLE " + txbTableName.Text + "(First_Name char(50), );";
            //executeQuery.CreateTable(sql, connectionString);

            NewTableName = txbTableName.Text;
            AmountRowsNew = txbTableColumns.Text;
            CreateTableForm createTableForm = new CreateTableForm(this);
            createTableForm.ShowDialog();
        }

        public void TableSelected(string tableName, string databaseName)
        {
            Table = tableName;
            DatabaseName = databaseName;
            TableSelected tableSelected = new TableSelected(this);
            this.Hide();
            tableSelected.ShowDialog();
        }

        public void lblClicked(object sender, EventArgs e)
        {
            dashboard.DatabaseName = (sender as Label).Text;
            DgvFull.Rows.Clear();
            DgvFull.Refresh();
            DatabaseSelected_Load(null, EventArgs.Empty);
        }
    }
}
