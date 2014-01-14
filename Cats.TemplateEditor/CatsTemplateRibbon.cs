using System;
using System.Windows.Forms;
using Cats.TemplateEditor.Forms;
using Cats.TemplateEditor.TemplateService;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;

namespace Cats.TemplateEditor
{
    public partial class CatsTemplateRibbon
    {
        private void CatsTemplateRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            DisableRibbon();
            //InitLoginForm();
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            var settingDialog = new Setting();
            settingDialog.ShowDialog();
        }

        private void btnEdit_Click(object sender, RibbonControlEventArgs e)
        {
            var load = new FileTransfer();
            load.ShowDialog();
        }

        private void btnAddNew_Click(object sender, RibbonControlEventArgs e)
        {
            var editTemp = new ObjectList();
            editTemp.ShowDialog();
        }

        private void btnSave_Click(object sender, RibbonControlEventArgs e)
        {
            //Microsoft.Office.Interop.Word.Application oWord = new Application();

            //oWord.Visible = true;

            //var oDoc = oWord.Documents.Add();

        }

        private void btnLogIn_Click(object sender, RibbonControlEventArgs e)
        {
            var login = new Login();
            login.ShowDialog();
        }

        private void DisableRibbon()
        {
            btnAddNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSetings.Enabled = false;
            
        }

        private void EnableRibbon()
        {
            btnAddNew.Enabled = true;
            btnEdit.Enabled = true;
            btnSetings.Enabled = true;
            btnLogIn.Visible = false;
        }

        public void InitLoginForm()
        {
            var login = new Login();
            login.ShowDialog();

            if (login.IsLoggedIn)
            {
                EnableRibbon();
            }
            else
            {
                DisableRibbon();
            }

        }

        private void btnSave_Click_1(object sender, RibbonControlEventArgs e)
        {
            IDocumentProcessorService documentProcessorService =  new DocumentProcessorService();


             _Application wordApp;
            try
            {
                wordApp = (_Application) System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            }
            catch (Exception)
            {

                wordApp = new Microsoft.Office.Interop.Word.Application();
            }

            string newFileName;
            var aDoc = wordApp.ActiveDocument;
            string fileName = aDoc.Name;

            int dotPosition = fileName.IndexOf(".", 1, System.StringComparison.Ordinal);

            switch (dotPosition)
            {
                case -1:
                    {
                        // Ask where it should be saved
                        var dlg = new SaveFileDialog()
                                      {
                                          RestoreDirectory = true,
                                          OverwritePrompt = true,
                                          Title = "Enter the file name of the template",
                                      };
                        dlg.ShowDialog();
                        fileName = dlg.FileName;
                    }
                    break;
                default:
                    fileName = fileName.Substring(0, aDoc.Name.Length  - (aDoc.Name.Length - dotPosition));
                    fileName = Properties.Settings.Default.DefaultPath.ToString() + fileName;
                    break;
            }

            if (fileName.Trim() == string.Empty)
                return;
            aDoc.SaveAs(fileName,WdSaveFormat.wdFormatTemplate);
            aDoc.Close();
            documentProcessorService.UploadDocument(1,fileName + ".dot");
            documentProcessorService.DeleteDocument(fileName + ".dot");
            InsertToLetterTemplate(fileName, GetTemplateType(fileName));
            MessageBox.Show("Template is Uploaded.", "Saving", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }

        private int GetTemplateType(string fileName)
        {
                    fileName = fileName.Substring(3);
            MessageBox.Show(fileName);
                    if (fileName.StartsWith("TRANS-"))
                        return 1;
                    if (fileName.StartsWith("GIFT-"))
                        return 2;
                    return 0;
                

            
        }

        private void InsertToLetterTemplate(string name,int templateType)
        {
            name = name.Substring(3);
            var letter = new LetterTemplate()
                                        {
                                            Name = name,
                                            FileName = name,
                                            TemplateType = templateType
                                        };

            var client= new TemplateManagerClient();
            client.InsertToLetterTemplate(letter);

        }
    }
}
