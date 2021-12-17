
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.tp_panel = new System.Windows.Forms.Panel();
            this.logo_pb = new System.Windows.Forms.PictureBox();
            this.logo_lbl = new System.Windows.Forms.Label();
            this.lft_panel = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btm_panel = new System.Windows.Forms.Panel();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button10 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tp_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).BeginInit();
            this.lft_panel.SuspendLayout();
            this.btm_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.gmap.Size = new System.Drawing.Size(1224, 626);
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
            this.lft_panel.Controls.Add(this.listBox1);
            this.lft_panel.Controls.Add(this.label1);
            this.lft_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lft_panel.Location = new System.Drawing.Point(0, 63);
            this.lft_panel.Name = "lft_panel";
            this.lft_panel.Padding = new System.Windows.Forms.Padding(20);
            this.lft_panel.Size = new System.Drawing.Size(334, 936);
            this.lft_panel.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(20, 86);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(292, 828);
            this.listBox1.TabIndex = 3;
            this.listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstChoices_DrawItem);
            this.listBox1.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstChoices_MeasureItem);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 15.81818F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 66);
            this.label1.TabIndex = 2;
            this.label1.Text = "             Alerts\r\n \r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btm_panel
            // 
            this.btm_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btm_panel.Controls.Add(this.tabControl1);
            this.btm_panel.Controls.Add(this.panel2);
            this.btm_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btm_panel.Location = new System.Drawing.Point(334, 791);
            this.btm_panel.Margin = new System.Windows.Forms.Padding(0);
            this.btm_panel.Name = "btm_panel";
            this.btm_panel.Padding = new System.Windows.Forms.Padding(5);
            this.btm_panel.Size = new System.Drawing.Size(1326, 208);
            this.btm_panel.TabIndex = 4;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.White;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(0);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(1015, 161);
            this.txtConsole.TabIndex = 0;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(142, 43);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(123, 38);
            this.button6.TabIndex = 5;
            this.button6.Text = "Refresh SQL";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Reset Map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(3, 132);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 33);
            this.button5.TabIndex = 4;
            this.button5.Text = "Show Stations";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(6, 157);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 33);
            this.button4.TabIndex = 3;
            this.button4.Text = "Show Trains";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(3, 43);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(133, 38);
            this.button3.TabIndex = 2;
            this.button3.Text = "Position request";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(3, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 34);
            this.button2.TabIndex = 1;
            this.button2.Text = "Pop Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gmap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(334, 63);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(50);
            this.panel1.Size = new System.Drawing.Size(1326, 728);
            this.panel1.TabIndex = 5;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(110, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(104, 34);
            this.button8.TabIndex = 7;
            this.button8.Text = "Refresh Alert";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(171, 157);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(108, 33);
            this.button7.TabIndex = 6;
            this.button7.Text = "Pause Timer";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(220, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(91, 34);
            this.button9.TabIndex = 8;
            this.button9.Text = "test alert";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(105, 9);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(87, 22);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(204, 5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 30);
            this.button10.TabIndex = 2;
            this.button10.Text = "Change";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Update Timer:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1029, 196);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtConsole);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1021, 167);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Console";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button9);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1021, 167);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Control";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button10);
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1034, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 196);
            this.panel2.TabIndex = 4;
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
    }
}

