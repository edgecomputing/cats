using Cats.TemplateEditor.Forms;
using Microsoft.Office.Tools.Ribbon;

namespace Cats.TemplateEditor
{
    public partial class CatsTemplateRibbon
    {
        private void CatsTemplateRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            DisableRibbon();
            InitLoginForm();
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
            //btnEdit.Enabled = false;
            btnSetings.Enabled = false;
            
        }

        private void EnableRibbon()
        {
            btnAddNew.Enabled = true;
            //btnEdit.Enabled = true;
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
    }
}
