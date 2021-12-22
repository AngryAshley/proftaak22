using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class CreateTableForm : Form
    {
        Data data = null;
        public CreateTableForm(Data c_data)
        {
            InitializeComponent();
            data = c_data;
        }

        private void CreateTableForm_Load(object sender, EventArgs e)
        {
            tlpFull.ColumnCount = 4;
            tlpFull.RowCount = 1;
        }


        private void btnAddRow_Click(object sender, EventArgs e)
        {
            tlpFull.Height = tlpFull.Height + 25;
            int i = 1;
            TextBox txbName = new TextBox();
            ComboBox cobType = new ComboBox();
            TextBox txbLenVal = new TextBox();
            ComboBox cobDefault = new ComboBox();
            txbName.Name = "txbName" + i;
            cobType.Name = "cobType" + i;
            txbLenVal.Name = "txbLenVal" + i;
            cobDefault.Name = "cobDefault" + i;

            cobType.DropDownStyle = ComboBoxStyle.DropDownList;
            cobDefault.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int j = 0; j < cobType0.Items.Count; j++)
            {
                cobType.Items.Add(cobType0.Items[j].ToString());
            }

            for (int j = 0; j < cobDefault0.Items.Count; j++)
            {
                cobDefault.Items.Add(cobDefault0.Items[j].ToString());
            }


            RowStyle temp = tlpFull.RowStyles[tlpFull.RowCount - 1];
            tlpFull.RowCount++;
            tlpFull.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));

            tlpFull.Controls.Add(txbName, 0, tlpFull.RowCount - 1);
            tlpFull.Controls.Add(cobType, 1, tlpFull.RowCount - 1);
            tlpFull.Controls.Add(txbLenVal, 2, tlpFull.RowCount - 1);
            tlpFull.Controls.Add(cobDefault, 3, tlpFull.RowCount - 1);
            i++;
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType().Equals(typeof(TableLayoutPanel)))
                {
                    foreach (Control ctr in tlpFull.Controls)
                    {
                        Console.WriteLine(ctr.Text);
                        // Maak hier een string aan en daarna uit de loop execute de query 
                    }

                }
            }
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
            //btn.Click += (s, e) => { ExecuteTableQuery(); };
            this.Controls.Add(btn);
        }


    }
}
