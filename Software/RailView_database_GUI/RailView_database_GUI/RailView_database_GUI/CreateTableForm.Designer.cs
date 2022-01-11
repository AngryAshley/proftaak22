
namespace RailView_database_GUI
{
    partial class CreateTableForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblDefault = new System.Windows.Forms.Label();
            this.tlpFull = new System.Windows.Forms.TableLayoutPanel();
            this.txbName0 = new System.Windows.Forms.TextBox();
            this.cobType0 = new System.Windows.Forms.ComboBox();
            this.txbLenVal0 = new System.Windows.Forms.TextBox();
            this.cobDefault0 = new System.Windows.Forms.ComboBox();
            this.chbPK = new System.Windows.Forms.CheckBox();
            this.chbAI = new System.Windows.Forms.CheckBox();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.btnAddTable = new System.Windows.Forms.Button();
            this.lblPK = new System.Windows.Forms.Label();
            this.lblAI = new System.Windows.Forms.Label();
            this.tlpFull.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(35, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 31);
            this.lblTitle.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(40, 60);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(157, 60);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 13);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(274, 60);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(77, 13);
            this.lblLength.TabIndex = 3;
            this.lblLength.Text = "Length/Values";
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(391, 60);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(41, 13);
            this.lblDefault.TabIndex = 4;
            this.lblDefault.Text = "Default";
            // 
            // tlpFull
            // 
            this.tlpFull.ColumnCount = 6;
            this.tlpFull.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tlpFull.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tlpFull.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tlpFull.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tlpFull.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpFull.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tlpFull.Controls.Add(this.txbName0, 0, 0);
            this.tlpFull.Controls.Add(this.cobType0, 1, 0);
            this.tlpFull.Controls.Add(this.txbLenVal0, 2, 0);
            this.tlpFull.Controls.Add(this.cobDefault0, 3, 0);
            this.tlpFull.Controls.Add(this.chbPK, 4, 0);
            this.tlpFull.Controls.Add(this.chbAI, 5, 0);
            this.tlpFull.Location = new System.Drawing.Point(40, 76);
            this.tlpFull.Name = "tlpFull";
            this.tlpFull.RowCount = 1;
            this.tlpFull.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFull.Size = new System.Drawing.Size(523, 28);
            this.tlpFull.TabIndex = 5;
            // 
            // txbName0
            // 
            this.txbName0.Location = new System.Drawing.Point(3, 3);
            this.txbName0.Name = "txbName0";
            this.txbName0.Size = new System.Drawing.Size(111, 20);
            this.txbName0.TabIndex = 0;
            // 
            // cobType0
            // 
            this.cobType0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobType0.FormattingEnabled = true;
            this.cobType0.Items.AddRange(new object[] {
            "INT",
            "VARCHAR",
            "BOOLEAN",
            "DOUBLE ",
            "TIMESTAMP"});
            this.cobType0.Location = new System.Drawing.Point(120, 3);
            this.cobType0.Name = "cobType0";
            this.cobType0.Size = new System.Drawing.Size(111, 21);
            this.cobType0.TabIndex = 1;
            // 
            // txbLenVal0
            // 
            this.txbLenVal0.Location = new System.Drawing.Point(237, 3);
            this.txbLenVal0.Name = "txbLenVal0";
            this.txbLenVal0.Size = new System.Drawing.Size(111, 20);
            this.txbLenVal0.TabIndex = 2;
            // 
            // cobDefault0
            // 
            this.cobDefault0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobDefault0.FormattingEnabled = true;
            this.cobDefault0.Items.AddRange(new object[] {
            "NONE",
            "NULL",
            "CURRENT_TIMESTAMP"});
            this.cobDefault0.Location = new System.Drawing.Point(354, 3);
            this.cobDefault0.Name = "cobDefault0";
            this.cobDefault0.Size = new System.Drawing.Size(111, 21);
            this.cobDefault0.TabIndex = 3;
            // 
            // chbPK
            // 
            this.chbPK.AutoSize = true;
            this.chbPK.Location = new System.Drawing.Point(471, 3);
            this.chbPK.Name = "chbPK";
            this.chbPK.Size = new System.Drawing.Size(15, 14);
            this.chbPK.TabIndex = 4;
            this.chbPK.UseVisualStyleBackColor = true;
            // 
            // chbAI
            // 
            this.chbAI.AutoSize = true;
            this.chbAI.Location = new System.Drawing.Point(497, 3);
            this.chbAI.Name = "chbAI";
            this.chbAI.Size = new System.Drawing.Size(15, 14);
            this.chbAI.TabIndex = 5;
            this.chbAI.UseVisualStyleBackColor = true;
            // 
            // btnAddRow
            // 
            this.btnAddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(91)))));
            this.btnAddRow.ForeColor = System.Drawing.Color.White;
            this.btnAddRow.Location = new System.Drawing.Point(626, 76);
            this.btnAddRow.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(125, 20);
            this.btnAddRow.TabIndex = 48;
            this.btnAddRow.Text = "Add row";
            this.btnAddRow.UseVisualStyleBackColor = false;
            this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
            // 
            // btnAddTable
            // 
            this.btnAddTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(115)))), ((int)(((byte)(91)))));
            this.btnAddTable.ForeColor = System.Drawing.Color.White;
            this.btnAddTable.Location = new System.Drawing.Point(626, 110);
            this.btnAddTable.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new System.Drawing.Size(125, 20);
            this.btnAddTable.TabIndex = 49;
            this.btnAddTable.Text = "Add Table";
            this.btnAddTable.UseVisualStyleBackColor = false;
            this.btnAddTable.Click += new System.EventHandler(this.btnAddTable_Click);
            // 
            // lblPK
            // 
            this.lblPK.AutoSize = true;
            this.lblPK.Location = new System.Drawing.Point(508, 60);
            this.lblPK.Name = "lblPK";
            this.lblPK.Size = new System.Drawing.Size(21, 13);
            this.lblPK.TabIndex = 50;
            this.lblPK.Text = "PK";
            // 
            // lblAI
            // 
            this.lblAI.AutoSize = true;
            this.lblAI.Location = new System.Drawing.Point(535, 60);
            this.lblAI.Name = "lblAI";
            this.lblAI.Size = new System.Drawing.Size(17, 13);
            this.lblAI.TabIndex = 51;
            this.lblAI.Text = "AI";
            // 
            // CreateTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 433);
            this.Controls.Add(this.lblAI);
            this.Controls.Add(this.lblPK);
            this.Controls.Add(this.btnAddTable);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.tlpFull);
            this.Controls.Add(this.lblDefault);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CreateTableForm";
            this.Text = "CreateTableForm";
            this.Load += new System.EventHandler(this.CreateTableForm_Load);
            this.tlpFull.ResumeLayout(false);
            this.tlpFull.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.TableLayoutPanel tlpFull;
        private System.Windows.Forms.TextBox txbName0;
        private System.Windows.Forms.ComboBox cobType0;
        private System.Windows.Forms.TextBox txbLenVal0;
        private System.Windows.Forms.ComboBox cobDefault0;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnAddTable;
        private System.Windows.Forms.CheckBox chbPK;
        private System.Windows.Forms.CheckBox chbAI;
        private System.Windows.Forms.Label lblPK;
        private System.Windows.Forms.Label lblAI;
    }
}