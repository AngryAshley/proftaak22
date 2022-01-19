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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var embed = "<html><head>" +
            "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=Edge\"/>" +
            "</head><body scroll=\"no\" style=\"padding:0px;margin:0px;\">" +
            "<iframe style=\"border: 0px;\" width=\"100%\" src=\"{0}\"" +
            "frameborder = \"0\" allow = \"autoplay; encrypted-media\" allowfullscreen></iframe>" +
            "</body></html>";

            var url = "https://www.youtube.com/embed/FfEJhEVcK4Q?rel=0&autoplay=1;showinfo=0";
            this.webBrowser1.DocumentText = string.Format(embed, url);
        }

        private void btnFalseAlert_Click(object sender, EventArgs e)
        {
            this.Close();
            clientForm.FalseAlertClick(cameraId);
        }

        private void btnAlert_Click(object sender, EventArgs e)
        {
            this.Close();
            clientForm.AlertClick(cameraId);
        }

        private void PopoutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Close();
            clientForm.ClickOnce();
        }
    }
}
