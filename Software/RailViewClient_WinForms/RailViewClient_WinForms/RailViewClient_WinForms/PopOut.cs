using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RailViewClient_WinForms.Classes;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Net;

namespace RailViewClient_WinForms
{
    public partial class PopoutForm : Form
    {
        ClientForm clientForm;
        int cameraId;

        public PopoutForm(ClientForm clientForm, int cameraId)
        {
            InitializeComponent();
            this.clientForm = clientForm;
            this.cameraId = cameraId;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        
            if (clientForm.IsStreaming == true)
            {
                pictureBox1.Load(clientForm.StreamLink);
            }         
        }

        private void btnFalseAlert_Click(object sender, EventArgs e)
        {
            this.Close();
            clientForm.FalseAlertClick(cameraId);
            clientForm.IsStreaming = false;
        }

        private void btnAlert_Click(object sender, EventArgs e)
        {
            this.Close();
            clientForm.AlertClick(cameraId);           
        }

        private void PopoutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            clientForm.ClickOnce();
            e.Cancel = true;
        }
    }
}
