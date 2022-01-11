
namespace RailView_database_GUI
{
    partial class Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Data));
            this.lblTitle = new System.Windows.Forms.Label();
            this.pibLogo = new System.Windows.Forms.PictureBox();
            this.DgvFull = new System.Windows.Forms.DataGridView();
            this.txbTableName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblBorderForm = new System.Windows.Forms.Label();
            this.lblAddSomething = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pibLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvFull)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(247, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(97, 31);
            this.lblTitle.TabIndex = 44;
            this.lblTitle.Text = "Table: ";
            // 
            // pibLogo
            // 
            this.pibLogo.Image = ((System.Drawing.Image)(resources.GetObject("pibLogo.Image")));
            this.pibLogo.Location = new System.Drawing.Point(11, 11);
            this.pibLogo.Margin = new System.Windows.Forms.Padding(2);
            this.pibLogo.Name = "pibLogo";
            this.pibLogo.Size = new System.Drawing.Size(217, 161);
            this.pibLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pibLogo.TabIndex = 35;
            this.pibLogo.TabStop = false;
            this.pibLogo.Click += new System.EventHandler(this.pibLogo_Click);
            // 
            // DgvFull
            // 
            this.DgvFull.AllowUserToAddRows = false;
            this.DgvFull.AllowUserToDeleteRows = false;
            this.DgvFull.BackgroundColor = System.Drawing.Color.White;
            this.DgvFull.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvFull.Location = new System.Drawing.Point(247, 125);
            this.DgvFull.Name = "DgvFull";
            this.DgvFull.ReadOnly = true;
            this.DgvFull.Size = new System.Drawing.Size(1245, 498);
            this.DgvFull.TabIndex = 34;
            this.DgvFull.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvFull_CellClick);
            // 
            // txbTableName
            // 
            this.txbTableName.Location = new System.Drawing.Point(267, 720);
            this.txbTableName.Name = "txbTableName";
            this.txbTableName.Size = new System.Drawing.Size(240, 20);
            this.txbTableName.TabIndex = 45;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(91)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(267, 764);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(240, 30);
            this.btnAdd.TabIndex = 47;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblBorderForm
            // 
            this.lblBorderForm.AutoSize = true;
            this.lblBorderForm.BackColor = System.Drawing.Color.Transparent;
            this.lblBorderForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBorderForm.Location = new System.Drawing.Point(247, 670);
            this.lblBorderForm.Name = "lblBorderForm";
            this.lblBorderForm.Padding = new System.Windows.Forms.Padding(0, 120, 280, 0);
            this.lblBorderForm.Size = new System.Drawing.Size(282, 135);
            this.lblBorderForm.TabIndex = 48;
            // 
            // lblAddSomething
            // 
            this.lblAddSomething.AutoSize = true;
            this.lblAddSomething.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lblAddSomething.Location = new System.Drawing.Point(264, 682);
            this.lblAddSomething.Name = "lblAddSomething";
            this.lblAddSomething.Size = new System.Drawing.Size(101, 22);
            this.lblAddSomething.TabIndex = 49;
            this.lblAddSomething.Text = "Add a table";
            // 
            // Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1553, 942);
            this.Controls.Add(this.lblAddSomething);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txbTableName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pibLogo);
            this.Controls.Add(this.DgvFull);
            this.Controls.Add(this.lblBorderForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Data";
            this.Text = "Data";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Data_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pibLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgvFull)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pibLogo;
        private System.Windows.Forms.DataGridView DgvFull;
        private System.Windows.Forms.TextBox txbTableName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblBorderForm;
        private System.Windows.Forms.Label lblAddSomething;
    }
}