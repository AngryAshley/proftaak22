using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace RailView_database_GUI
{
    public partial class DatabaseSelected : Form
    {
        ExecuteQuery executeQuery = new ExecuteQuery();
        public string Table;
        public string NewTableName;
        public string AmountRowsNew;
        string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;";

        public DatabaseSelected()
        {
            InitializeComponent();
            string Name;
            string TableName;

            Name = "Show";
            SetButton(Name);

            Name = "Delete";
            SetButton(Name);

            string sql = "SHOW TABLES";
            bool countRows = false;
            List<string> DataTables = executeQuery.ShowDatabase(sql, countRows, connectionString);

            foreach (string item in DataTables)
            {
                TableName = item.ToString();
                List<string> DataAlerts = GetScript(connectionString, TableName);                 
                DgvFull.Rows.Add(TableName, DataAlerts.Last().ToString());
            }
        }

        private void DgvFull_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowNumber = e.RowIndex;
            DataGridViewRow SelectedRow = DgvFull.Rows[RowNumber];

            if (e.ColumnIndex == DgvFull.Columns["btnShow"].Index)
            {
                Console.WriteLine("Show button clicked = " + SelectedRow.Cells["clmTables"].Value);
                string TableName = SelectedRow.Cells["clmTables"].Value.ToString();
                GetDatabaseForm(TableName);
            }

            if (e.ColumnIndex == DgvFull.Columns["btnDelete"].Index)
            {
                 Console.WriteLine("Delete button clicked = " + SelectedRow.Cells["clmTables"].Value);
            }
        }

        public void SetButton(string Name)
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.Name = "btn" + Name;
            button.HeaderText = Name;
            button.Text = Name;
            button.UseColumnTextForButtonValue = true;
            this.DgvFull.Columns.Add(button);
        }

        public List<string> GetScript(string connectionString, string tableName)
        {
            string sql = "SELECT * FROM " + tableName;
            bool countRows = true;

            List<string> DataAlerts = executeQuery.ShowDatabase(sql, countRows, connectionString);

            return DataAlerts;
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

        public void GetDatabaseForm(string tableName)
        {
            Table = tableName;
            TableSelected tableSelected = new TableSelected(this);
            this.Hide();
            tableSelected.ShowDialog();
        }

    }
}
