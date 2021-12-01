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
        Connection conn = new Connection();
        ExecuteQuery executeQuery = new ExecuteQuery();
        public string Table;
        public string NewTableName;
        public string AmountRowsNew;

        string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;";

        public DatabaseSelected()
        {
            InitializeComponent();

            txbTableColumns.Text = "Number of columns";
            txbTableName.Text = "Name";
            btnAddTable.TabStop = false;
            btnAddTable.FlatStyle = FlatStyle.Flat;
            btnAddTable.FlatAppearance.BorderSize = 0;

            string sql = "SHOW TABLES";
            bool countRows = false;

            List<string> DataTables = executeQuery.ShowDatabase(sql, countRows, connectionString);

            int chbLocationX = 165;
            int btnShowLocationX = 265;
            int btnDeleteLocationX = 365;
            int lblLocationX = 465;
            int LocationY = 130;

            foreach (string item in DataTables)
            {
                // Checkboxes
                SetCheckBox(item, chbLocationX, LocationY);

                // Showbuttons
                string NameShow = "Show";
                SetButton(item, NameShow, btnShowLocationX, LocationY);

                // Deleterbuttons
                string NameDelete = "Delete";
                SetButton(item, NameDelete, btnDeleteLocationX, LocationY);

                List<string> DataAlerts = GetScript(connectionString, item.ToString(), lblLocationX, LocationY);

                // Row labels
                SetLabel(DataAlerts, lblLocationX, LocationY);

                lblBorderRows.Padding = new Padding(50, 0, 910, Convert.ToInt32(lblBorderRows.Padding.Bottom) + 25);
                LocationY = LocationY + 25;
            }
        }



        public List<string> GetScript(string connectionString, string tableName, int lblLocationX, int LocationY)
        {
            string sql = "SELECT * FROM " + tableName;
            bool countRows = true;

            List<string> DataAlerts = executeQuery.ShowDatabase(sql, countRows, connectionString);

            return DataAlerts;
        }

        public void SetButton(string item, string Name, int LocationX, int LocationY)
        {
            Button btn = new Button();
            btn.Name = "btn" + Name + item.ToString();
            btn.Text = Name;
            btn.Location = new Point(LocationX, LocationY);
            btn.Click += (s, e) => { GetDatabaseForm(item.ToString()); };
            this.Controls.Add(btn);
            btn.BringToFront();

        }

        public void SetLabel(List<string> DataAlerts, int LocationX, int LocationY)
        {
            Label lbl = new Label();
            lbl.Name = "lbl" + DataAlerts.Last().ToString();
            lbl.Text = DataAlerts.Last().ToString();
            lbl.Location = new Point(LocationX, LocationY);
            this.Controls.Add(lbl);
            lbl.BringToFront();
        }

        public void SetCheckBox(string item, int LocationX, int LocationY)
        {
            CheckBox chb = new CheckBox();
            chb.Name = "chb" + item.ToString();
            chb.Text = item.ToString();
            chb.Location = new Point(LocationX, LocationY);
            this.Controls.Add(chb);
            chb.BringToFront();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string sql = "CREATE TABLE " + txbTableName.Text + "(First_Name char(50));";
            executeQuery.CreateTable(sql, connectionString);

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
