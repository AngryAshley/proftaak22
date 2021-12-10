using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    class Navigation
    {
        ExecuteQuery executeQuery = new ExecuteQuery();
        string connectionString = "Server=192.168.161.205;Port=3306;Uid=admin;Pwd=TopMaster99;";

        public List<Control> AddNaviagtion()
        {
            List<Control> listOfControls = new List<Control>();

            int locationX = 10;
            int locationY = 140;

            bool countRows = false;
            string sql = "SHOW DATABASES";
            List<string> tables = executeQuery.GetData(sql, countRows, connectionString);


            for (int i = 0; i < tables.Count(); i++)
            {
                string name = tables[i].ToString();

                if(name != "mysql")
                {
                    Label lbl = new Label();
                    lbl.Text = name;
                    lbl.Name = "lbl" + name;
                    lbl.Location = new Point(locationX, locationY);
                    lbl.AutoSize = true;
                    locationY = locationY + 25;

                    listOfControls.Add(lbl);
                }
            }

            return listOfControls;
        }
    }
}
