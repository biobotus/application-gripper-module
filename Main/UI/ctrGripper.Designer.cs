namespace GripperControler.Main.UI
{
    partial class ctrGripper
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
            this.btnOpenGripper = new System.Windows.Forms.Button();
            this.btnCloseGripper = new System.Windows.Forms.Button();
            this.txtPourcentage = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.txtPourcentage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenGripper
            // 
            this.btnOpenGripper.Location = new System.Drawing.Point(3, 3);
            this.btnOpenGripper.Name = "btnOpenGripper";
            this.btnOpenGripper.Size = new System.Drawing.Size(86, 37);
            this.btnOpenGripper.TabIndex = 1;
            this.btnOpenGripper.Text = "Open Gripper";
            this.btnOpenGripper.UseVisualStyleBackColor = true;
            this.btnOpenGripper.Click += new System.EventHandler(this.btnOpenGripper_Click);
            // 
            // btnCloseGripper
            // 
            this.btnCloseGripper.Location = new System.Drawing.Point(3, 46);
            this.btnCloseGripper.Name = "btnCloseGripper";
            this.btnCloseGripper.Size = new System.Drawing.Size(86, 37);
            this.btnCloseGripper.TabIndex = 2;
            this.btnCloseGripper.Text = "Close Gripper";
            this.btnCloseGripper.UseVisualStyleBackColor = true;
            this.btnCloseGripper.Click += new System.EventHandler(this.btnCloseGripper_Click);
            // 
            // txtPourcentage
            // 
            this.txtPourcentage.Location = new System.Drawing.Point(95, 20);
            this.txtPourcentage.Name = "txtPourcentage";
            this.txtPourcentage.Size = new System.Drawing.Size(43, 20);
            this.txtPourcentage.TabIndex = 20;
            this.txtPourcentage.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Pourcentage:";
            // 
            // ctrGripper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPourcentage);
            this.Controls.Add(this.btnCloseGripper);
            this.Controls.Add(this.btnOpenGripper);
            this.Name = "ctrGripper";
            this.Size = new System.Drawing.Size(298, 251);
            ((System.ComponentModel.ISupportInitialize)(this.txtPourcentage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOpenGripper;
        private System.Windows.Forms.Button btnCloseGripper;
        private System.Windows.Forms.NumericUpDown txtPourcentage;
        private System.Windows.Forms.Label label1;
    }
}
