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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tbProcessName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbMemory1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbRangeMin1 = new System.Windows.Forms.TextBox();
            this.tbRangeMax1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbType1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lFileLoaded = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbSize = new System.Windows.Forms.ComboBox();
            this.cbOffsetAdd1 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label26 = new System.Windows.Forms.Label();
            this.tbStateList4 = new System.Windows.Forms.TextBox();
            this.tbStateList3 = new System.Windows.Forms.TextBox();
            this.tbStateList2 = new System.Windows.Forms.TextBox();
            this.tbStateList1 = new System.Windows.Forms.TextBox();
            this.cbWhen4 = new System.Windows.Forms.ComboBox();
            this.cbWhen3 = new System.Windows.Forms.ComboBox();
            this.cbWhen2 = new System.Windows.Forms.ComboBox();
            this.cbWhen1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbType4 = new System.Windows.Forms.ComboBox();
            this.cbType3 = new System.Windows.Forms.ComboBox();
            this.cbType2 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.checkActive4 = new System.Windows.Forms.CheckBox();
            this.checkActive3 = new System.Windows.Forms.CheckBox();
            this.checkActive2 = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbOffsetAdd4 = new System.Windows.Forms.ComboBox();
            this.tbMemory4 = new System.Windows.Forms.TextBox();
            this.tbRangeMin4 = new System.Windows.Forms.TextBox();
            this.tbRangeMax4 = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cbOffsetAdd3 = new System.Windows.Forms.ComboBox();
            this.tbMemory3 = new System.Windows.Forms.TextBox();
            this.tbRangeMin3 = new System.Windows.Forms.TextBox();
            this.tbRangeMax3 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.cbOffsetAdd2 = new System.Windows.Forms.ComboBox();
            this.tbMemory2 = new System.Windows.Forms.TextBox();
            this.tbRangeMin2 = new System.Windows.Forms.TextBox();
            this.tbRangeMax2 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkActive1 = new System.Windows.Forms.CheckBox();
            this.lcValue2 = new System.Windows.Forms.Label();
            this.lcValue4 = new System.Windows.Forms.Label();
            this.lcValue3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lLastByte4 = new System.Windows.Forms.Label();
            this.lLastByte3 = new System.Windows.Forms.Label();
            this.lLastByte2 = new System.Windows.Forms.Label();
            this.lLastByte1 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lcValue1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comPortsDrop
            // 
            this.comPortsDrop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortsDrop.FormattingEnabled = true;
            this.comPortsDrop.Location = new System.Drawing.Point(305, 25);
            this.comPortsDrop.Name = "comPortsDrop";
            this.comPortsDrop.Size = new System.Drawing.Size(147, 21);
            this.comPortsDrop.TabIndex = 0;
            this.comPortsDrop.SelectedIndexChanged += new System.EventHandler(this.comPortsDrop_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(754, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Process Found:";
            // 
            // lpFound
            // 
            this.lpFound.AutoSize = true;
            this.lpFound.ForeColor = System.Drawing.Color.White;
            this.lpFound.Location = new System.Drawing.Point(754, 33);
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
            this.label2.Location = new System.Drawing.Point(301, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current Value:";
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
            this.label7.Location = new System.Drawing.Point(301, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "COM Port:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(760, 420);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Start Serial";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbProcessName
            // 
            this.tbProcessName.Location = new System.Drawing.Point(579, 26);
            this.tbProcessName.Name = "tbProcessName";
            this.tbProcessName.Size = new System.Drawing.Size(146, 20);
            this.tbProcessName.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(576, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Process Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(66, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Mem/Offset Value:";
            // 
            // tbMemory1
            // 
            this.tbMemory1.Location = new System.Drawing.Point(68, 52);
            this.tbMemory1.Name = "tbMemory1";
            this.tbMemory1.Size = new System.Drawing.Size(123, 20);
            this.tbMemory1.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(456, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 17);
            this.label10.TabIndex = 17;
            this.label10.Text = "Valid Range:";
            // 
            // tbRangeMin1
            // 
            this.tbRangeMin1.Location = new System.Drawing.Point(436, 51);
            this.tbRangeMin1.Name = "tbRangeMin1";
            this.tbRangeMin1.Size = new System.Drawing.Size(49, 20);
            this.tbRangeMin1.TabIndex = 16;
            // 
            // tbRangeMax1
            // 
            this.tbRangeMax1.Location = new System.Drawing.Point(497, 51);
            this.tbRangeMax1.Name = "tbRangeMax1";
            this.tbRangeMax1.Size = new System.Drawing.Size(58, 20);
            this.tbRangeMax1.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(487, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "-";
            // 
            // cbType1
            // 
            this.cbType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType1.FormattingEnabled = true;
            this.cbType1.Items.AddRange(new object[] {
            "0 = Int16",
            "1 = Int32",
            "2 = Int64",
            "3 = double float",
            "4 = single float"});
            this.cbType1.Location = new System.Drawing.Point(327, 51);
            this.cbType1.Name = "cbType1";
            this.cbType1.Size = new System.Drawing.Size(95, 21);
            this.cbType1.TabIndex = 20;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(12, 420);
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
            this.label13.Location = new System.Drawing.Point(243, 450);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(425, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Avoid using this software for online games. Memory reading may be viewed as tampe" +
    "ring.";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(128, 425);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "File Loaded:";
            // 
            // lFileLoaded
            // 
            this.lFileLoaded.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lFileLoaded.AutoSize = true;
            this.lFileLoaded.ForeColor = System.Drawing.Color.White;
            this.lFileLoaded.Location = new System.Drawing.Point(199, 425);
            this.lFileLoaded.Name = "lFileLoaded";
            this.lFileLoaded.Size = new System.Drawing.Size(13, 13);
            this.lFileLoaded.TabIndex = 29;
            this.lFileLoaded.Text = "--";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(462, 9);
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
            this.cbSize.Location = new System.Drawing.Point(465, 24);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(100, 21);
            this.cbSize.TabIndex = 30;
            this.cbSize.SelectedIndexChanged += new System.EventHandler(this.cbSize_SelectedIndexChanged);
            // 
            // cbOffsetAdd1
            // 
            this.cbOffsetAdd1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffsetAdd1.FormattingEnabled = true;
            this.cbOffsetAdd1.Items.AddRange(new object[] {
            "0 = Offset",
            "1 = Address"});
            this.cbOffsetAdd1.Location = new System.Drawing.Point(207, 51);
            this.cbOffsetAdd1.Name = "cbOffsetAdd1";
            this.cbOffsetAdd1.Size = new System.Drawing.Size(100, 21);
            this.cbOffsetAdd1.TabIndex = 32;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(208, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(117, 17);
            this.label18.TabIndex = 33;
            this.label18.Text = "Offset/Mem Add?";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.tbStateList4);
            this.panel1.Controls.Add(this.tbStateList3);
            this.panel1.Controls.Add(this.tbStateList2);
            this.panel1.Controls.Add(this.tbStateList1);
            this.panel1.Controls.Add(this.cbWhen4);
            this.panel1.Controls.Add(this.cbWhen3);
            this.panel1.Controls.Add(this.cbWhen2);
            this.panel1.Controls.Add(this.cbWhen1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.cbType4);
            this.panel1.Controls.Add(this.cbType3);
            this.panel1.Controls.Add(this.cbType2);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.checkActive4);
            this.panel1.Controls.Add(this.checkActive3);
            this.panel1.Controls.Add(this.checkActive2);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.cbOffsetAdd4);
            this.panel1.Controls.Add(this.tbMemory4);
            this.panel1.Controls.Add(this.tbRangeMin4);
            this.panel1.Controls.Add(this.tbRangeMax4);
            this.panel1.Controls.Add(this.label29);
            this.panel1.Controls.Add(this.cbOffsetAdd3);
            this.panel1.Controls.Add(this.cbType1);
            this.panel1.Controls.Add(this.tbMemory3);
            this.panel1.Controls.Add(this.tbRangeMin3);
            this.panel1.Controls.Add(this.tbRangeMax3);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.cbOffsetAdd2);
            this.panel1.Controls.Add(this.tbMemory2);
            this.panel1.Controls.Add(this.tbRangeMin2);
            this.panel1.Controls.Add(this.tbRangeMax2);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.checkActive1);
            this.panel1.Controls.Add(this.cbOffsetAdd1);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.tbMemory1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.tbRangeMin1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tbRangeMax1);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Location = new System.Drawing.Point(12, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 188);
            this.panel1.TabIndex = 34;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(682, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(157, 17);
            this.label26.TabIndex = 80;
            this.label26.Text = "State List (comma sep.)";
            // 
            // tbStateList4
            // 
            this.tbStateList4.Location = new System.Drawing.Point(686, 153);
            this.tbStateList4.Name = "tbStateList4";
            this.tbStateList4.Size = new System.Drawing.Size(132, 20);
            this.tbStateList4.TabIndex = 79;
            // 
            // tbStateList3
            // 
            this.tbStateList3.Location = new System.Drawing.Point(686, 120);
            this.tbStateList3.Name = "tbStateList3";
            this.tbStateList3.Size = new System.Drawing.Size(132, 20);
            this.tbStateList3.TabIndex = 78;
            // 
            // tbStateList2
            // 
            this.tbStateList2.Location = new System.Drawing.Point(686, 87);
            this.tbStateList2.Name = "tbStateList2";
            this.tbStateList2.Size = new System.Drawing.Size(132, 20);
            this.tbStateList2.TabIndex = 77;
            // 
            // tbStateList1
            // 
            this.tbStateList1.Location = new System.Drawing.Point(686, 54);
            this.tbStateList1.Name = "tbStateList1";
            this.tbStateList1.Size = new System.Drawing.Size(132, 20);
            this.tbStateList1.TabIndex = 76;
            // 
            // cbWhen4
            // 
            this.cbWhen4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhen4.FormattingEnabled = true;
            this.cbWhen4.Items.AddRange(new object[] {
            "0 = On Decrease",
            "1 = On Increase",
            "2 = On Change",
            "3 = Continuous",
            "4 = On State"});
            this.cbWhen4.Location = new System.Drawing.Point(568, 153);
            this.cbWhen4.Name = "cbWhen4";
            this.cbWhen4.Size = new System.Drawing.Size(100, 21);
            this.cbWhen4.TabIndex = 75;
            // 
            // cbWhen3
            // 
            this.cbWhen3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhen3.FormattingEnabled = true;
            this.cbWhen3.Items.AddRange(new object[] {
            "0 = On Decrease",
            "1 = On Increase",
            "2 = On Change",
            "3 = Continuous",
            "4 = On State"});
            this.cbWhen3.Location = new System.Drawing.Point(568, 119);
            this.cbWhen3.Name = "cbWhen3";
            this.cbWhen3.Size = new System.Drawing.Size(100, 21);
            this.cbWhen3.TabIndex = 74;
            // 
            // cbWhen2
            // 
            this.cbWhen2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhen2.FormattingEnabled = true;
            this.cbWhen2.Items.AddRange(new object[] {
            "0 = On Decrease",
            "1 = On Increase",
            "2 = On Change",
            "3 = Continuous",
            "4 = On State"});
            this.cbWhen2.Location = new System.Drawing.Point(568, 85);
            this.cbWhen2.Name = "cbWhen2";
            this.cbWhen2.Size = new System.Drawing.Size(100, 21);
            this.cbWhen2.TabIndex = 73;
            // 
            // cbWhen1
            // 
            this.cbWhen1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWhen1.FormattingEnabled = true;
            this.cbWhen1.Items.AddRange(new object[] {
            "0 = On Decrease",
            "1 = On Increase",
            "2 = On Change",
            "3 = Continuous",
            "4 = On State"});
            this.cbWhen1.Location = new System.Drawing.Point(568, 53);
            this.cbWhen1.Name = "cbWhen1";
            this.cbWhen1.Size = new System.Drawing.Size(100, 21);
            this.cbWhen1.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(564, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 71;
            this.label3.Text = "Send When?";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(10, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(829, 10);
            this.panel2.TabIndex = 70;
            // 
            // cbType4
            // 
            this.cbType4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType4.FormattingEnabled = true;
            this.cbType4.Items.AddRange(new object[] {
            "0 = Int16",
            "1 = Int32",
            "2 = Int64",
            "3 = double float",
            "4 = single float"});
            this.cbType4.Location = new System.Drawing.Point(327, 153);
            this.cbType4.Name = "cbType4";
            this.cbType4.Size = new System.Drawing.Size(95, 21);
            this.cbType4.TabIndex = 69;
            // 
            // cbType3
            // 
            this.cbType3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType3.FormattingEnabled = true;
            this.cbType3.Items.AddRange(new object[] {
            "0 = Int16",
            "1 = Int32",
            "2 = Int64",
            "3 = double float",
            "4 = single float"});
            this.cbType3.Location = new System.Drawing.Point(327, 121);
            this.cbType3.Name = "cbType3";
            this.cbType3.Size = new System.Drawing.Size(95, 21);
            this.cbType3.TabIndex = 68;
            // 
            // cbType2
            // 
            this.cbType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType2.FormattingEnabled = true;
            this.cbType2.Items.AddRange(new object[] {
            "0 = Int16",
            "1 = Int32",
            "2 = Int64",
            "3 = double float",
            "4 = single float"});
            this.cbType2.Location = new System.Drawing.Point(327, 86);
            this.cbType2.Name = "cbType2";
            this.cbType2.Size = new System.Drawing.Size(95, 21);
            this.cbType2.TabIndex = 67;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(356, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(40, 17);
            this.label19.TabIndex = 66;
            this.label19.Text = "Type";
            // 
            // checkActive4
            // 
            this.checkActive4.AutoSize = true;
            this.checkActive4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkActive4.ForeColor = System.Drawing.Color.White;
            this.checkActive4.Location = new System.Drawing.Point(10, 151);
            this.checkActive4.Name = "checkActive4";
            this.checkActive4.Size = new System.Drawing.Size(54, 24);
            this.checkActive4.TabIndex = 65;
            this.checkActive4.Text = "4. - ";
            this.checkActive4.UseVisualStyleBackColor = true;
            // 
            // checkActive3
            // 
            this.checkActive3.AutoSize = true;
            this.checkActive3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkActive3.ForeColor = System.Drawing.Color.White;
            this.checkActive3.Location = new System.Drawing.Point(10, 117);
            this.checkActive3.Name = "checkActive3";
            this.checkActive3.Size = new System.Drawing.Size(54, 24);
            this.checkActive3.TabIndex = 64;
            this.checkActive3.Text = "3. - ";
            this.checkActive3.UseVisualStyleBackColor = true;
            // 
            // checkActive2
            // 
            this.checkActive2.AutoSize = true;
            this.checkActive2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkActive2.ForeColor = System.Drawing.Color.White;
            this.checkActive2.Location = new System.Drawing.Point(10, 83);
            this.checkActive2.Name = "checkActive2";
            this.checkActive2.Size = new System.Drawing.Size(54, 24);
            this.checkActive2.TabIndex = 63;
            this.checkActive2.Text = "2. - ";
            this.checkActive2.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(7, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 17);
            this.label14.TabIndex = 62;
            this.label14.Text = "Active";
            // 
            // cbOffsetAdd4
            // 
            this.cbOffsetAdd4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffsetAdd4.FormattingEnabled = true;
            this.cbOffsetAdd4.Items.AddRange(new object[] {
            "0 = Offset",
            "1 = Address"});
            this.cbOffsetAdd4.Location = new System.Drawing.Point(208, 154);
            this.cbOffsetAdd4.Name = "cbOffsetAdd4";
            this.cbOffsetAdd4.Size = new System.Drawing.Size(100, 21);
            this.cbOffsetAdd4.TabIndex = 59;
            // 
            // tbMemory4
            // 
            this.tbMemory4.Location = new System.Drawing.Point(69, 155);
            this.tbMemory4.Name = "tbMemory4";
            this.tbMemory4.Size = new System.Drawing.Size(122, 20);
            this.tbMemory4.TabIndex = 53;
            // 
            // tbRangeMin4
            // 
            this.tbRangeMin4.Location = new System.Drawing.Point(437, 154);
            this.tbRangeMin4.Name = "tbRangeMin4";
            this.tbRangeMin4.Size = new System.Drawing.Size(49, 20);
            this.tbRangeMin4.TabIndex = 55;
            // 
            // tbRangeMax4
            // 
            this.tbRangeMax4.Location = new System.Drawing.Point(498, 154);
            this.tbRangeMax4.Name = "tbRangeMax4";
            this.tbRangeMax4.Size = new System.Drawing.Size(57, 20);
            this.tbRangeMax4.TabIndex = 57;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(488, 156);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(10, 13);
            this.label29.TabIndex = 58;
            this.label29.Text = "-";
            // 
            // cbOffsetAdd3
            // 
            this.cbOffsetAdd3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffsetAdd3.FormattingEnabled = true;
            this.cbOffsetAdd3.Items.AddRange(new object[] {
            "0 = Offset",
            "1 = Address"});
            this.cbOffsetAdd3.Location = new System.Drawing.Point(208, 120);
            this.cbOffsetAdd3.Name = "cbOffsetAdd3";
            this.cbOffsetAdd3.Size = new System.Drawing.Size(100, 21);
            this.cbOffsetAdd3.TabIndex = 50;
            // 
            // tbMemory3
            // 
            this.tbMemory3.Location = new System.Drawing.Point(69, 121);
            this.tbMemory3.Name = "tbMemory3";
            this.tbMemory3.Size = new System.Drawing.Size(122, 20);
            this.tbMemory3.TabIndex = 44;
            // 
            // tbRangeMin3
            // 
            this.tbRangeMin3.Location = new System.Drawing.Point(437, 120);
            this.tbRangeMin3.Name = "tbRangeMin3";
            this.tbRangeMin3.Size = new System.Drawing.Size(49, 20);
            this.tbRangeMin3.TabIndex = 46;
            // 
            // tbRangeMax3
            // 
            this.tbRangeMax3.Location = new System.Drawing.Point(498, 120);
            this.tbRangeMax3.Name = "tbRangeMax3";
            this.tbRangeMax3.Size = new System.Drawing.Size(57, 20);
            this.tbRangeMax3.TabIndex = 48;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(488, 122);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(10, 13);
            this.label25.TabIndex = 49;
            this.label25.Text = "-";
            // 
            // cbOffsetAdd2
            // 
            this.cbOffsetAdd2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffsetAdd2.FormattingEnabled = true;
            this.cbOffsetAdd2.Items.AddRange(new object[] {
            "0 = Offset",
            "1 = Address"});
            this.cbOffsetAdd2.Location = new System.Drawing.Point(207, 85);
            this.cbOffsetAdd2.Name = "cbOffsetAdd2";
            this.cbOffsetAdd2.Size = new System.Drawing.Size(100, 21);
            this.cbOffsetAdd2.TabIndex = 41;
            // 
            // tbMemory2
            // 
            this.tbMemory2.Location = new System.Drawing.Point(68, 86);
            this.tbMemory2.Name = "tbMemory2";
            this.tbMemory2.Size = new System.Drawing.Size(123, 20);
            this.tbMemory2.TabIndex = 35;
            // 
            // tbRangeMin2
            // 
            this.tbRangeMin2.Location = new System.Drawing.Point(436, 85);
            this.tbRangeMin2.Name = "tbRangeMin2";
            this.tbRangeMin2.Size = new System.Drawing.Size(49, 20);
            this.tbRangeMin2.TabIndex = 37;
            // 
            // tbRangeMax2
            // 
            this.tbRangeMax2.Location = new System.Drawing.Point(497, 85);
            this.tbRangeMax2.Name = "tbRangeMax2";
            this.tbRangeMax2.Size = new System.Drawing.Size(58, 20);
            this.tbRangeMax2.TabIndex = 39;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(487, 87);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(10, 13);
            this.label21.TabIndex = 40;
            this.label21.Text = "-";
            // 
            // checkActive1
            // 
            this.checkActive1.AutoSize = true;
            this.checkActive1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkActive1.ForeColor = System.Drawing.Color.White;
            this.checkActive1.Location = new System.Drawing.Point(10, 49);
            this.checkActive1.Name = "checkActive1";
            this.checkActive1.Size = new System.Drawing.Size(54, 24);
            this.checkActive1.TabIndex = 34;
            this.checkActive1.Text = "1. - ";
            this.checkActive1.UseVisualStyleBackColor = true;
            // 
            // lcValue2
            // 
            this.lcValue2.AutoSize = true;
            this.lcValue2.ForeColor = System.Drawing.Color.White;
            this.lcValue2.Location = new System.Drawing.Point(322, 107);
            this.lcValue2.Name = "lcValue2";
            this.lcValue2.Size = new System.Drawing.Size(13, 13);
            this.lcValue2.TabIndex = 35;
            this.lcValue2.Text = "--";
            // 
            // lcValue4
            // 
            this.lcValue4.AutoSize = true;
            this.lcValue4.ForeColor = System.Drawing.Color.White;
            this.lcValue4.Location = new System.Drawing.Point(504, 107);
            this.lcValue4.Name = "lcValue4";
            this.lcValue4.Size = new System.Drawing.Size(13, 13);
            this.lcValue4.TabIndex = 37;
            this.lcValue4.Text = "--";
            // 
            // lcValue3
            // 
            this.lcValue3.AutoSize = true;
            this.lcValue3.ForeColor = System.Drawing.Color.White;
            this.lcValue3.Location = new System.Drawing.Point(504, 85);
            this.lcValue3.Name = "lcValue3";
            this.lcValue3.Size = new System.Drawing.Size(13, 13);
            this.lcValue3.TabIndex = 36;
            this.lcValue3.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(306, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "1.)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(306, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "2.)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(482, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 40;
            this.label12.Text = "3.)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(482, 107);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 13);
            this.label16.TabIndex = 41;
            this.label16.Text = "4.)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(485, 195);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 13);
            this.label20.TabIndex = 50;
            this.label20.Text = "4.)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(485, 173);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(19, 13);
            this.label22.TabIndex = 49;
            this.label22.Text = "3.)";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(309, 195);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(19, 13);
            this.label23.TabIndex = 48;
            this.label23.Text = "2.)";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(309, 173);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(19, 13);
            this.label24.TabIndex = 47;
            this.label24.Text = "1.)";
            // 
            // lLastByte4
            // 
            this.lLastByte4.AutoSize = true;
            this.lLastByte4.ForeColor = System.Drawing.Color.White;
            this.lLastByte4.Location = new System.Drawing.Point(507, 195);
            this.lLastByte4.Name = "lLastByte4";
            this.lLastByte4.Size = new System.Drawing.Size(13, 13);
            this.lLastByte4.TabIndex = 46;
            this.lLastByte4.Text = "--";
            // 
            // lLastByte3
            // 
            this.lLastByte3.AutoSize = true;
            this.lLastByte3.ForeColor = System.Drawing.Color.White;
            this.lLastByte3.Location = new System.Drawing.Point(507, 173);
            this.lLastByte3.Name = "lLastByte3";
            this.lLastByte3.Size = new System.Drawing.Size(13, 13);
            this.lLastByte3.TabIndex = 45;
            this.lLastByte3.Text = "--";
            // 
            // lLastByte2
            // 
            this.lLastByte2.AutoSize = true;
            this.lLastByte2.ForeColor = System.Drawing.Color.White;
            this.lLastByte2.Location = new System.Drawing.Point(325, 195);
            this.lLastByte2.Name = "lLastByte2";
            this.lLastByte2.Size = new System.Drawing.Size(13, 13);
            this.lLastByte2.TabIndex = 44;
            this.lLastByte2.Text = "--";
            // 
            // lLastByte1
            // 
            this.lLastByte1.AutoSize = true;
            this.lLastByte1.ForeColor = System.Drawing.Color.White;
            this.lLastByte1.Location = new System.Drawing.Point(325, 173);
            this.lLastByte1.Name = "lLastByte1";
            this.lLastByte1.Size = new System.Drawing.Size(13, 13);
            this.lLastByte1.TabIndex = 43;
            this.lLastByte1.Text = "--";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(302, 154);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(158, 13);
            this.label31.TabIndex = 42;
            this.label31.Text = "Last Byte Sent (port, value, bin):";
            // 
            // lcValue1
            // 
            this.lcValue1.AutoSize = true;
            this.lcValue1.ForeColor = System.Drawing.Color.White;
            this.lcValue1.Location = new System.Drawing.Point(322, 85);
            this.lcValue1.Name = "lcValue1";
            this.lcValue1.Size = new System.Drawing.Size(13, 13);
            this.lcValue1.TabIndex = 4;
            this.lcValue1.Text = "--";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(872, 464);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lLastByte4);
            this.Controls.Add(this.lLastByte3);
            this.Controls.Add(this.lLastByte2);
            this.Controls.Add(this.lLastByte1);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lcValue4);
            this.Controls.Add(this.lcValue3);
            this.Controls.Add(this.lcValue2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbSize);
            this.Controls.Add(this.lFileLoaded);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbProcessName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lcValue1);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbProcessName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbMemory1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbRangeMin1;
        private System.Windows.Forms.TextBox tbRangeMax1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbType1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lFileLoaded;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbSize;
        private System.Windows.Forms.ComboBox cbOffsetAdd1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbOffsetAdd4;
        private System.Windows.Forms.TextBox tbMemory4;
        private System.Windows.Forms.TextBox tbRangeMin4;
        private System.Windows.Forms.TextBox tbRangeMax4;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cbOffsetAdd3;
        private System.Windows.Forms.TextBox tbMemory3;
        private System.Windows.Forms.TextBox tbRangeMin3;
        private System.Windows.Forms.TextBox tbRangeMax3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cbOffsetAdd2;
        private System.Windows.Forms.TextBox tbMemory2;
        private System.Windows.Forms.TextBox tbRangeMin2;
        private System.Windows.Forms.TextBox tbRangeMax2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox checkActive1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbType4;
        private System.Windows.Forms.ComboBox cbType3;
        private System.Windows.Forms.ComboBox cbType2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox checkActive4;
        private System.Windows.Forms.CheckBox checkActive3;
        private System.Windows.Forms.CheckBox checkActive2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lcValue2;
        private System.Windows.Forms.Label lcValue4;
        private System.Windows.Forms.Label lcValue3;
        private System.Windows.Forms.ComboBox cbWhen4;
        private System.Windows.Forms.ComboBox cbWhen3;
        private System.Windows.Forms.ComboBox cbWhen2;
        private System.Windows.Forms.ComboBox cbWhen1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lLastByte4;
        private System.Windows.Forms.Label lLastByte3;
        private System.Windows.Forms.Label lLastByte2;
        private System.Windows.Forms.Label lLastByte1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lcValue1;
        private System.Windows.Forms.TextBox tbStateList4;
        private System.Windows.Forms.TextBox tbStateList3;
        private System.Windows.Forms.TextBox tbStateList2;
        private System.Windows.Forms.TextBox tbStateList1;
        private System.Windows.Forms.Label label26;
    }
}

