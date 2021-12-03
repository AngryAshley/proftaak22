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
    public partial class TableSelected : Form
    {
        DatabaseSelected databaseSelected = null;
        GridButton gridButton = new GridButton();
        ExecuteQuery executeQuery = new ExecuteQuery();
        string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;";

        public TableSelected(DatabaseSelected c_databaseSelected)
        {
            InitializeComponent();

            bool countRows = false;
            string name;
            databaseSelected = c_databaseSelected;

            lblTitle.Text = "Table: " + databaseSelected.Table;

            string sql = "SELECT * FROM " + databaseSelected.Table;
            List<string> dataAlerts = executeQuery.ShowDatabase(sql, countRows, connectionString);

            DataGridViewButtonColumn btn;

            name = "Show";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);

            name = "Delete";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);


            foreach (string item in dataAlerts)
            {
                DgvFull.Columns["clmFirst"].HeaderText = "Name of Table thing";

                DgvFull.Rows.Add(item);
            }
        }


    }
}
