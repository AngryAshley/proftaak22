﻿using System;
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

            bool countRows;
            string name;
            string sql;
            databaseSelected = c_databaseSelected;

            lblTitle.Text = "Table: " + databaseSelected.Table;

            //string sql = "SELECT * FROM " + databaseSelected.Table;
            countRows = false;
            sql = "SELECT COLUMN_NAME FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = 'RailView' AND TABLE_NAME = '" + databaseSelected.Table + "'";
            List<string> columns = executeQuery.ShowDatabase(sql, countRows, connectionString);


            int countColumns = 0;
            foreach (string item in columns)
            {
                //DgvFull.Columns["clmFirst"].HeaderText = "Name of Table thing";

                DataGridViewColumn clm = new DataGridViewColumn();
                clm.Name = item;
                clm.HeaderText = item;
                this.DgvFull.Columns.Add(clm);
                countColumns++;
            }



            countRows = true;
            sql = "SELECT * FROM " + databaseSelected.Table;
            List<string> dataAlerts = executeQuery.ShowDatabase(sql, countRows, connectionString);

            DataGridViewButtonColumn btn;

            name = "Show";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);

            name = "Delete";
            btn = gridButton.SetButton(name);
            this.DgvFull.Columns.Add(btn);

            //int amountRows

            foreach (string item in dataAlerts)
            {
                for(int i = 0; i < countColumns; i++)
                {

                }

                DgvFull.Rows.Add();
            }
        }

        private void lblDatabase1_Click(object sender, EventArgs e)
        {
            DatabaseSelected databaseSelected = new DatabaseSelected();
            this.Hide();
            databaseSelected.ShowDialog();
        }
    }
}
