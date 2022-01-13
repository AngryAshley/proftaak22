using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class DashboardForm : Form
    {
        // Fileds
        Navigation navigation = new Navigation();
        private readonly string username;
        private readonly string password;
        private string databaseName;

        public string Username { get { return username; } }
        public string Password { get { return password; } }
        public string DatabaseName { get { return databaseName; } set { databaseName = value; } }

        public DashboardForm(string username, string password)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            List<Control> myControls = navigation.AddNaviagtion(Username, Password);
            foreach (Control c in myControls)
            {
                c.Click += new EventHandler(DatabaseClicked);
                this.Controls.Add(c);
                c.BringToFront();
            }
        }

        public void DatabaseClicked(object sender, EventArgs e)
        {
            this.Hide();
            DatabaseName = (sender as Label).Text;
            DataForm data = new DataForm(Username, Password, DatabaseName);
            data.ShowDialog();
        }
    }
}
