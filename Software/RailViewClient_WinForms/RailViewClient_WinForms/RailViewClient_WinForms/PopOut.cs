using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailViewClient_WinForms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
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
    }
}
