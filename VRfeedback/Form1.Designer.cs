namespace VRfeedback
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comPortsDrop = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lpFound = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lcValue = new System.Windows.Forms.Label();
            this.lValueDecrease = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbProcessName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMemory = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRangeMin = new System.Windows.Forms.TextBox();
            this.tbRangeMax = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.loutOfRange = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lFileLoaded = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comPortsDrop
            // 
            this.comPortsDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortsDrop.FormattingEnabled = true;
            this.comPortsDrop.Location = new System.Drawing.Point(334, 37);
            this.comPortsDrop.Name = "comPortsDrop";
            this.comPortsDrop.Size = new System.Drawing.Size(147, 21);
            this.comPortsDrop.TabIndex = 0;
            this.comPortsDrop.SelectedIndexChanged += new System.EventHandler(this.comPortsDrop_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 16;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(332, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Found:";
            // 
            // lpFound
            // 
            this.lpFound.AutoSize = true;
            this.lpFound.ForeColor = System.Drawing.Color.White;
            this.lpFound.Location = new System.Drawing.Point(419, 205);
            this.lpFound.Name = "lpFound";
            this.lpFound.Size = new System.Drawing.Size(21, 13);
            this.lpFound.TabIndex = 2;
            this.lpFound.Text = "No";
            // 
            // timer2
            // 
            this.timer2.Interval = 50;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(331, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current Value:";
            // 
            // lcValue
            // 
            this.lcValue.AutoSize = true;
            this.lcValue.ForeColor = System.Drawing.Color.White;
            this.lcValue.Location = new System.Drawing.Point(421, 229);
            this.lcValue.Name = "lcValue";
            this.lcValue.Size = new System.Drawing.Size(13, 13);
            this.lcValue.TabIndex = 4;
            this.lcValue.Text = "--";
            // 
            // lValueDecrease
            // 
            this.lValueDecrease.AutoSize = true;
            this.lValueDecrease.ForeColor = System.Drawing.Color.White;
            this.lValueDecrease.Location = new System.Drawing.Point(421, 266);
            this.lValueDecrease.Name = "lValueDecrease";
            this.lValueDecrease.Size = new System.Drawing.Size(13, 13);
            this.lValueDecrease.TabIndex = 6;
            this.lValueDecrease.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(331, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Value Decreased:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(421, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(331, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Last Byte Sent:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(97, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "VRFeedback";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(330, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "COM Port:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(510, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Start Serial";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbProcessName
            // 
            this.tbProcessName.Location = new System.Drawing.Point(335, 84);
            this.tbProcessName.Name = "tbProcessName";
            this.tbProcessName.Size = new System.Drawing.Size(146, 20);
            this.tbProcessName.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(332, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Process Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(335, 157);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Memory Address:";
            // 
            // tbMemory
            // 
            this.tbMemory.Location = new System.Drawing.Point(337, 173);
            this.tbMemory.Name = "tbMemory";
            this.tbMemory.Size = new System.Drawing.Size(144, 20);
            this.tbMemory.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(334, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Valid Range:";
            // 
            // tbRangeMin
            // 
            this.tbRangeMin.Location = new System.Drawing.Point(337, 132);
            this.tbRangeMin.Name = "tbRangeMin";
            this.tbRangeMin.Size = new System.Drawing.Size(49, 20);
            this.tbRangeMin.TabIndex = 16;
            // 
            // tbRangeMax
            // 
            this.tbRangeMax.Location = new System.Drawing.Point(413, 132);
            this.tbRangeMax.Name = "tbRangeMax";
            this.tbRangeMax.Size = new System.Drawing.Size(68, 20);
            this.tbRangeMax.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(395, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "-";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "0 = Int16",
            "1 = Int32",
            "2 = Int64",
            "3 = double float",
            "4 = single float"});
            this.cbType.Location = new System.Drawing.Point(510, 83);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(100, 21);
            this.cbType.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(507, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Type:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(510, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "Load Preset";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(118, 304);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(425, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Avoid using this software for online games. Memory reading may be viewed as tampe" +
    "ring.";
            // 
            // cbPorts
            // 
            this.cbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Items.AddRange(new object[] {
            "1 port",
            "2 ports",
            "3 ports",
            "4 ports",
            "5 ports",
            "6 ports",
            "7 ports",
            "8 ports",
            "9 ports",
            "10 ports",
            "11 ports",
            "12 ports",
            "13 ports",
            "14 ports",
            "15 ports",
            "16 ports"});
            this.cbPorts.Location = new System.Drawing.Point(510, 131);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(100, 21);
            this.cbPorts.TabIndex = 24;
            this.cbPorts.SelectedIndexChanged += new System.EventHandler(this.cbPorts_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(507, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 25;
            this.label14.Text = "# of Ports:";
            // 
            // loutOfRange
            // 
            this.loutOfRange.AutoSize = true;
            this.loutOfRange.ForeColor = System.Drawing.Color.White;
            this.loutOfRange.Location = new System.Drawing.Point(421, 244);
            this.loutOfRange.Name = "loutOfRange";
            this.loutOfRange.Size = new System.Drawing.Size(13, 13);
            this.loutOfRange.TabIndex = 27;
            this.loutOfRange.Text = "--";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(331, 244);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(74, 13);
            this.label16.TabIndex = 26;
            this.label16.Text = "Out of Range:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(507, 240);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "File Loaded:";
            // 
            // lFileLoaded
            // 
            this.lFileLoaded.AutoSize = true;
            this.lFileLoaded.ForeColor = System.Drawing.Color.White;
            this.lFileLoaded.Location = new System.Drawing.Point(507, 255);
            this.lFileLoaded.Name = "lFileLoaded";
            this.lFileLoaded.Size = new System.Drawing.Size(13, 13);
            this.lFileLoaded.TabIndex = 29;
            this.lFileLoaded.Text = "--";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(507, 22);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 13);
            this.label17.TabIndex = 31;
            this.label17.Text = "Address Bit Size:";
            // 
            // cbSize
            // 
            this.cbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSize.FormattingEnabled = true;
            this.cbSize.Items.AddRange(new object[] {
            "0 = 32 Bit",
            "1 = 64 Bit"});
            this.cbSize.Location = new System.Drawing.Point(510, 37);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(100, 21);
            this.cbSize.TabIndex = 30;
            this.cbSize.SelectedIndexChanged += new System.EventHandler(this.cbSize_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(645, 320);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbSize);
            this.Controls.Add(this.lFileLoaded);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.loutOfRange);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbRangeMax);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbRangeMin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbMemory);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbProcessName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lValueDecrease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lcValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lpFound);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comPortsDrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.Text = "VRFeedback";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comPortsDrop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lpFound;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lcValue;
        private System.Windows.Forms.Label lValueDecrease;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbProcessName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbMemory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRangeMin;
        private System.Windows.Forms.TextBox tbRangeMax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbPorts;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label loutOfRange;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lFileLoaded;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbSize;
    }
}

