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
        connection conn = new connection();
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

            conn.OpenConection(connectionString);
            List<string> DataTables = conn.ShowDatabase(sql, countRows);
            conn.CloseConnection();

            int chbLocationX = 165;
            int btnShowLocationX = 265;
            int btnDeleteLocationX = 365;
            int lblLocationX = 465;
            int LocationY = 130;

            foreach (string item in DataTables)
            {
                // Checkboxes
                setCheckBox(item, chbLocationX, LocationY);

                // Showbuttons
                string NameShow = "Show";
                setButton(item, NameShow, btnShowLocationX, LocationY);

                // Deleterbuttons
                string NameDelete = "Delete";
                setButton(item, NameDelete, btnDeleteLocationX, LocationY);


                List<string> DataAlerts = getScript(connectionString, item.ToString(), lblLocationX, LocationY);

                // Row labels
                setLabel(DataAlerts, lblLocationX, LocationY);

                lblBorderRows.Padding = new Padding(50, 0, 910, Convert.ToInt32(lblBorderRows.Padding.Bottom) + 25);
                LocationY = LocationY + 25;
            }
        }



        public List<string> getScript(string connectionString, string tableName, int lblLocationX ,int LocationY)
        {
            string sql = "SELECT * FROM " + tableName;
            bool countRows = true;

            conn.OpenConection(connectionString);
            List<string> DataAlerts = conn.ShowDatabase(sql, countRows);
            conn.CloseConnection();

            return DataAlerts;
        }

        public void setButton(string item, string Name, int LocationX, int LocationY)
        {
            Button btn = new Button();
            btn.Name = "btn" + Name + item.ToString();
            btn.Text = Name;
            btn.Location = new Point(LocationX, LocationY);
            this.Controls.Add(btn);
            btn.BringToFront();
        }

        public void setLabel(List<string> DataAlerts, int LocationX, int LocationY)
        {
            Label lbl = new Label();
            lbl.Name = "lbl" + DataAlerts.Last().ToString();
            lbl.Text = DataAlerts.Last().ToString();
            lbl.Location = new Point(LocationX, LocationY);
            this.Controls.Add(lbl);
            lbl.BringToFront();
        }

        public void setCheckBox(string item, int LocationX, int LocationY)
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

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {

                try
                {
                    //
                    // Open the SqlConnection.
                    //
                    con.Open();
                    //
                    // The following code uses an SqlCommand based on the SqlConnection.
                    //
                    using (MySqlCommand command = new MySqlCommand(sql, con))
                        command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //MySqlCommand command = new MySqlCommand(sql, conn);
            //command.ExecuteNonQuery();

            //conn.CreateTable(sql);
            //conn.CloseConnection();
        }
    }
}
