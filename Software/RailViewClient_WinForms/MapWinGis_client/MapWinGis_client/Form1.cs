using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxMapWinGIS;
using MapWinGIS;

namespace MapWinGis_client
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            ResetMap();
        }

        public void ResetMap()
        {
            railMap.KnownExtents = tkKnownExtents.keNetherlands;
            railMap.CurrentZoom = 8;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            railMap.CursorMode = MapWinGIS.tkCursorMode.cmPan;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            railMap.CursorMode = MapWinGIS.tkCursorMode.cmZoomOut;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ResetMap();
        }


    }
}
