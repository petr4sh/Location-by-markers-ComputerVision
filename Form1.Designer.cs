namespace LR6_TVis
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.loadIm_Btn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadVid_Btn = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mapBox = new System.Windows.Forms.PictureBox();
            this.btn_Outline = new System.Windows.Forms.Button();
            this.coordsListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fShiftBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rShiftBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rotationBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadIm_Btn,
            this.toolStripSeparator1,
            this.loadVid_Btn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(965, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // loadIm_Btn
            // 
            this.loadIm_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadIm_Btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadIm_Btn.Name = "loadIm_Btn";
            this.loadIm_Btn.Size = new System.Drawing.Size(55, 24);
            this.loadIm_Btn.Text = "Image";
            this.loadIm_Btn.Click += new System.EventHandler(this.loadIm_Btn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // loadVid_Btn
            // 
            this.loadVid_Btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadVid_Btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadVid_Btn.Name = "loadVid_Btn";
            this.loadVid_Btn.Size = new System.Drawing.Size(52, 24);
            this.loadVid_Btn.Text = "Video";
            this.loadVid_Btn.Click += new System.EventHandler(this.loadVid_Btn_Click);
            // 
            // timer
            // 
            this.timer.Interval = 50;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // mapBox
            // 
            this.mapBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mapBox.Location = new System.Drawing.Point(224, 42);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(729, 400);
            this.mapBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mapBox.TabIndex = 23;
            this.mapBox.TabStop = false;
            // 
            // btn_Outline
            // 
            this.btn_Outline.Location = new System.Drawing.Point(15, 305);
            this.btn_Outline.Name = "btn_Outline";
            this.btn_Outline.Size = new System.Drawing.Size(189, 32);
            this.btn_Outline.TabIndex = 21;
            this.btn_Outline.Text = "DETECT";
            this.btn_Outline.UseVisualStyleBackColor = true;
            this.btn_Outline.Click += new System.EventHandler(this.btn_Outline_Click);
            // 
            // coordsListBox
            // 
            this.coordsListBox.FormattingEnabled = true;
            this.coordsListBox.ItemHeight = 16;
            this.coordsListBox.Items.AddRange(new object[] {
            "0 25 100",
            "1 25 25",
            "2 260 25",
            "3 430 25",
            "4 520 75",
            "5 520 275"});
            this.coordsListBox.Location = new System.Drawing.Point(15, 42);
            this.coordsListBox.Name = "coordsListBox";
            this.coordsListBox.Size = new System.Drawing.Size(189, 100);
            this.coordsListBox.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Height";
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(14, 45);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(58, 22);
            this.heightBox.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Forward Shift";
            // 
            // fShiftBox
            // 
            this.fShiftBox.Location = new System.Drawing.Point(96, 45);
            this.fShiftBox.Name = "fShiftBox";
            this.fShiftBox.Size = new System.Drawing.Size(58, 22);
            this.fShiftBox.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Right Shift";
            // 
            // rShiftBox
            // 
            this.rShiftBox.Location = new System.Drawing.Point(96, 87);
            this.rShiftBox.Name = "rShiftBox";
            this.rShiftBox.Size = new System.Drawing.Size(58, 22);
            this.rShiftBox.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "Rotation";
            // 
            // rotationBox
            // 
            this.rotationBox.Location = new System.Drawing.Point(14, 89);
            this.rotationBox.Name = "rotationBox";
            this.rotationBox.Size = new System.Drawing.Size(58, 22);
            this.rotationBox.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rShiftBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.heightBox);
            this.groupBox1.Controls.Add(this.rotationBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.fShiftBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 161);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 129);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera Parameters";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 459);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.btn_Outline);
            this.Controls.Add(this.coordsListBox);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "FedotovaTA_221-328";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton loadIm_Btn;
        private System.Windows.Forms.ToolStripButton loadVid_Btn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox mapBox;
        private System.Windows.Forms.Button btn_Outline;
        private System.Windows.Forms.ListBox coordsListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fShiftBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rShiftBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox rotationBox;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

