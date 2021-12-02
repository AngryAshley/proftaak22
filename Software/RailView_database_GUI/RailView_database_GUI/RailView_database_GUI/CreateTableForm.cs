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
    public partial class CreateTableForm : Form
    {
        DatabaseSelected databaseSelected = null;
        List<string> Numbers = new List<string> { "Null", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen" };

        public CreateTableForm(DatabaseSelected c_databaseSelected)
        {
            InitializeComponent();

            databaseSelected = c_databaseSelected;
            Console.WriteLine(databaseSelected.NewTableName);
            Console.WriteLine(databaseSelected.AmountRowsNew);

            lblTitle.Text = databaseSelected.NewTableName;

            int txbNameLocationX = 40;
            int cobTypeLocationX = 150;
            int txbLengthLocationX = 280;
            int cobDefaultLocationX = 390;
            int btnLocationX = 40;
            int LocationY = 80;


            for (int i = 0; i < Convert.ToInt32(databaseSelected.AmountRowsNew); i++)
            {
                SetTextBox(Numbers[i], "Name", txbNameLocationX, LocationY);
                SetComboBox(Numbers[i], "Type", cobTypeLocationX, LocationY);
                SetTextBox(Numbers[i], "Lenght", txbLengthLocationX, LocationY);
                SetComboBox(Numbers[i], "Default", cobDefaultLocationX, LocationY);

                LocationY = LocationY + 25;
            }

            // SetButton --> Deze maakt dus de Tabel aan met de SQL Query
            SetButton(btnLocationX, LocationY);
        }

        public void SetTextBox(string RowNumber, string Name, int LocationX, int LocationY)
        {
            TextBox txb = new TextBox();
            txb.Name = "txb" + Name + "Row" + RowNumber;
            txb.Location = new Point(LocationX, LocationY);
            this.Controls.Add(txb);
            txb.BringToFront();
        }

        public void SetComboBox(string RowNumber, string Name, int LocationX, int LocationY)
        {
            ComboBox cob = new ComboBox();
            cob.Name = "cob" + Name + "Row" + RowNumber;
            cob.Location = new Point(LocationX, LocationY);

            if(Name == "Type")
            {
                cob.Items.Add("INT");
                cob.Items.Add("VARCHAR");
                cob.Items.Add("TEXT");
                cob.Items.Add("DOUBLE");
                cob.Items.Add("BOOLEAN");
            } else
            {
                cob.Items.Add("None");
                cob.Items.Add("NULL");
            }

            cob.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(cob);
            cob.BringToFront();
        }

        public void SetButton(int LocationX, int LocationY)
        {
            Button btn = new Button();
            btn.Name = "btnAddTable";
            btn.Text = "Add";
            btn.Location = new Point(LocationX, LocationY);
            btn.Click += (s, e) => { ExecuteTableQuery(); };
            btn.TabStop = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(33, 115, 91);
            btn.ForeColor = Color.White;
            this.Controls.Add(btn);
            btn.BringToFront();
        }

        public void ExecuteTableQuery()
        {
            string Query;

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
