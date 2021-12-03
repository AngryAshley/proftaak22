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
        GridButton gridButton = new GridButton();
        public string Table;
        public string NewTableName;
        public string AmountRowsNew;
        readonly string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;";

        public DatabaseSelected()
        {
            InitializeComponent();
            string name;
            string tableName;
            bool countRows;
            DataGridViewButtonColumn btn;

            name = "Show";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);

            name = "Delete";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);

            string sql = "SHOW TABLES";
            countRows = false;
            List<string> dataTables = executeQuery.ShowDatabase(sql, countRows, connectionString);

            foreach (string item in dataTables)
            {
                tableName = item.ToString();
                countRows = true;

                sql = "SELECT * FROM " + tableName;
                List<string> dataAlerts = executeQuery.ShowDatabase(sql, countRows, connectionString);
                
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
                GetDatabaseForm(tableName);
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

        public void GetDatabaseForm(string tableName)
        {
            Table = tableName;
            TableSelected tableSelected = new TableSelected(this);
            this.Hide();
            tableSelected.ShowDialog();
        }

    }
}
