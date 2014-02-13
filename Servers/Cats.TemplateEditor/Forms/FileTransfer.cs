using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Cats.TemplateEditor.TemplateService;
using Microsoft.Office.Interop.Word;

namespace Cats.TemplateEditor.Forms
{
    public partial class FileTransfer : Form
    {
        public FileTransfer()
        {
            InitializeComponent();
        }

        private void FileTransfer_Load(object sender, EventArgs e)
        {
            
            GetTemplateTypes();
            RefreshFileList();
            
        }

        private void RefreshFileList()
        {
            StorageFileInfo[] files = null;

            using (TemplateManagerClient client = new TemplateManagerClient())
            {
                files = client.List(null);
            }

            FileList.Items.Clear();

            int width = FileList.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;

            float[] widths = { .2f, .6f, .2f };

            for (int i = 0; i < widths.Length; i++)
                FileList.Columns[i].Width = (int)((float)width * widths[i]);

            foreach (var file in files)
            {
                ListViewItem item = new ListViewItem(Path.GetFileName(file.VirtualPathk__BackingField));

                if (CheckFile(int.Parse(cmbTemplateTypes.SelectedValue.ToString()), file.VirtualPathk__BackingField))
                {
                    item.SubItems.Add(file.VirtualPathk__BackingField);

                    float fileSize = (float) file.Sizek__BackingField/1024.0f;
                    string suffix = "Kb";

                    if (fileSize > 1000.0f)
                    {
                        fileSize /= 1024.0f;
                        suffix = "Mb";
                    }
                    item.SubItems.Add(string.Format("{0:0.0} {1}", fileSize, suffix));

                    FileList.Items.Add(item);
                }
            }
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            Download();
        }

       

        private void DeleteButton_Click(object sender, EventArgs e)
        {

            if (FileList.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a file to delete");
            }
            else
            {
                string virtualPath = FileList.SelectedItems[0].SubItems[1].Text;

                using (TemplateManagerClient client = new TemplateManagerClient())
                {
                    client.DeleteFile(virtualPath);
                }

                RefreshFileList();
            }

        }


        

        private void Download()
        {

            var filePath = string.Empty;
            if (FileList.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a file to download");
            }
            else
            {
                var documentProcessorService = new DocumentProcessorService();
                string path = documentProcessorService.DownloadDocument(FileList);
                if (path!=string.Empty)
                {
                    this.Close();
                    this.Dispose();
                    Process.Start("WINWORD.EXE",path);
                }
            }
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

        private static string AppendToFileName(int templateType,string fileName)
        {
            switch (templateType)
            {
                case 1:
                    if (fileName.StartsWith("TRANS-"))
                        return fileName;
                    return "TRANS-" + fileName;
                case 2:
                    if (fileName.StartsWith("GIFT-"))
                        return fileName;
                    return "GIFT-" + fileName;
                default:
                    return fileName;

            }
        }


        private bool CheckFile(int templateType, string fileName)
        {
            if (templateType == 1 && !fileName.StartsWith("TRANS-"))
                return false;
            if (templateType == 2 && !fileName.StartsWith("GIFT-"))
                return false;
            return true;
        }

        private void cmbTemplateTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshFileList();  
            }catch
            {
                
            }
            
        }
    }
}
