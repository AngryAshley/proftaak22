
namespace RailViewClient_WinForms
{
    partial class PopoutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopoutForm));
            this.tp_panel = new System.Windows.Forms.Panel();
            this.logo_pb = new System.Windows.Forms.PictureBox();
            this.logo_lbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAlert = new System.Windows.Forms.Button();
            this.btnFalseAlert = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tp_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tp_panel
            // 
            this.tp_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.tp_panel.Controls.Add(this.logo_pb);
            this.tp_panel.Controls.Add(this.logo_lbl);
            this.tp_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tp_panel.Location = new System.Drawing.Point(0, 0);
            this.tp_panel.Name = "tp_panel";
            this.tp_panel.Size = new System.Drawing.Size(1050, 63);
            this.tp_panel.TabIndex = 3;
            // 
            // logo_pb
            // 
            this.logo_pb.Image = ((System.Drawing.Image)(resources.GetObject("logo_pb.Image")));
            this.logo_pb.Location = new System.Drawing.Point(14, 12);
            this.logo_pb.Name = "logo_pb";
            this.logo_pb.Size = new System.Drawing.Size(44, 42);
            this.logo_pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo_pb.TabIndex = 0;
            this.logo_pb.TabStop = false;
            // 
            // logo_lbl
            // 
            this.logo_lbl.AutoSize = true;
            this.logo_lbl.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logo_lbl.ForeColor = System.Drawing.Color.White;
            this.logo_lbl.Location = new System.Drawing.Point(55, 16);
            this.logo_lbl.Name = "logo_lbl";
            this.logo_lbl.Size = new System.Drawing.Size(129, 35);
            this.logo_lbl.TabIndex = 1;
            this.logo_lbl.Text = "RailView";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAlert);
            this.panel1.Controls.Add(this.btnFalseAlert);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 627);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 56);
            this.panel1.TabIndex = 4;
            // 
            // btnAlert
            // 
            this.btnAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAlert.Location = new System.Drawing.Point(353, 8);
            this.btnAlert.Name = "btnAlert";
            this.btnAlert.Size = new System.Drawing.Size(208, 42);
            this.btnAlert.TabIndex = 0;
            this.btnAlert.Text = "Alert";
            this.btnAlert.UseVisualStyleBackColor = false;
            this.btnAlert.Click += new System.EventHandler(this.btnAlert_Click);
            // 
            // btnFalseAlert
            // 
            this.btnFalseAlert.BackColor = System.Drawing.Color.Lime;
            this.btnFalseAlert.Location = new System.Drawing.Point(567, 8);
            this.btnFalseAlert.Name = "btnFalseAlert";
            this.btnFalseAlert.Size = new System.Drawing.Size(208, 42);
            this.btnFalseAlert.TabIndex = 1;
            this.btnFalseAlert.Text = "False Alert";
            this.btnFalseAlert.UseVisualStyleBackColor = false;
            this.btnFalseAlert.Click += new System.EventHandler(this.btnFalseAlert_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.webBrowser1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20);
            this.panel2.Size = new System.Drawing.Size(1050, 564);
            this.panel2.TabIndex = 5;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(20, 20);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(1010, 524);
            this.webBrowser1.TabIndex = 0;
            // 
            // PopoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 683);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tp_panel);
            this.Name = "PopoutForm";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PopoutForm_FormClosing);
            this.tp_panel.ResumeLayout(false);
            this.tp_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tp_panel;
        private System.Windows.Forms.PictureBox logo_pb;
        private System.Windows.Forms.Label logo_lbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnFalseAlert;
        private System.Windows.Forms.Button btnAlert;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}