namespace GripperControler
{
    partial class frmGripper
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
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tabMotorSettings = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabCAN = new System.Windows.Forms.TabPage();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabNewMotorInitialization = new System.Windows.Forms.TabPage();
            this.tmrSendCANMessage = new System.Windows.Forms.Timer(this.components);
            this.ctrCanConnector1 = new PCAN.UI.ctrCanConnector();
            this.ctrDynamixelScanner1 = new Gripper.UI.ctrDynamixelScanner();
            this.ctrPresentValue1 = new Gripper.UI.ctrDynamixelPresentValue();
            this.ctrProperties1 = new Gripper.UI.ctrDynamixelProperties();
            this.ctrMotorPosition1 = new Gripper.UI.ctrDynamixelPosition();
            this.ctrMotorInitialization1 = new Gripper.UI.ctrDynamixelInitialization();
            this.ctrGripper1 = new Gripper.UI.ctrGripper();
            this.tabMotorSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabCAN.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabNewMotorInitialization.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusBar.Location = new System.Drawing.Point(0, 504);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(772, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.TabStop = true;
            this.statusBar.Text = "statusStrip1";
            // 
            // tabMotorSettings
            // 
            this.tabMotorSettings.Controls.Add(this.tableLayoutPanel1);
            this.tabMotorSettings.Location = new System.Drawing.Point(4, 22);
            this.tabMotorSettings.Name = "tabMotorSettings";
            this.tabMotorSettings.Size = new System.Drawing.Size(764, 478);
            this.tabMotorSettings.TabIndex = 2;
            this.tabMotorSettings.Text = "Motor Settings";
            this.tabMotorSettings.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.44951F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.55049F));
            this.tableLayoutPanel1.Controls.Add(this.ctrDynamixelScanner1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctrPresentValue1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctrProperties1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctrMotorPosition1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 183F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 478);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tabCAN
            // 
            this.tabCAN.Controls.Add(this.ctrCanConnector1);
            this.tabCAN.Location = new System.Drawing.Point(4, 22);
            this.tabCAN.Name = "tabCAN";
            this.tabCAN.Padding = new System.Windows.Forms.Padding(3);
            this.tabCAN.Size = new System.Drawing.Size(764, 478);
            this.tabCAN.TabIndex = 3;
            this.tabCAN.Text = "CAN";
            this.tabCAN.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCAN);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabMotorSettings);
            this.tabControl.Controls.Add(this.tabNewMotorInitialization);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(772, 504);
            this.tabControl.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrGripper1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(764, 478);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Gripper Control";
            // 
            // tabNewMotorInitialization
            // 
            this.tabNewMotorInitialization.Controls.Add(this.ctrMotorInitialization1);
            this.tabNewMotorInitialization.Location = new System.Drawing.Point(4, 22);
            this.tabNewMotorInitialization.Name = "tabNewMotorInitialization";
            this.tabNewMotorInitialization.Size = new System.Drawing.Size(764, 478);
            this.tabNewMotorInitialization.TabIndex = 4;
            this.tabNewMotorInitialization.Text = "Initialize New Motor";
            this.tabNewMotorInitialization.UseVisualStyleBackColor = true;
            // 
            // tmrSendCANMessage
            // 
            this.tmrSendCANMessage.Enabled = true;
            this.tmrSendCANMessage.Interval = 5;
            this.tmrSendCANMessage.Tick += new System.EventHandler(this.tmrSendCANMessage_Tick);
            // 
            // ctrCanConnector1
            // 
            this.ctrCanConnector1.Location = new System.Drawing.Point(6, 6);
            this.ctrCanConnector1.Margin = new System.Windows.Forms.Padding(6);
            this.ctrCanConnector1.Name = "ctrCanConnector1";
            this.ctrCanConnector1.Size = new System.Drawing.Size(302, 222);
            this.ctrCanConnector1.TabIndex = 0;
            // 
            // ctrDynamixelScanner1
            // 
            this.ctrDynamixelScanner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrDynamixelScanner1.Location = new System.Drawing.Point(1, 1);
            this.ctrDynamixelScanner1.Margin = new System.Windows.Forms.Padding(1);
            this.ctrDynamixelScanner1.Name = "ctrDynamixelScanner1";
            this.tableLayoutPanel1.SetRowSpan(this.ctrDynamixelScanner1, 2);
            this.ctrDynamixelScanner1.Size = new System.Drawing.Size(148, 476);
            this.ctrDynamixelScanner1.TabIndex = 0;
            // 
            // ctrPresentValue1
            // 
            this.ctrPresentValue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrPresentValue1.Location = new System.Drawing.Point(153, 3);
            this.ctrPresentValue1.Name = "ctrPresentValue1";
            this.ctrPresentValue1.Size = new System.Drawing.Size(401, 177);
            this.ctrPresentValue1.TabIndex = 5;
            // 
            // ctrProperties1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ctrProperties1, 2);
            this.ctrProperties1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrProperties1.Location = new System.Drawing.Point(152, 185);
            this.ctrProperties1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrProperties1.Name = "ctrProperties1";
            this.ctrProperties1.Size = new System.Drawing.Size(610, 291);
            this.ctrProperties1.TabIndex = 7;
            // 
            // ctrMotorPosition1
            // 
            this.ctrMotorPosition1.Angle = 0F;
            this.ctrMotorPosition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrMotorPosition1.Location = new System.Drawing.Point(559, 2);
            this.ctrMotorPosition1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrMotorPosition1.Name = "ctrMotorPosition1";
            this.ctrMotorPosition1.Size = new System.Drawing.Size(203, 179);
            this.ctrMotorPosition1.TabIndex = 8;
            // 
            // ctrMotorInitialization1
            // 
            this.ctrMotorInitialization1.Location = new System.Drawing.Point(8, 12);
            this.ctrMotorInitialization1.Name = "ctrMotorInitialization1";
            this.ctrMotorInitialization1.Size = new System.Drawing.Size(511, 322);
            this.ctrMotorInitialization1.TabIndex = 0;
            // 
            // ctrGripper1
            // 
            this.ctrGripper1.Location = new System.Drawing.Point(8, 3);
            this.ctrGripper1.Name = "ctrGripper1";
            this.ctrGripper1.Size = new System.Drawing.Size(298, 251);
            this.ctrGripper1.TabIndex = 0;
            // 
            // frmGripper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 526);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusBar);
            this.Name = "frmGripper";
            this.Text = "Gripper Controler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGripper_FormClosing);
            this.Load += new System.EventHandler(this.frmGripper_Load);
            this.tabMotorSettings.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabCAN.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabNewMotorInitialization.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.TabPage tabMotorSettings;
        private System.Windows.Forms.TabPage tabCAN;
        private PCAN.UI.ctrCanConnector ctrCanConnector1;
        private System.Windows.Forms.TabControl tabControl;
        private Gripper.UI.ctrDynamixelScanner ctrDynamixelScanner1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer tmrSendCANMessage;
        private System.Windows.Forms.TabPage tabNewMotorInitialization;
        private Gripper.UI.ctrDynamixelInitialization ctrMotorInitialization1;
        private Gripper.UI.ctrDynamixelPresentValue ctrPresentValue1;
        private Gripper.UI.ctrDynamixelProperties ctrProperties1;
        private Gripper.UI.ctrDynamixelPosition ctrMotorPosition1;
        private System.Windows.Forms.TabPage tabPage1;
        private Gripper.UI.ctrGripper ctrGripper1;
    }
}

