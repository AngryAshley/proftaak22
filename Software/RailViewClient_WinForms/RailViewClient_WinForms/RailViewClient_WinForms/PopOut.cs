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
using System.IO;
using System.Drawing.Imaging;

namespace RailViewClient_WinForms
{
    public partial class PopoutForm : Form
    {
        ClientForm clientForm;
        Timer StreamTimer = new Timer();
        int cameraId;

        public PopoutForm(ClientForm clientForm, int cameraId)
        {
            InitializeComponent();

            this.clientForm = clientForm;
            this.cameraId = cameraId;

            lblCameraName.Text = clientForm.CamName;

            StreamTimer.Enabled = true;
            StreamTimer.Interval = 3000;
            StreamTimer.Tick += StreamTimer_Tick;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        private void StreamTimer_Tick(object sender, EventArgs e)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(clientForm.StreamLink);
            request.Method = "HEAD";

            bool exists;
            try
            {
                request.GetResponse();
                exists = true;
            }
            catch
            {
                exists = false;
            }

            if (exists == true)
            {
                pictureBox1.InitialImage = null;
                pictureBox1.ImageLocation = clientForm.StreamLink;
            }         
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
