using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailView_database_GUI
{
    class GridButton
    {
        public string ButtonName { get; set; }

        public GridButton() : this("Standaard")
        {
        }

        public GridButton(string buttonName)
        {
            ButtonName = buttonName;
        }

        public DataGridViewButtonColumn SetButton(string name)
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = "btn" + name;
            btn.HeaderText = name;
            btn.Text = name;
            btn.UseColumnTextForButtonValue = true;

            return btn;
        }
    }
}
