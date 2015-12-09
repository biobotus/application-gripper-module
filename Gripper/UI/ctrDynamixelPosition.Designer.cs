namespace Gripper.UI
{
    partial class ctrDynamixelPosition
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
            this.components = new System.ComponentModel.Container();
            this.tmrCurrentPositionRequest = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrCurrentPositionRequest
            // 
            this.tmrCurrentPositionRequest.Enabled = true;
            // 
            // ctrMotorPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ctrMotorPosition";
            this.Size = new System.Drawing.Size(100, 104);
            this.Load += new System.EventHandler(this.ctrMotorPosition_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ctrMotorPosition_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ctrMotorPosition_MouseDoubleClick);
            this.Resize += new System.EventHandler(this.ctrMotorPosition_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrCurrentPositionRequest;
    }
}
