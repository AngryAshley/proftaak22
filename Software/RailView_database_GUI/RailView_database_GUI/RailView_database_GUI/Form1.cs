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
    public partial class Form1 : Form
    {
        connection conn = new connection();
        public Form1()
        {
            InitializeComponent();
            conn.OpenConection();
            conn.CloseConnection();
        }
    }
}
