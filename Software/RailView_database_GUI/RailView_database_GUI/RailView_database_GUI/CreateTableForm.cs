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

        public CreateTableForm(DatabaseSelected c_databaseSelected)
        {
            InitializeComponent();

            databaseSelected = c_databaseSelected;
            Console.WriteLine(databaseSelected.NewTableName);
            Console.WriteLine(databaseSelected.AmountRowsNew);

            for(int i = 0; i < Convert.ToInt32(databaseSelected.AmountRowsNew); i++)
            {
                // SetTextBox 
                // SetCombobox

                // SetButton --> Deze maakt dus de Tabel aan met de SQL Query
            }
        }
    }
}
