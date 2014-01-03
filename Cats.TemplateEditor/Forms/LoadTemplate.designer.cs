namespace Cats.TemplateEditor.Forms
{
    partial class LoadTemplate
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
            this.LstTemplates = new System.Windows.Forms.ListBox();
            this.btnView = new System.Windows.Forms.Button();
            this.cmbTemplateTypes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LstTemplates
            // 
            this.LstTemplates.FormattingEnabled = true;
            this.LstTemplates.Location = new System.Drawing.Point(15, 66);
            this.LstTemplates.Name = "LstTemplates";
            this.LstTemplates.ScrollAlwaysVisible = true;
            this.LstTemplates.Size = new System.Drawing.Size(249, 199);
            this.LstTemplates.TabIndex = 7;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(189, 271);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // cmbTemplateTypes
            // 
            this.cmbTemplateTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplateTypes.FormattingEnabled = true;
            this.cmbTemplateTypes.Location = new System.Drawing.Point(96, 39);
            this.cmbTemplateTypes.Name = "cmbTemplateTypes";
            this.cmbTemplateTypes.Size = new System.Drawing.Size(168, 21);
            this.cmbTemplateTypes.TabIndex = 9;
            this.cmbTemplateTypes.SelectedValueChanged += new System.EventHandler(this.cmbTemplateTypes_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Template Type";
            // 
            // LoadTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 353);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTemplateTypes);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.LstTemplates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load Template";
            this.Load += new System.EventHandler(this.LoadTemplate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LstTemplates;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ComboBox cmbTemplateTypes;
        private System.Windows.Forms.Label label1;

    }
}