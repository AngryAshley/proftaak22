using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class TableSelected : Form
    {
        DatabaseSelected databaseSelected = null;
        GridButton gridButton = new GridButton();
        ExecuteQuery executeQuery = new ExecuteQuery();
        Navigation navigation = new Navigation();

        public TableSelected(DatabaseSelected c_databaseSelected)
        {
            InitializeComponent();
            databaseSelected = c_databaseSelected;
        }

        private void TableSelected_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=192.168.161.205;Port=3306;Database=" + databaseSelected.DatabaseName + ";Uid=admin;Pwd=TopMaster99;Convert Zero Datetime=true;";

            List<Control> myControls = navigation.AddNaviagtion();
            foreach (Control c in myControls)
            {
                c.Click += new EventHandler(MenuLabelClicked);
                this.Controls.Add(c);
                c.BringToFront();
            }

            bool countRows;
            string name;
            string sql;

            lblTitle.Text = lblTitle.Text + databaseSelected.Table;

            Console.WriteLine(databaseSelected.DatabaseName);

            countRows = false;
            sql = "SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '" + databaseSelected.DatabaseName + "' AND TABLE_NAME = '" + databaseSelected.Table + "' ORDER BY ORDINAL_POSITION ASC";
            List<string> columns = executeQuery.GetData(sql, countRows, connectionString);


            int countColumns = 0;
            foreach (string item in columns)
            {
                //DgvFull.Columns["clmFirst"].HeaderText = "Name of Table thing";

                DataGridViewTextBoxColumn clm = new DataGridViewTextBoxColumn();
                clm.Name = item; 
                clm.HeaderText = item;
                this.DgvFull.Columns.Add(clm);
                countColumns++;
            }

            countRows = false;
            sql = "SELECT * FROM " + databaseSelected.Table;
            List<string> dataAlerts = executeQuery.GetData(sql, countRows, connectionString);

            DataGridViewButtonColumn btn;

            name = "Delete";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);

            int i = 0;

            List<string> listRow = new List<string> { };

            Console.WriteLine(countColumns);
            Console.WriteLine(countColumns - 1);
            for (int x = 0; x < dataAlerts.Count; x++)
            {
                if (i % countColumns != countColumns - 1)
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

        public void MenuLabelClicked(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Hide();
            dashboard.RedirectDatabaseSelected(sender);
        }
    }
}
