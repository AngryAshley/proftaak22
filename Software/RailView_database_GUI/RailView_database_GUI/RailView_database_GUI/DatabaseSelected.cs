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
    public partial class DatabaseSelected : Form
    {
        connection conn = new connection();

        public DatabaseSelected()
        {
            InitializeComponent();

            txbTableColumns.Text = "Number of columns";
            txbTableName.Text = "Name";
            btnAddTable.TabStop = false;
            btnAddTable.FlatStyle = FlatStyle.Flat;
            btnAddTable.FlatAppearance.BorderSize = 0;

            string connectionString = "Server=192.168.161.205;Port=3306;Database=RailView;Uid=admin;Pwd=TopMaster99;";
            string sql = "SHOW TABLES";

            conn.OpenConection(connectionString);
            List<string> DataTables = conn.ShowDatabase(sql);
            conn.CloseConnection();

            int lblBorderPadding = 30;
            int countAmount = 0;
            int chbLocationX = 162;
            int chbLocationY = 131;

            foreach (string item in DataTables)
            {
                //Console.WriteLine($"Dit is een tabel in databaseSelected: {item.ToString()}");
                CheckBox chb = new CheckBox();

                chb.Name = "chb" + item.ToString();
                chb.Text = item.ToString();
                chb.Location = new Point(chbLocationX, chbLocationY);
                this.Controls.Add(chb);
                chb.BringToFront();


                lblBorderRows.Padding = new Padding(50, 0, 910, Convert.ToInt32(lblBorderRows.Padding.Bottom) + 20);
                
                chbLocationY = chbLocationY + 20;
                countAmount++;
            }

            sql = "SELECT * FROM alerts";

            conn.OpenConection(connectionString);
            List<string> DataAlerts = conn.ShowDatabase(sql);
            conn.CloseConnection();

            Console.WriteLine(DataAlerts.Count());
            Console.WriteLine(DataAlerts.Last());


            foreach (string item in DataAlerts)
            {
                //Console.WriteLine($"Dit is een alerts in databaseSelected: {item.ToString()}");
            }
        }
    }
}
