namespace _22133044_TranThiKimPhuong
{
    partial class frmPaint
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
            boxFill = new ComboBox();
            boxDash = new ComboBox();
            boxShape = new ComboBox();
            btnUngroup = new Button();
            btnClear = new Button();
            btnDelete = new Button();
            btnGroup = new Button();
            barWidth = new TrackBar();
            lblWidth = new Label();
            lblColor = new Label();
            btnLineColor = new Button();
            btnSelect = new Button();
            btnFillColor = new Button();
            lblFColor = new Label();
            panel1 = new MyPanel();
            btnExit = new Button();
            ((System.ComponentModel.ISupportInitialize)barWidth).BeginInit();
            SuspendLayout();
            // 
            // boxFill
            // 
            boxFill.DropDownStyle = ComboBoxStyle.DropDownList;
            boxFill.Items.AddRange(new object[] { "Fill Shape", "No Fill Shape" });
            boxFill.Location = new Point(13, 15);
            boxFill.Margin = new Padding(3, 4, 3, 4);
            boxFill.Name = "boxFill";
            boxFill.Size = new Size(205, 28);
            boxFill.Sorted = true;
            boxFill.TabIndex = 3;
            boxFill.SelectedIndexChanged += boxFill_SelectedIndexChanged;
            // 
            // boxDash
            // 
            boxDash.DropDownStyle = ComboBoxStyle.DropDownList;
            boxDash.Items.AddRange(new object[] { "Dash", "Dash Dot", "Dash Dot Dot", "Dot", "Solid" });
            boxDash.Location = new Point(13, 90);
            boxDash.Margin = new Padding(3, 4, 3, 4);
            boxDash.Name = "boxDash";
            boxDash.Size = new Size(205, 28);
            boxDash.Sorted = true;
            boxDash.TabIndex = 4;
            boxDash.SelectedIndexChanged += boxDash_SelectedIndexChanged;
            // 
            // boxShape
            // 
            boxShape.DropDownStyle = ComboBoxStyle.DropDownList;
            boxShape.Items.AddRange(new object[] { "Circle", "Curve", "Elipse", "Line", "Polygon", "Rectangle", "Square" });
            boxShape.Location = new Point(13, 52);
            boxShape.Margin = new Padding(3, 4, 3, 4);
            boxShape.Name = "boxShape";
            boxShape.Size = new Size(205, 28);
            boxShape.Sorted = true;
            boxShape.TabIndex = 5;
            boxShape.SelectedIndexChanged += boxShape_SelectedIndexChanged;
            // 
            // btnUngroup
            // 
            btnUngroup.Enabled = false;
            btnUngroup.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnUngroup.ForeColor = Color.Black;
            btnUngroup.Location = new Point(124, 269);
            btnUngroup.Margin = new Padding(3, 4, 3, 4);
            btnUngroup.Name = "btnUngroup";
            btnUngroup.Size = new Size(94, 36);
            btnUngroup.TabIndex = 12;
            btnUngroup.Text = "Ungroup";
            btnUngroup.UseVisualStyleBackColor = true;
            btnUngroup.Click += btnUngroup_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnClear.ForeColor = Color.Red;
            btnClear.Location = new Point(124, 313);
            btnClear.Margin = new Padding(3, 4, 3, 4);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(94, 37);
            btnClear.TabIndex = 13;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnDelete.ForeColor = Color.Black;
            btnDelete.Location = new Point(13, 313);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 37);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnGroup
            // 
            btnGroup.Enabled = false;
            btnGroup.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnGroup.ForeColor = Color.Black;
            btnGroup.Location = new Point(13, 269);
            btnGroup.Margin = new Padding(3, 4, 3, 4);
            btnGroup.Name = "btnGroup";
            btnGroup.Size = new Size(94, 36);
            btnGroup.TabIndex = 15;
            btnGroup.Text = "Group";
            btnGroup.UseVisualStyleBackColor = true;
            btnGroup.Click += btnGroup_Click;
            // 
            // barWidth
            // 
            barWidth.Location = new Point(92, 217);
            barWidth.Margin = new Padding(3, 4, 3, 4);
            barWidth.Minimum = 1;
            barWidth.Name = "barWidth";
            barWidth.Size = new Size(142, 56);
            barWidth.TabIndex = 11;
            barWidth.Value = 1;
            // 
            // lblWidth
            // 
            lblWidth.AutoSize = true;
            lblWidth.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblWidth.ForeColor = Color.Black;
            lblWidth.Location = new Point(13, 217);
            lblWidth.Name = "lblWidth";
            lblWidth.Size = new Size(60, 23);
            lblWidth.TabIndex = 9;
            lblWidth.Text = "Width";
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblColor.ForeColor = Color.Black;
            lblColor.Location = new Point(13, 142);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(91, 23);
            lblColor.TabIndex = 10;
            lblColor.Text = "Line Color";
            // 
            // btnLineColor
            // 
            btnLineColor.BackColor = Color.Black;
            btnLineColor.ForeColor = Color.White;
            btnLineColor.Location = new Point(124, 142);
            btnLineColor.Margin = new Padding(3, 4, 3, 4);
            btnLineColor.Name = "btnLineColor";
            btnLineColor.Size = new Size(94, 28);
            btnLineColor.TabIndex = 8;
            btnLineColor.UseVisualStyleBackColor = false;
            btnLineColor.Click += btnLColor_Click;
            // 
            // btnSelect
            // 
            btnSelect.BackColor = SystemColors.ControlLight;
            btnSelect.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnSelect.ForeColor = Color.Black;
            btnSelect.Location = new Point(12, 358);
            btnSelect.Margin = new Padding(3, 4, 3, 4);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(205, 59);
            btnSelect.TabIndex = 15;
            btnSelect.Text = "Select";
            btnSelect.UseVisualStyleBackColor = false;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnFillColor
            // 
            btnFillColor.BackColor = Color.Black;
            btnFillColor.Enabled = false;
            btnFillColor.ForeColor = Color.White;
            btnFillColor.Location = new Point(124, 179);
            btnFillColor.Margin = new Padding(3, 4, 3, 4);
            btnFillColor.Name = "btnFillColor";
            btnFillColor.Size = new Size(94, 28);
            btnFillColor.TabIndex = 8;
            btnFillColor.UseVisualStyleBackColor = false;
            btnFillColor.Click += btnFColor_Click;
            // 
            // lblFColor
            // 
            lblFColor.AutoSize = true;
            lblFColor.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblFColor.ForeColor = Color.Black;
            lblFColor.Location = new Point(13, 179);
            lblFColor.Name = "lblFColor";
            lblFColor.Size = new Size(82, 23);
            lblFColor.TabIndex = 10;
            lblFColor.Text = "Fill Color";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(235, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(891, 592);
            panel1.TabIndex = 16;
            panel1.Paint += pnlMain_Paint;
            panel1.MouseDown += pnlMain_MouseDown;
            panel1.MouseMove += pnlMain_MouseMove;
            panel1.MouseUp += pnlMain_MouseUp;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnExit.ForeColor = Color.Red;
            btnExit.Location = new Point(13, 566);
            btnExit.Margin = new Padding(3, 4, 3, 4);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(60, 37);
            btnExit.TabIndex = 18;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // Paint
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveBorder;
            ClientSize = new Size(1138, 616);
            Controls.Add(btnExit);
            Controls.Add(panel1);
            Controls.Add(btnSelect);
            Controls.Add(btnUngroup);
            Controls.Add(btnClear);
            Controls.Add(btnDelete);
            Controls.Add(btnGroup);
            Controls.Add(barWidth);
            Controls.Add(lblWidth);
            Controls.Add(lblFColor);
            Controls.Add(lblColor);
            Controls.Add(btnFillColor);
            Controls.Add(btnLineColor);
            Controls.Add(boxFill);
            Controls.Add(boxDash);
            Controls.Add(boxShape);
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Paint";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Paint";
            Load += Paint_Load;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ((System.ComponentModel.ISupportInitialize)barWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox boxFill;
        private System.Windows.Forms.ComboBox boxDash;
        private System.Windows.Forms.ComboBox boxShape;
        private System.Windows.Forms.Button btnUngroup;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnGroup;
        private System.Windows.Forms.TrackBar barWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnLineColor;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnFillColor;
        private System.Windows.Forms.Label lblFColor;
        private MyPanel panel1;
        private Button btnExit;
    }
}

