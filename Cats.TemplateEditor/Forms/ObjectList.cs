using System;
using System.Windows.Forms;
using Cats.TemplateEditor.TemplateService;

namespace Cats.TemplateEditor.Forms
{
    public partial class ObjectList : Form
    {
        public ObjectList()
        {
            InitializeComponent();
        }

        private void ObjectList_Load(object sender, EventArgs e)
        {
            LoadTemplateTypes();
        }

        private void LoadTemplateTypes()
        {

            var client = new TemplateManagerClient();
            cmbTemplateTypes.DataSource = null;
            cmbTemplateTypes.DataSource = client.GetTemplateTypes();
            cmbTemplateTypes.DisplayMember = "TemplateObject";
            cmbTemplateTypes.ValueMember = "TemplateTypeId";
            client.Close();
           
        }

        private void GetTemplates(int templetTypeId)
        {
            var client = new TemplateManagerClient();
            LstTemplates.DataSource = null;
            LstTemplates.DataSource = client.GetTemplates(templetTypeId);
            LstTemplates.DisplayMember = "Name";
            LstTemplates.ValueMember = "TemplateId";
            client.Close();
        }

        private void ShowFields(int templateId)
        {
            var client = new TemplateManagerClient();

            LstFields.DataSource = null;
            LstFields.Items.Clear();
            LstFields.DataSource = client.GetFieldsByTemplateId(templateId);
            LstFields.DisplayMember = "Field";
            LstFields.ValueMember = "TemplateId";
            client.Close();
        }

        private void cmbTemplateTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GetTemplates(int.Parse(cmbTemplateTypes.SelectedValue.ToString()));
                LstFields.DataSource = null;
                LstFields.Items.Clear();
            }
            catch (Exception)
            {
                
                
            }
            
        }

        private void LstObjects_Click(object sender, EventArgs e)
        {
            try
            {
                ShowFields(int.Parse(LstTemplates.SelectedValue.ToString()));
            }
            catch (Exception)
            {
                
                
            }
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

     
            // insert merge field at the current cursor location
            Microsoft.Office.Interop.Word.Application
              word = Globals.ThisAddIn.Application;

           
            Microsoft.Office.Interop.Word.Range selection = word.Selection.Range;
           // selection.Text = LstFields.Text;

             Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            doc.MailMerge.Fields.Add(selection,LstFields.Text);

            this.Close();
            this.Dispose();
        }
    }
}
