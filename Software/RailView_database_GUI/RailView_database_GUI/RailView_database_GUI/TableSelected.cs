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
    public partial class TableSelected : Form
    {
        DatabaseSelected databaseSelected = null;
        public TableSelected(DatabaseSelected c_databaseSelected)
        {
            InitializeComponent();

            databaseSelected = c_databaseSelected;
            Console.WriteLine(databaseSelected.Table);
        }
    }
}
