namespace Cats.TemplateEditor.Forms
{
    partial class ObjectList
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
            this.LstFields = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbTemplateTypes = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LstTemplates
            // 
            this.LstTemplates.FormattingEnabled = true;
            this.LstTemplates.Location = new System.Drawing.Point(12, 87);
            this.LstTemplates.Name = "LstTemplates";
            this.LstTemplates.ScrollAlwaysVisible = true;
            this.LstTemplates.Size = new System.Drawing.Size(176, 199);
            this.LstTemplates.TabIndex = 0;
            this.LstTemplates.Click += new System.EventHandler(this.LstObjects_Click);
            // 
            // LstFields
            // 
            this.LstFields.FormattingEnabled = true;
            this.LstFields.Location = new System.Drawing.Point(215, 87);
            this.LstFields.Name = "LstFields";
            this.LstFields.ScrollAlwaysVisible = true;
            this.LstFields.Size = new System.Drawing.Size(176, 199);
            this.LstFields.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(316, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbTemplateTypes
            // 
            this.cmbTemplateTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplateTypes.FormattingEnabled = true;
            this.cmbTemplateTypes.Location = new System.Drawing.Point(163, 12);
            this.cmbTemplateTypes.Name = "cmbTemplateTypes";
            this.cmbTemplateTypes.Size = new System.Drawing.Size(145, 21);
            this.cmbTemplateTypes.TabIndex = 3;
            this.cmbTemplateTypes.SelectedValueChanged += new System.EventHandler(this.cmbTemplateTypes_SelectedValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 302);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(176, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Publish to server when saving...";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ObjectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 331);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmbTemplateTypes);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.LstFields);
            this.Controls.Add(this.LstTemplates);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ObjectList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Field";
            this.Load += new System.EventHandler(this.ObjectList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LstTemplates;
        private System.Windows.Forms.ListBox LstFields;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbTemplateTypes;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}