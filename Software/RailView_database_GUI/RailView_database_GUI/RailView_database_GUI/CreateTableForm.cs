using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class CreateTableForm : Form
    {
        DatabaseSelected databaseSelected = null;

        public CreateTableForm(DatabaseSelected c_databaseSelected)
        {
            InitializeComponent();

            databaseSelected = c_databaseSelected;

            lblTitle.Text = databaseSelected.NewTableName;

            int txbNameLocationX = 40;
            int cobTypeLocationX = 150;
            int txbLengthLocationX = 280;
            int cobDefaultLocationX = 390;
            int btnLocationX = 40;
            int locationY = 80;

            for (int i = 0; i < Convert.ToInt32(databaseSelected.AmountRowsNew); i++)
            {
                //AddTextBox(numbers[i], "Name", txbNameLocationX, locationY);
                //AddComboBox(numbers[i], "Type", cobTypeLocationX, locationY);
                //AddTextBox(numbers[i], "Lenght", txbLengthLocationX, locationY);
                //AddComboBox(numbers[i], "Default", cobDefaultLocationX, locationY);

                locationY = locationY + 25;
            }

            // SetButton --> Deze maakt dus de Tabel aan met de SQL Query
            AddButton(btnLocationX, locationY);

        }

        public void AddTextBox(string rowNumber, string name, int locationX, int locationY)
        {
            TextBox txb = new TextBox();
            txb.Name = "txb" + name + "Row" + rowNumber;
            txb.Location = new Point(locationX, locationY);
            this.Controls.Add(txb);
            txb.BringToFront();
        }

        public void AddComboBox(string rowNumber, string name, int locationX, int locationY)
        {
            ComboBox cob = new ComboBox();
            cob.Name = "cob" + name + "Row" + rowNumber;
            cob.Location = new Point(locationX, locationY);

            if (name == "Type")
            {
                cob.Items.Add("INT");
                cob.Items.Add("VARCHAR");
                cob.Items.Add("TEXT");
                cob.Items.Add("DOUBLE");
                cob.Items.Add("BOOLEAN");
            }
            else
            {
                cob.Items.Add("None");
                cob.Items.Add("NULL");
            }

            cob.DropDownStyle = ComboBoxStyle.DropDownList;
            cob.BringToFront();
            this.Controls.Add(cob);
        }

        public void AddButton(int locationX, int locationY)
        {
            Button btn = new Button();
            btn.Name = "btnAddTable";
            btn.Text = "Add";
            btn.Location = new Point(locationX, locationY);

            btn.TabStop = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(33, 115, 91);
            btn.ForeColor = Color.White;

            btn.BringToFront();
            btn.Click += (s, e) => { ExecuteTableQuery(); };
            this.Controls.Add(btn);
        }

        public void ExecuteTableQuery()
        {
            string query;

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    Console.WriteLine(c.Text);
                    // Make list with te texts
                }

                //if (c.GetType() != typeof(ComboBox))
                //{
                //    Console.WriteLine(c.);

                //}
            }

            //List.Reverse() 

            //for (int i = 0; i < Convert.ToInt32(databaseSelected.AmountRowsNew); i++)
            //{
            //    //Query = $"({txbNameRowOne.Text} )";
            //    Console.WriteLine(txbNameRowOne.Text);
            //}
        }
    }
}
