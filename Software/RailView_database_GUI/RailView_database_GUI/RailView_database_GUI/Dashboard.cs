using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class Dashboard : Form
    {
        Navigation navigation = new Navigation();
        public string DatabaseName { get; set; }

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            List<Control> myControls = navigation.AddNaviagtion();
            foreach (Control c in myControls)
            {
                c.Click += new EventHandler(DatabaseClicked);
                this.Controls.Add(c);
                c.BringToFront();
            }
        }

        public void DatabaseClicked(object sender, EventArgs e)
        {
            DatabaseName = (sender as Label).Text;
            Data data = new Data(this);
            this.Hide();
            data.ShowDialog();
        }

        public void RedirectDatabaseSelected(object sender)
        {
            DatabaseClicked(sender, EventArgs.Empty);
        }
    }
}
