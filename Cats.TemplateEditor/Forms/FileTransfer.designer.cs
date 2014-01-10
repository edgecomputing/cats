namespace Cats.TemplateEditor.Forms
{
    partial class FileTransfer
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
            this.FileList = new System.Windows.Forms.ListView();
            this.FilenameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FullPathHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DeleteButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.cmbTemplateTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // FileList
            // 
            this.FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FilenameColumnHeader,
            this.FullPathHeader,
            this.SizeHeader,
            this.columnHeader1});
            this.FileList.FullRowSelect = true;
            this.FileList.GridLines = true;
            this.FileList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.FileList.Location = new System.Drawing.Point(26, 62);
            this.FileList.Name = "FileList";
            this.FileList.Size = new System.Drawing.Size(427, 192);
            this.FileList.TabIndex = 12;
            this.FileList.UseCompatibleStateImageBehavior = false;
            this.FileList.View = System.Windows.Forms.View.Details;
            // 
            // FilenameColumnHeader
            // 
            this.FilenameColumnHeader.Text = "Filename";
            this.FilenameColumnHeader.Width = 80;
            // 
            // FullPathHeader
            // 
            this.FullPathHeader.Text = "Storage path";
            this.FullPathHeader.Width = 160;
            // 
            // SizeHeader
            // 
            this.SizeHeader.Text = "Size";
            this.SizeHeader.Width = 80;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(277, 25);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(85, 31);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "&Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(368, 25);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(85, 31);
            this.DownloadButton.TabIndex = 9;
            this.DownloadButton.Text = "&Open";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // cmbTemplateTypes
            // 
            this.cmbTemplateTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplateTypes.FormattingEnabled = true;
            this.cmbTemplateTypes.Location = new System.Drawing.Point(26, 35);
            this.cmbTemplateTypes.Name = "cmbTemplateTypes";
            this.cmbTemplateTypes.Size = new System.Drawing.Size(163, 21);
            this.cmbTemplateTypes.TabIndex = 13;
            this.cmbTemplateTypes.SelectedIndexChanged += new System.EventHandler(this.cmbTemplateTypes_SelectedIndexChanged);
            // 
            // FileTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 269);
            this.Controls.Add(this.FileList);
            this.Controls.Add(this.cmbTemplateTypes);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.DownloadButton);
            this.Name = "FileTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileTransfer";
            this.Load += new System.EventHandler(this.FileTransfer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView FileList;
        private System.Windows.Forms.ColumnHeader FilenameColumnHeader;
        private System.Windows.Forms.ColumnHeader FullPathHeader;
        private System.Windows.Forms.ColumnHeader SizeHeader;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.ComboBox cmbTemplateTypes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}