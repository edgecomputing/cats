namespace Cats.TemplateEditor
{
    partial class CatsTemplateRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CatsTemplateRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.btnSetings = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.btnAddNew = this.Factory.CreateRibbonButton();
            this.btnLogIn = this.Factory.CreateRibbonButton();
            this.saveDlg = new System.Windows.Forms.SaveFileDialog();
            this.createGroup = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.button4 = this.Factory.CreateRibbonButton();
            this.button5 = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.createGroup.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.createGroup);
            this.tab1.Groups.Add(this.group2);
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "CATS";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.btnSetings);
            this.group1.Items.Add(this.btnLogIn);
            this.group1.Label = "Settings";
            this.group1.Name = "group1";
            // 
            // btnSetings
            // 
            this.btnSetings.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnSetings.Description = "CATS server connection url";
            this.btnSetings.Label = "Server Settings";
            this.btnSetings.Name = "btnSetings";
            this.btnSetings.OfficeImageId = "ServerProperties";
            this.btnSetings.ScreenTip = "Provide connection information for CATS server";
            this.btnSetings.ShowImage = true;
            this.btnSetings.SuperTip = "Please provide the server information where CATS is installed on. This informatio" +
    "n is usually in the form of a web url (eg. http://www.catsproject.org/TemplateSe" +
    "rver.svc)";
            this.btnSetings.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // group2
            // 
            this.group2.Items.Add(this.button3);
            this.group2.Items.Add(this.btnAddNew);
            this.group2.Items.Add(this.button4);
            this.group2.Label = "Templates";
            this.group2.Name = "group2";
            // 
            // btnAddNew
            // 
            this.btnAddNew.Label = "Add/EditTemplate";
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.OfficeImageId = "AddToMySite";
            this.btnAddNew.ShowImage = true;
            this.btnAddNew.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnAddNew_Click);
            // 
            // btnLogIn
            // 
            this.btnLogIn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnLogIn.Label = "Login";
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.OfficeImageId = "SiteCollectionAdmins";
            this.btnLogIn.ShowImage = true;
            this.btnLogIn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnLogIn_Click);
            // 
            // createGroup
            // 
            this.createGroup.Items.Add(this.button1);
            this.createGroup.Items.Add(this.button2);
            this.createGroup.Items.Add(this.button5);
            this.createGroup.Label = "Authoring";
            this.createGroup.Name = "createGroup";
            // 
            // button1
            // 
            this.button1.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button1.Label = "New Template";
            this.button1.Name = "button1";
            this.button1.OfficeImageId = "NewCategoryFolder";
            this.button1.ShowImage = true;
            // 
            // button2
            // 
            this.button2.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button2.Label = "Open Template";
            this.button2.Name = "button2";
            this.button2.OfficeImageId = "NewDocumentsTool";
            this.button2.ShowImage = true;
            // 
            // button3
            // 
            this.button3.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button3.Label = "Preview Template";
            this.button3.Name = "button3";
            this.button3.OfficeImageId = "PrintPreviewFullScreen";
            this.button3.ShowImage = true;
            // 
            // button4
            // 
            this.button4.Label = "Insert Placeholder";
            this.button4.Name = "button4";
            this.button4.OfficeImageId = "InsertCellMenu";
            this.button4.ShowImage = true;
            // 
            // button5
            // 
            this.button5.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.button5.Label = "Save";
            this.button5.Name = "button5";
            this.button5.OfficeImageId = "SaveAndCloseConflictView";
            this.button5.ShowImage = true;
            // 
            // CatsTemplateRibbon
            // 
            this.Name = "CatsTemplateRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.CatsTemplateRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.createGroup.ResumeLayout(false);
            this.createGroup.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSetings;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnAddNew;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLogIn;
        private System.Windows.Forms.SaveFileDialog saveDlg;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup createGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button5;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button4;
    }

    partial class ThisRibbonCollection
    {
        internal CatsTemplateRibbon CatsTemplateRibbon
        {
            get { return this.GetRibbon<CatsTemplateRibbon>(); }
        }
    }
}
