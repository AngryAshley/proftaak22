﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class CreateTableForm : Form
    {
        Data data = null;
        int i = 1;
        
        public CreateTableForm(Data c_data)
        {
            InitializeComponent();
            data = c_data;
        }

        private void CreateTableForm_Load(object sender, EventArgs e)
        {
            tlpFull.ColumnCount = 6;
            tlpFull.RowCount = 1;
        }


        private void btnAddRow_Click(object sender, EventArgs e)
        {
            tlpFull.Height = tlpFull.Height + 25;
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

            txbName.Size = new Size(111, 20);
            txbLenVal.Size = new Size(111, 20);


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

            int j = 1;
            string fullString = "";
            string tempString = "";
            bool error = false; 

            string prev = null;
            string name = null;

            //check if leng/values ingevuld zijn 

            foreach (Control ctr in tlpFull.Controls)
            {
                // check if primary key == ja dan doe naam + INT PRIMARY KEY IDENTITY (1, 1)

                if(ctr.Name == "chbPK")
                {
                    if((ctr as CheckBox).Checked)
                    {
                        
                    }
                }
                else if (ctr.Name == "chbAI")
                {
                    if ((ctr as CheckBox).Checked)
                    {
                        
                    }
                }
                else
                {
                    if (j == 1)
                    {
                        //de naam van de tabel 
                        tempString = "`" + ctr.Text + "` ";
                        name = ctr.Text;
                    }
                    else if (j == 2)
                    {
                        //het type 
                        if (ctr.Text == "TIMESTAMP")
                        {
                            tempString = tempString + ctr.Text;
                        }
                        else
                        {
                            tempString = tempString + ctr.Text;
                        }
                    }
                    else if (j == 3)
                    {
                        // hoeveel letters(number)
                        if (ctr.Text == "")
                        {
                            //error and finish
                            MessageBox.Show("Please enter a valid length!", "Error", MessageBoxButtons.OK);
                            error = true;
                            break;
                        }
                        else if (prev == "TIMESTAMP")
                        {
                            tempString = tempString + " " + ctr.Text;
                        }
                        else
                        {
                            tempString = tempString + " (" + ctr.Text + ") ";
                        }

                    }
                    else
                    {
                        // default
                        if (ctr.Text == "NONE" || ctr.Text == "")
                        {
                            tempString = tempString + "NOT NULL, ";
                        }
                        else if (ctr.Text == "CURRENT_TIMESTAMP")
                        {
                            tempString = tempString + "NOT NULL DEFAULT " + ctr.Text + ", ";
                        }
                        else
                        {
                            tempString = tempString + ctr.Text + ", ";
                        }
                    }

                    if (j == 4)
                    {
                        j = 1;
                        fullString = fullString + tempString;
                        tempString = "";
                    }
                    else
                    {
                        j++;
                    }

                    prev = ctr.Text;
                }
            }

            if(error == false)
            {
                fullString = fullString.Remove(fullString.Length - 2, 2);
                string connectionString = "Server=192.168.161.205;Port=3306;Database=" + data.DatabaseName + ";Uid=admin;Pwd=TopMaster99;Convert Zero Datetime=true;";
                ExecuteQuery executeQuery = new ExecuteQuery(connectionString);

                string sql = "CREATE TABLE " + data.NewTableName + " (" + fullString + ") ENGINE = InnoDB;";
                //executeQuery.SimpleExecute(sql);

                this.Hide();
            }

        }
    }
}
