﻿namespace GripperControler.Dynamixel.UI
{
    partial class ctrProperties
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.columnMemory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnProperty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnMemory,
            this.columnProperty,
            this.columnValue});
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowTemplate.Height = 33;
            this.dataGrid.Size = new System.Drawing.Size(348, 309);
            this.dataGrid.TabIndex = 3;
            this.dataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellEndEdit);
            this.dataGrid.ColumnHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColumnHeaderMouseDoubleClick);
            // 
            // columnMemory
            // 
            this.columnMemory.Frozen = true;
            this.columnMemory.HeaderText = "Memory";
            this.columnMemory.MaxInputLength = 3;
            this.columnMemory.Name = "columnMemory";
            this.columnMemory.ReadOnly = true;
            this.columnMemory.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.columnMemory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnMemory.Width = 40;
            // 
            // columnProperty
            // 
            this.columnProperty.HeaderText = "Property";
            this.columnProperty.Name = "columnProperty";
            this.columnProperty.ReadOnly = true;
            this.columnProperty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.columnProperty.ToolTipText = "Double click to refresh all the properties.";
            this.columnProperty.Width = 130;
            // 
            // columnValue
            // 
            this.columnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnValue.HeaderText = "Value";
            this.columnValue.Name = "columnValue";
            this.columnValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ctrProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGrid);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ctrProperties";
            this.Size = new System.Drawing.Size(348, 309);
            this.Load += new System.EventHandler(this.ctrProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMemory;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnProperty;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
    }
}
