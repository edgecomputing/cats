using System;
using System.Windows.Forms;

namespace Cats.TemplateEditor.Forms
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            btnUseCurrent.Enabled = false;
            txtUrl.Text = Properties.Settings.Default.ServerUrl.ToString();
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtUrl.Text = Properties.Settings.Default.ServerUrl.ToString();
        }

        private void btnUseCurrent_Click(object sender, EventArgs e)
        {

          if (MessageBox.Show(text: "This will overwrite the default url path to the server. Continue?", caption: "Warning", buttons: MessageBoxButtons.YesNo, icon: MessageBoxIcon.Warning)  == DialogResult.Yes)
          {
              Properties.Settings.Default["ServerUrl"] = txtUrl.Text;
              Properties.Settings.Default.Save();
          }
            
        }

        private void txtUrl_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            btnUseCurrent.Enabled = txtUrl.Text != Properties.Settings.Default["ServerUrl"].ToString();
        }
    }
}
