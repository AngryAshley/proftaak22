using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class Dashboard : Form
    {
        Navigation navigation = new Navigation();
        public string Username;
        public string Password;

        public Dashboard()
        {
            InitializeComponent();
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
            Data data = new Data();
            data.Username = Username;
            data.Password = Password;
            data.DatabaseName = (sender as Label).Text;
            data.ShowDialog();
        }
    }
}
