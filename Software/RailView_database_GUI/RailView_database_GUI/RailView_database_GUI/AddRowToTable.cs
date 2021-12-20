using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    public partial class AddRowToTable : Form
    {
        Data data = null;
        public string ConnectionString;

        public AddRowToTable(Data c_data)
        {
            InitializeComponent();
            data = c_data;
        }

        private void AddRowToTable_Load(object sender, EventArgs e)
        {
            ConnectionString = "Server=192.168.161.205;Port=3306;Database=" + data.DatabaseName + ";Uid=admin;Pwd=TopMaster99;Convert Zero Datetime=true;";
            ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);
            Label lbl;
            TextBox txb;
            ComboBox cmb;
            NumericUpDown nup;

            bool countRows = false;

            string sql = "SELECT COLUMN_NAME, COLUMN_TYPE FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '" + data.DatabaseName + "' AND TABLE_NAME = '" + data.CurrentTableName + "' ORDER BY ORDINAL_POSITION ASC";
            List<string> columns = executeQuery.GetData(sql, countRows);

            int positionY = 30;
            int lblPositionX = 30;
            int typePositionX = 200;
            int txbPositionX = 400;
            int columnsAndTypesCount = 0;
            int valuesCount = 0;
            int column = 1;

            foreach (string item in columns)
            {
                if (column == 1)
                {
                    AddLabel(item, columnsAndTypesCount, "Columns", lblPositionX, positionY);
                    column++;
                    columnsAndTypesCount++;
                }
                else if (column == 2)
                {
                    AddLabel(item, columnsAndTypesCount, "Types", typePositionX, positionY);

                    if (item.ToString() == "timestamp")
                    {
                        AddLabel(item, valuesCount, "DontAdd", txbPositionX, positionY);
                    }
                    else if (item.Contains("enum"))
                    {
                        string enums;
                        enums = item.ToString();

                        enums = enums.Substring(4, enums.Length - 4);
                        char[] charsToTrim = { '(', ')', ',' };
                        enums = enums.Trim(charsToTrim);
                        var enumArr = enums.Split(',');

                        cmb = new ComboBox();
                        cmb.Name = "Values" + valuesCount;
                        cmb.Items.AddRange(enumArr);
                        cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb.Location = new Point(txbPositionX, positionY);
                        this.Controls.Add(cmb);
                        valuesCount++;
                    }
                    else if (item.Contains("int") || item.Contains("double"))
                    {
                        nup = new NumericUpDown();
                        nup.Name = "Values" + valuesCount;
                        nup.Location = new Point(txbPositionX, positionY);
                        nup.Size = new Size(120, 20);
                        this.Controls.Add(nup);
                        valuesCount++;
                    }
                    else
                    {
                        txb = new TextBox();
                        txb.Name = "Values" + valuesCount;
                        txb.Location = new Point(txbPositionX, positionY);
                        txb.Size = new Size(120, 20);
                        this.Controls.Add(txb);
                        valuesCount++;
                    }

                    column = 1;
                    positionY = positionY +25;
                }
            }
        }

        private void AddLabel(string item, int count, string name, int positionX, int positionY)
        {
            Label lbl = new Label();
            lbl.Name = name + count;
            lbl.Text = item;
            lbl.AutoSize = true;
            lbl.Location = new Point(positionX, positionY);
            this.Controls.Add(lbl);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int columnsCount = 0;
            int valuesCount = 0;
            int typesCount = 1;
            string values = "";
            string columns = "";

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    values = values + "'" + c.Text + "', ";
                    valuesCount++;
                }
                else
                {
                    if (c.Name == "Values" + valuesCount)
                    {
                        values = values + c.Text + ", ";
                        valuesCount++;
                    }
                    else if (c.Name == "Columns" + columnsCount)
                    {
                        string typeOfColumn = this.Controls.Find("Types" + typesCount, true)[0].Text;
                        typesCount++;

                        if (typeOfColumn != "timestamp")
                        {
                            columns = columns + c.Text + ", ";
                        }
                        columnsCount++;
                    }
                }

            }

            values = values.Remove(values.Length - 2, 2);
            columns = columns.Remove(columns.Length - 2, 2);

            string sql = "INSERT INTO " + data.CurrentTableName + " (" + columns + ") VALUES (" + values + ");";
            ExecuteQuery executeQuery = new ExecuteQuery(ConnectionString);
            executeQuery.SimpleExecute(sql);

            this.Hide();
        }
    }
}
