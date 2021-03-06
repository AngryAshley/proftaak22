using System;
using System.Drawing;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class CreateTableForm : Form
    {
        int i = 1;
        private string username;
        private string password;
        private string newTableName;
        private string connectionString;

        public string Username { get { return username; } }
        public string Password { get { return password; } }
        public string NewTableName { get { return newTableName; } }
        public string ConnectionString { get { return connectionString; } }

        public CreateTableForm(string username, string password, string newTableName, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.newTableName = newTableName;
            this.connectionString = connectionString;
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
            string primaryKey = null;
            string name = null;

            bool pkChecked = false;
            bool aiChecked = false;

            if (chbPK.Checked)
            {
                pkChecked = true;
            }

            if (chbAI.Checked)
            {
                aiChecked = true;
            }

            //check if leng/values ingevuld zijn 

            foreach (Control ctr in tlpFull.Controls)
            {
                if (ctr.Name != "chbPK")
                {
                    if (ctr.Name != "chbAI")
                    {
                        if (j == 1)
                        {
                            tempString = "`" + ctr.Text + "` ";
                            name = ctr.Text;
                        }
                        else if (j == 2)
                        {
                            tempString = tempString + ctr.Text;
                        }
                        else if (j == 3)
                        {
                            if (prev == "TIMESTAMP")
                            {
                                tempString = tempString + " ";
                            }
                            else if (ctr.Text == "")
                            {
                                MessageBox.Show("Please enter a valid length!", "Error", MessageBoxButtons.OK);
                                error = true;
                                break;
                            }
                            else
                            {
                                tempString = tempString + "(" + ctr.Text + ") ";
                            }
                        }
                        else
                        {
                            // default
                            if (ctr.Text == "NONE" || ctr.Text == "")
                            {
                                tempString = tempString + "NOT NULL";
                            }
                            else if (ctr.Text == "CURRENT_TIMESTAMP")
                            {
                                tempString = tempString + "NOT NULL DEFAULT " + ctr.Text;
                            }
                            else
                            {
                                tempString = tempString + ctr.Text + " DEFAULT NULL";
                            }
                        }

                        if (pkChecked)
                        {
                            //add pk to tempstring
                            primaryKey = ", PRIMARY KEY (`" + name + "`)";
                            pkChecked = false;
                        }


                        if (j == 4)
                        {
                            if (aiChecked)
                            {
                                //add ai to tempstring
                                tempString = tempString + " AUTO_INCREMENT, ";
                                aiChecked = false;
                            }
                            else
                            {
                                tempString = tempString + ", ";
                            }

                            fullString = fullString + tempString;
                            tempString = "";
                            j = 1;
                        }
                        else
                        {
                            j++;
                        }
                    }
                }

                prev = ctr.Text;
            }

            if (error == false)
            {
                fullString = fullString.Remove(fullString.Length - 2, 2);
                ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);

                string sql = "CREATE TABLE " + NewTableName + " (" + fullString + primaryKey + ") ENGINE = InnoDB;";
                bool errorQuery = executeQuery.SimpleExecute(sql);

                if (errorQuery == false)
                {
                    this.Hide();
                }

            }

        }

        private void btnDeleteLastRow_Click(object sender, EventArgs e)
        {
            if (tlpFull.RowCount > 1)
            {
                int countControls = tlpFull.Controls.Count - 5;

                for (int i = tlpFull.Controls.Count - 1; i > countControls; i--)
                {
                    tlpFull.Controls[i].Dispose();
                }

                tlpFull.RowStyles.RemoveAt(tlpFull.RowCount - 1);
                tlpFull.RowCount--;
                tlpFull.Height -= 25;
            }
            else
            {
                MessageBox.Show("Can't delete first row", "Error", MessageBoxButtons.OK);
            }
        }
    }
}