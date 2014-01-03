using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Cats.TemplateEditor.TemplateService;

namespace Cats.TemplateEditor.Forms
{
    public partial class LoadTemplate : Form
    {
        public LoadTemplate()
        {
            InitializeComponent();
        }

        private void LoadTemplate_Load(object sender, EventArgs e)
        {
            LoadTemplateTypes();
        }
        private void LoadTemplateTypes()
        {
           GetTemplateTypes();
            
        }

       
        private void cmbTemplateTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                PopulateTemplates(int.Parse(cmbTemplateTypes.SelectedValue.ToString()));
                
            }
            catch (Exception)
            {


            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            
                //ListViewItem item = FileList.SelectedItems[0];

                // Strip off 'Root' from the full path
            string path = "storage/partial1.html";// item.SubItems[1].Text;

                // Ask where it should be saved
                SaveFileDialog dlg = new SaveFileDialog()
                {
                    RestoreDirectory = true,
                    OverwritePrompt = true,
                    Title = "Save as...",
                    FileName = Path.GetFileName(path)
                };

                dlg.ShowDialog(this);

                if (!string.IsNullOrEmpty(dlg.FileName))
                {
                    // Get the file from the server
                    using (FileStream output = new FileStream(dlg.FileName, FileMode.Create))
                    {
                        Stream downloadStream;

                        using (TemplateManagerClient client = new TemplateManagerClient())
                        {
                            downloadStream = client.GetFile(path);
                        }

                        downloadStream.CopyTo(output);
                    }

                    Process.Start(dlg.FileName);
                
            }
            //OpenFileDialog dlg = new OpenFileDialog()
            //{
            //    Title = "Select a file to upload",
            //    RestoreDirectory = true,
            //    CheckFileExists = true
            //};

            //dlg.ShowDialog();

            //if (!string.IsNullOrEmpty(dlg.FileName))
            //{
            //    string virtualPath = Path.GetFileName(dlg.FileName);

            //    using (Stream uploadStream = new FileStream(dlg.FileName, FileMode.Open))
            //    {
            //        using (var client = new FileRepositoryServiceClient())
            //        {
            //            client.PutFile(new FileUploadMessage() { VirtualPath = virtualPath, DataStream = uploadStream });
            //        }
            //    }

               
            //}
        }


        private  void PopulateTemplates(int templetId)
        {

            var client = new TemplateManagerClient();
            LstTemplates.DataSource = null;
            LstTemplates.DataSource = client.GetTemplates(templetId);
            LstTemplates.DisplayMember = "Name";
            LstTemplates.ValueMember = "TemplateId";
            client.Close();
        }

        private void GetTemplateTypes()
        {
            var client = new TemplateManagerClient();
            cmbTemplateTypes.DataSource = null;
            cmbTemplateTypes.DataSource = client.GetTemplateTypes();
            cmbTemplateTypes.DisplayMember = "TemplateObject";
            cmbTemplateTypes.ValueMember = "TemplateTypeId";
            client.Close();
        }
    }
}
