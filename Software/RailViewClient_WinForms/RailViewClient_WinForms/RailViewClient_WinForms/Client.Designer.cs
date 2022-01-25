
namespace RailViewClient_WinForms
{
    partial class ClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientForm));
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.tp_panel = new System.Windows.Forms.Panel();
            this.logo_pb = new System.Windows.Forms.PictureBox();
            this.logo_lbl = new System.Windows.Forms.Label();
            this.lft_panel = new System.Windows.Forms.Panel();
            this.listBoxAlerts = new System.Windows.Forms.ListBox();
            this.lblAlerts = new System.Windows.Forms.Label();
            this.btm_panel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_PopOut = new System.Windows.Forms.Button();
            this.btn_PauseAlerts = new System.Windows.Forms.Button();
            this.btn_RequestSQL = new System.Windows.Forms.Button();
            this.btnRequestPos = new System.Windows.Forms.Button();
            this.btnResetMap = new System.Windows.Forms.Button();
            this.btn_ShowStations = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btn_ChangeTimerInterval = new System.Windows.Forms.Button();
            this.nrUpDwn_TimerInterval = new System.Windows.Forms.NumericUpDown();
            this.btn_PauseTrains = new System.Windows.Forms.Button();
            this.btn_ShowTrains = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblZoom = new System.Windows.Forms.Label();
            this.tp_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).BeginInit();
            this.lft_panel.SuspendLayout();
            this.btm_panel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrUpDwn_TimerInterval)).BeginInit();
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
            this.gmap.Location = new System.Drawing.Point(51, 50);
            this.gmap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.gmap.Size = new System.Drawing.Size(1222, 626);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            this.gmap.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.gmap_OnMarkerClick);
            this.gmap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gmap_OnMapZoomChanged);
            this.gmap.Load += new System.EventHandler(this.gmap_Load);
            // 
            // tp_panel
            // 
            this.tp_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.tp_panel.Controls.Add(this.logo_pb);
            this.tp_panel.Controls.Add(this.logo_lbl);
            this.tp_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tp_panel.Location = new System.Drawing.Point(0, 0);
            this.tp_panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tp_panel.Name = "tp_panel";
            this.tp_panel.Size = new System.Drawing.Size(1660, 63);
            this.tp_panel.TabIndex = 2;
            // 
            // logo_pb
            // 
            this.logo_pb.Image = ((System.Drawing.Image)(resources.GetObject("logo_pb.Image")));
            this.logo_pb.Location = new System.Drawing.Point(13, 12);
            this.logo_pb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.lft_panel.Controls.Add(this.listBoxAlerts);
            this.lft_panel.Controls.Add(this.lblAlerts);
            this.lft_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.lft_panel.Location = new System.Drawing.Point(0, 63);
            this.lft_panel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lft_panel.Name = "lft_panel";
            this.lft_panel.Padding = new System.Windows.Forms.Padding(20);
            this.lft_panel.Size = new System.Drawing.Size(334, 936);
            this.lft_panel.TabIndex = 3;
            // 
            // listBoxAlerts
            // 
            this.listBoxAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxAlerts.FormattingEnabled = true;
            this.listBoxAlerts.ItemHeight = 16;
            this.listBoxAlerts.Location = new System.Drawing.Point(20, 86);
            this.listBoxAlerts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxAlerts.Name = "listBoxAlerts";
            this.listBoxAlerts.Size = new System.Drawing.Size(292, 828);
            this.listBoxAlerts.TabIndex = 3;
            this.listBoxAlerts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstChoices_DrawItem);
            this.listBoxAlerts.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstChoices_MeasureItem);
            // 
            // lblAlerts
            // 
            this.lblAlerts.AutoSize = true;
            this.lblAlerts.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAlerts.Font = new System.Drawing.Font("Arial Narrow", 15.81818F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlerts.Location = new System.Drawing.Point(20, 20);
            this.lblAlerts.Name = "lblAlerts";
            this.lblAlerts.Size = new System.Drawing.Size(173, 66);
            this.lblAlerts.TabIndex = 2;
            this.lblAlerts.Text = "             Alerts\r\n \r\n";
            this.lblAlerts.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btm_panel
            // 
            this.btm_panel.BackColor = System.Drawing.Color.White;
            this.btm_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btm_panel.Controls.Add(this.tabControl);
            this.btm_panel.Controls.Add(this.panel2);
            this.btm_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btm_panel.Location = new System.Drawing.Point(334, 791);
            this.btm_panel.Margin = new System.Windows.Forms.Padding(0);
            this.btm_panel.Name = "btm_panel";
            this.btm_panel.Padding = new System.Windows.Forms.Padding(5);
            this.btm_panel.Size = new System.Drawing.Size(1326, 208);
            this.btm_panel.TabIndex = 4;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(5, 5);
            this.tabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1029, 196);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtConsole);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(1021, 167);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Console";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.SystemColors.Control;
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Location = new System.Drawing.Point(3, 2);
            this.txtConsole.Margin = new System.Windows.Forms.Padding(0);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(1015, 163);
            this.txtConsole.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_PopOut);
            this.tabPage2.Controls.Add(this.btn_PauseAlerts);
            this.tabPage2.Controls.Add(this.btn_RequestSQL);
            this.tabPage2.Controls.Add(this.btnRequestPos);
            this.tabPage2.Controls.Add(this.btnResetMap);
            this.tabPage2.Controls.Add(this.btn_ShowStations);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1021, 167);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Control";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_PopOut
            // 
            this.btn_PopOut.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PopOut.Location = new System.Drawing.Point(3, 92);
            this.btn_PopOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PopOut.Name = "btn_PopOut";
            this.btn_PopOut.Size = new System.Drawing.Size(83, 34);
            this.btn_PopOut.TabIndex = 1;
            this.btn_PopOut.Text = "Pop Out";
            this.btn_PopOut.UseVisualStyleBackColor = true;
            this.btn_PopOut.Click += new System.EventHandler(this.btn_PopOutClick);
            // 
            // btn_PauseAlerts
            // 
            this.btn_PauseAlerts.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PauseAlerts.Location = new System.Drawing.Point(109, 2);
            this.btn_PauseAlerts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PauseAlerts.Name = "btn_PauseAlerts";
            this.btn_PauseAlerts.Size = new System.Drawing.Size(104, 34);
            this.btn_PauseAlerts.TabIndex = 7;
            this.btn_PauseAlerts.Text = "Pause Alerts";
            this.btn_PauseAlerts.UseVisualStyleBackColor = true;
            this.btn_PauseAlerts.Click += new System.EventHandler(this.btn_PauseAlerts_click);
            // 
            // btn_RequestSQL
            // 
            this.btn_RequestSQL.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RequestSQL.Location = new System.Drawing.Point(141, 43);
            this.btn_RequestSQL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_RequestSQL.Name = "btn_RequestSQL";
            this.btn_RequestSQL.Size = new System.Drawing.Size(123, 38);
            this.btn_RequestSQL.TabIndex = 5;
            this.btn_RequestSQL.Text = "Request SQL";
            this.btn_RequestSQL.UseVisualStyleBackColor = true;
            this.btn_RequestSQL.Click += new System.EventHandler(this.btn_RequestSQLClick);
            // 
            // btnRequestPos
            // 
            this.btnRequestPos.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRequestPos.Location = new System.Drawing.Point(3, 43);
            this.btnRequestPos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRequestPos.Name = "btnRequestPos";
            this.btnRequestPos.Size = new System.Drawing.Size(133, 38);
            this.btnRequestPos.TabIndex = 2;
            this.btnRequestPos.Text = "Position request";
            this.btnRequestPos.UseVisualStyleBackColor = true;
            this.btnRequestPos.Click += new System.EventHandler(this.btn_GetPosClick);
            // 
            // btnResetMap
            // 
            this.btnResetMap.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetMap.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnResetMap.Location = new System.Drawing.Point(3, 2);
            this.btnResetMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnResetMap.Name = "btnResetMap";
            this.btnResetMap.Size = new System.Drawing.Size(101, 34);
            this.btnResetMap.TabIndex = 0;
            this.btnResetMap.Text = "Reset Map";
            this.btnResetMap.UseVisualStyleBackColor = true;
            this.btnResetMap.Click += new System.EventHandler(this.btn_ResetMapClick);
            // 
            // btn_ShowStations
            // 
            this.btn_ShowStations.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ShowStations.Location = new System.Drawing.Point(3, 132);
            this.btn_ShowStations.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ShowStations.Name = "btn_ShowStations";
            this.btn_ShowStations.Size = new System.Drawing.Size(117, 33);
            this.btn_ShowStations.TabIndex = 4;
            this.btn_ShowStations.Text = "Show Stations";
            this.btn_ShowStations.UseVisualStyleBackColor = true;
            this.btn_ShowStations.Click += new System.EventHandler(this.btn_ShowStationsClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTimer);
            this.panel2.Controls.Add(this.btn_ChangeTimerInterval);
            this.panel2.Controls.Add(this.nrUpDwn_TimerInterval);
            this.panel2.Controls.Add(this.btn_PauseTrains);
            this.panel2.Controls.Add(this.btn_ShowTrains);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1034, 5);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(285, 196);
            this.panel2.TabIndex = 4;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Location = new System.Drawing.Point(5, 11);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(93, 16);
            this.lblTimer.TabIndex = 3;
            this.lblTimer.Text = "Update Timer:";
            // 
            // btn_ChangeTimerInterval
            // 
            this.btn_ChangeTimerInterval.Location = new System.Drawing.Point(204, 5);
            this.btn_ChangeTimerInterval.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ChangeTimerInterval.Name = "btn_ChangeTimerInterval";
            this.btn_ChangeTimerInterval.Size = new System.Drawing.Size(75, 30);
            this.btn_ChangeTimerInterval.TabIndex = 2;
            this.btn_ChangeTimerInterval.Text = "Change";
            this.btn_ChangeTimerInterval.UseVisualStyleBackColor = true;
            this.btn_ChangeTimerInterval.Click += new System.EventHandler(this.btn_ChangeTimerInterval_Click);
            // 
            // nrUpDwn_TimerInterval
            // 
            this.nrUpDwn_TimerInterval.Location = new System.Drawing.Point(105, 9);
            this.nrUpDwn_TimerInterval.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nrUpDwn_TimerInterval.Name = "nrUpDwn_TimerInterval";
            this.nrUpDwn_TimerInterval.Size = new System.Drawing.Size(87, 22);
            this.nrUpDwn_TimerInterval.TabIndex = 1;
            // 
            // btn_PauseTrains
            // 
            this.btn_PauseTrains.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_PauseTrains.Location = new System.Drawing.Point(171, 158);
            this.btn_PauseTrains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_PauseTrains.Name = "btn_PauseTrains";
            this.btn_PauseTrains.Size = new System.Drawing.Size(108, 33);
            this.btn_PauseTrains.TabIndex = 6;
            this.btn_PauseTrains.Text = "Pause Trains";
            this.btn_PauseTrains.UseVisualStyleBackColor = true;
            this.btn_PauseTrains.Click += new System.EventHandler(this.btn_PauseTrains_Click);
            // 
            // btn_ShowTrains
            // 
            this.btn_ShowTrains.Font = new System.Drawing.Font("Arial", 7.090909F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ShowTrains.Location = new System.Drawing.Point(5, 158);
            this.btn_ShowTrains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ShowTrains.Name = "btn_ShowTrains";
            this.btn_ShowTrains.Size = new System.Drawing.Size(111, 33);
            this.btn_ShowTrains.TabIndex = 3;
            this.btn_ShowTrains.Text = "Show Trains";
            this.btn_ShowTrains.UseVisualStyleBackColor = true;
            this.btn_ShowTrains.Click += new System.EventHandler(this.btn_ShowTrainsClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblZoom);
            this.panel1.Controls.Add(this.gmap);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(334, 63);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(51, 50, 51, 50);
            this.panel1.Size = new System.Drawing.Size(1326, 728);
            this.panel1.TabIndex = 5;
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.BackColor = System.Drawing.Color.Transparent;
            this.lblZoom.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoom.Location = new System.Drawing.Point(51, 50);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(71, 22);
            this.lblZoom.TabIndex = 1;
            this.lblZoom.Text = "Zoom: ";
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1660, 999);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btm_panel);
            this.Controls.Add(this.lft_panel);
            this.Controls.Add(this.tp_panel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ClientForm";
            this.Text = "RailView Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tp_panel.ResumeLayout(false);
            this.tp_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo_pb)).EndInit();
            this.lft_panel.ResumeLayout(false);
            this.lft_panel.PerformLayout();
            this.btm_panel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nrUpDwn_TimerInterval)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button btnResetMap;
        private System.Windows.Forms.Button btn_PopOut;
        private System.Windows.Forms.Label lblAlerts;
        private System.Windows.Forms.Button btnRequestPos;
        private System.Windows.Forms.Button btn_ShowTrains;
        private System.Windows.Forms.ListBox listBoxAlerts;
        private System.Windows.Forms.Button btn_ShowStations;
        private System.Windows.Forms.Button btn_RequestSQL;
        private System.Windows.Forms.Button btn_PauseAlerts;
        private System.Windows.Forms.Button btn_PauseTrains;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btn_ChangeTimerInterval;
        private System.Windows.Forms.NumericUpDown nrUpDwn_TimerInterval;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblZoom;
    }
}

