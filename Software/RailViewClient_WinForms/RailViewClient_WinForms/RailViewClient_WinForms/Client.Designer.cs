
namespace RailViewClient_WinForms
{
    partial class Client
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.tp_panel = new System.Windows.Forms.Panel();
            this.logo_pb = new System.Windows.Forms.PictureBox();
            this.logo_lbl = new System.Windows.Forms.Label();
            this.lft_panel = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btm_panel = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timerPositions = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.tp_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).BeginInit();
            this.lft_panel.SuspendLayout();
            this.btm_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = true;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemory = 5;
            this.gmap.Location = new System.Drawing.Point(50, 50);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 2;
            this.gmap.MinZoom = 2;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(1333, 696);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick_1);
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            // 
            // tp_panel
            // 
            this.tp_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.tp_panel.Controls.Add(this.logo_pb);
            this.tp_panel.Controls.Add(this.logo_lbl);
            this.tp_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tp_panel.Location = new System.Drawing.Point(0, 0);
            this.tp_panel.Name = "tp_panel";
            this.tp_panel.Size = new System.Drawing.Size(1660, 63);
            this.tp_panel.TabIndex = 2;
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
            this.logo_lbl.Size = new System.Drawing.Size(140, 38);
            this.logo_lbl.TabIndex = 1;
            this.logo_lbl.Text = "RailView";
            // 
            // lft_panel
            // 
            this.lft_panel.BackColor = System.Drawing.Color.White;
            this.lft_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lft_panel.Controls.Add(this.richTextBox1);
            this.lft_panel.Controls.Add(this.label1);
            this.lft_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lft_panel.Location = new System.Drawing.Point(0, 63);
            this.lft_panel.Name = "lft_panel";
            this.lft_panel.Padding = new System.Windows.Forms.Padding(20);
            this.lft_panel.Size = new System.Drawing.Size(225, 936);
            this.lft_panel.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(20, 119);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(183, 795);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15.81818F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 99);
            this.label1.TabIndex = 2;
            this.label1.Text = "\r\n       Alerts\r\n \r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btm_panel
            // 
            this.btm_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btm_panel.Controls.Add(this.button4);
            this.btm_panel.Controls.Add(this.button3);
            this.btm_panel.Controls.Add(this.button2);
            this.btm_panel.Controls.Add(this.button1);
            this.btm_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btm_panel.Location = new System.Drawing.Point(225, 861);
            this.btm_panel.Name = "btm_panel";
            this.btm_panel.Size = new System.Drawing.Size(1435, 138);
            this.btm_panel.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial", 9.272727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(102, 55);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 33);
            this.button3.TabIndex = 2;
            this.button3.Text = "Get POS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 9.272727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(102, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 33);
            this.button2.TabIndex = 1;
            this.button2.Text = "Pop Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 9.272727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gmap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(225, 63);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(50);
            this.panel1.Size = new System.Drawing.Size(1435, 798);
            this.panel1.TabIndex = 5;
            // 
            // timerPositions
            // 
            this.timerPositions.Interval = 1000;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Arial", 9.272727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(6, 45);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 52);
            this.button4.TabIndex = 3;
            this.button4.Text = "Show Trains";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1660, 999);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btm_panel);
            this.Controls.Add(this.lft_panel);
            this.Controls.Add(this.tp_panel);
            this.Name = "Client";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tp_panel.ResumeLayout(false);
            this.tp_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).EndInit();
            this.lft_panel.ResumeLayout(false);
            this.lft_panel.PerformLayout();
            this.btm_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Panel tp_panel;
        private System.Windows.Forms.PictureBox logo_pb;
        private System.Windows.Forms.Label logo_lbl;
        private System.Windows.Forms.Panel lft_panel;
        private System.Windows.Forms.Panel btm_panel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timerPositions;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

