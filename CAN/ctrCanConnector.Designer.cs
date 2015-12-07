namespace PCAN
{
    partial class ctrCanConnector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbChannel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBaudrates = new System.Windows.Forms.ComboBox();
            this.laBaudrate = new System.Windows.Forms.Label();
            this.cbHwType = new System.Windows.Forms.ComboBox();
            this.cbInterrupt = new System.Windows.Forms.ComboBox();
            this.laInterrupt = new System.Windows.Forms.Label();
            this.cbIO = new System.Windows.Forms.ComboBox();
            this.laIOPort = new System.Windows.Forms.Label();
            this.laHwType = new System.Windows.Forms.Label();
            this.btnHwRefresh = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(103, 163);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbChannel
            // 
            this.cbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChannel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbChannel.Items.AddRange(new object[] {
            "None",
            "DNG-Channel 1",
            "ISA-Channel 1",
            "ISA-Channel 2",
            "ISA-Channel 3",
            "ISA-Channel 4",
            "ISA-Channel 5",
            "ISA-Channel 6",
            "ISA-Channel 7",
            "ISA-Channel 8",
            "PCC-Channel 1",
            "PCC-Channel 2",
            "PCI-Channel 1",
            "PCI-Channel 2",
            "PCI-Channel 3",
            "PCI-Channel 4",
            "PCI-Channel 5",
            "PCI-Channel 6",
            "PCI-Channel 7",
            "PCI-Channel 8",
            "USB-Channel 1",
            "USB-Channel 2",
            "USB-Channel 3",
            "USB-Channel 4",
            "USB-Channel 5",
            "USB-Channel 6",
            "USB-Channel 7",
            "USB-Channel 8"});
            this.cbChannel.Location = new System.Drawing.Point(103, 16);
            this.cbChannel.Name = "cbChannel";
            this.cbChannel.Size = new System.Drawing.Size(127, 21);
            this.cbChannel.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Hardware";
            // 
            // cbBaudrates
            // 
            this.cbBaudrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudrates.Items.AddRange(new object[] {
            "1 MBit/sec",
            "800 kBit/s",
            "500 kBit/sec",
            "250 kBit/sec",
            "125 kBit/sec",
            "100 kBit/sec",
            "95,238 kBit/s",
            "83,333 kBit/s",
            "50 kBit/sec",
            "47,619 kBit/s",
            "33,333 kBit/s",
            "20 kBit/sec",
            "10 kBit/sec",
            "5 kBit/sec"});
            this.cbBaudrates.Location = new System.Drawing.Point(103, 45);
            this.cbBaudrates.Name = "cbBaudrates";
            this.cbBaudrates.Size = new System.Drawing.Size(127, 21);
            this.cbBaudrates.TabIndex = 54;
            // 
            // laBaudrate
            // 
            this.laBaudrate.Location = new System.Drawing.Point(13, 48);
            this.laBaudrate.Name = "laBaudrate";
            this.laBaudrate.Size = new System.Drawing.Size(56, 23);
            this.laBaudrate.TabIndex = 55;
            this.laBaudrate.Text = "Baudrate:";
            // 
            // cbHwType
            // 
            this.cbHwType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHwType.Enabled = false;
            this.cbHwType.Items.AddRange(new object[] {
            "ISA-82C200",
            "ISA-SJA1000",
            "ISA-PHYTEC",
            "DNG-82C200",
            "DNG-82C200 EPP",
            "DNG-SJA1000",
            "DNG-SJA1000 EPP"});
            this.cbHwType.Location = new System.Drawing.Point(103, 72);
            this.cbHwType.Name = "cbHwType";
            this.cbHwType.Size = new System.Drawing.Size(127, 21);
            this.cbHwType.TabIndex = 57;
            // 
            // cbInterrupt
            // 
            this.cbInterrupt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterrupt.Enabled = false;
            this.cbInterrupt.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "7",
            "9",
            "10",
            "11",
            "12",
            "15"});
            this.cbInterrupt.Location = new System.Drawing.Point(103, 128);
            this.cbInterrupt.Name = "cbInterrupt";
            this.cbInterrupt.Size = new System.Drawing.Size(55, 21);
            this.cbInterrupt.TabIndex = 59;
            // 
            // laInterrupt
            // 
            this.laInterrupt.Location = new System.Drawing.Point(13, 131);
            this.laInterrupt.Name = "laInterrupt";
            this.laInterrupt.Size = new System.Drawing.Size(53, 23);
            this.laInterrupt.TabIndex = 62;
            this.laInterrupt.Text = "Interrupt:";
            // 
            // cbIO
            // 
            this.cbIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIO.Enabled = false;
            this.cbIO.Items.AddRange(new object[] {
            "0100",
            "0120",
            "0140",
            "0200",
            "0220",
            "0240",
            "0260",
            "0278",
            "0280",
            "02A0",
            "02C0",
            "02E0",
            "02E8",
            "02F8",
            "0300",
            "0320",
            "0340",
            "0360",
            "0378",
            "0380",
            "03BC",
            "03E0",
            "03E8",
            "03F8"});
            this.cbIO.Location = new System.Drawing.Point(103, 101);
            this.cbIO.Name = "cbIO";
            this.cbIO.Size = new System.Drawing.Size(55, 21);
            this.cbIO.TabIndex = 58;
            // 
            // laIOPort
            // 
            this.laIOPort.Location = new System.Drawing.Point(13, 104);
            this.laIOPort.Name = "laIOPort";
            this.laIOPort.Size = new System.Drawing.Size(55, 23);
            this.laIOPort.TabIndex = 61;
            this.laIOPort.Text = "I/O Port:";
            // 
            // laHwType
            // 
            this.laHwType.Location = new System.Drawing.Point(13, 75);
            this.laHwType.Name = "laHwType";
            this.laHwType.Size = new System.Drawing.Size(90, 23);
            this.laHwType.TabIndex = 60;
            this.laHwType.Text = "Hardware Type:";
            // 
            // btnHwRefresh
            // 
            this.btnHwRefresh.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnHwRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHwRefresh.Location = new System.Drawing.Point(236, 15);
            this.btnHwRefresh.Name = "btnHwRefresh";
            this.btnHwRefresh.Size = new System.Drawing.Size(57, 23);
            this.btnHwRefresh.TabIndex = 63;
            this.btnHwRefresh.Text = "Refresh";
            this.btnHwRefresh.Click += new System.EventHandler(this.btnHwRefresh_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(16, 163);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 64;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // ctrCanConnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnHwRefresh);
            this.Controls.Add(this.cbHwType);
            this.Controls.Add(this.cbInterrupt);
            this.Controls.Add(this.laInterrupt);
            this.Controls.Add(this.cbIO);
            this.Controls.Add(this.laIOPort);
            this.Controls.Add(this.laHwType);
            this.Controls.Add(this.cbBaudrates);
            this.Controls.Add(this.laBaudrate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbChannel);
            this.Controls.Add(this.btnConnect);
            this.Name = "ctrCanConnector";
            this.Size = new System.Drawing.Size(302, 199);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cbChannel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBaudrates;
        private System.Windows.Forms.Label laBaudrate;
        private System.Windows.Forms.ComboBox cbHwType;
        private System.Windows.Forms.ComboBox cbInterrupt;
        private System.Windows.Forms.Label laInterrupt;
        private System.Windows.Forms.ComboBox cbIO;
        private System.Windows.Forms.Label laIOPort;
        private System.Windows.Forms.Label laHwType;
        private System.Windows.Forms.Button btnHwRefresh;
        private System.Windows.Forms.Button btnDisconnect;
    }
}
