namespace Cats.Web.Hub.Reports
{
    partial class DistributionReportPivot
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.bindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.fieldBudgetYear1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldProgram1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldRegion1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldQuarter1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMonth1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDistributedAmount1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 396.475F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.CustomTotalCell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrPivotGrid1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.xrPivotGrid1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Silver;
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Verdana", 8F);
            this.xrPivotGrid1.Appearance.FieldHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.Gainsboro;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Verdana", 9F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Verdana", 8F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Blue;
            this.xrPivotGrid1.Appearance.HeaderGroupLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Verdana", 8F);
            this.xrPivotGrid1.DataSource = this.bindingSource3;
            this.xrPivotGrid1.Dpi = 254F;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldBudgetYear1,
            this.fieldProgram1,
            this.fieldRegion1,
            this.fieldQuarter1,
            this.fieldMonth1,
            this.fieldDistributedAmount1});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0.0002018611F, 0F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OLAPConnectionString = "";
            this.xrPivotGrid1.OptionsChartDataSource.UpdateDelay = 300;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1987F, 371.475F);
            // 
            // bindingSource3
            // 
            this.bindingSource3.DataSource = typeof(Cats.Models.Hub.ViewModels.Report.Data.DistributionRows);
            // 
            // fieldBudgetYear1
            // 
            this.fieldBudgetYear1.AreaIndex = 0;
            this.fieldBudgetYear1.Caption = "Budget Year";
            this.fieldBudgetYear1.FieldName = "BudgetYear";
            this.fieldBudgetYear1.Name = "fieldBudgetYear1";
            // 
            // fieldProgram1
            // 
            this.fieldProgram1.AreaIndex = 1;
            this.fieldProgram1.Caption = "Program";
            this.fieldProgram1.FieldName = "Program";
            this.fieldProgram1.Name = "fieldProgram1";
            // 
            // fieldRegion1
            // 
            this.fieldRegion1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldRegion1.AreaIndex = 0;
            this.fieldRegion1.Caption = "Region";
            this.fieldRegion1.FieldName = "Region";
            this.fieldRegion1.Name = "fieldRegion1";
            this.fieldRegion1.Width = 150;
            // 
            // fieldQuarter1
            // 
            this.fieldQuarter1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldQuarter1.AreaIndex = 0;
            this.fieldQuarter1.Caption = "Quarter";
            this.fieldQuarter1.FieldName = "Quarter";
            this.fieldQuarter1.Name = "fieldQuarter1";
            // 
            // fieldMonth1
            // 
            this.fieldMonth1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldMonth1.AreaIndex = 1;
            this.fieldMonth1.Caption = "Month";
            this.fieldMonth1.FieldName = "Month";
            this.fieldMonth1.Name = "fieldMonth1";
            this.fieldMonth1.Width = 190;
            // 
            // fieldDistributedAmount1
            // 
            this.fieldDistributedAmount1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldDistributedAmount1.AreaIndex = 0;
            this.fieldDistributedAmount1.Caption = "Distributed Amount";
            this.fieldDistributedAmount1.FieldName = "DistributedAmount";
            this.fieldDistributedAmount1.Name = "fieldDistributedAmount1";
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 18F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 51F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 254F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // DistributionReportPivot
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(36, 53, 18, 51);
            this.PageHeight = 2969;
            this.PageWidth = 2101;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private System.Windows.Forms.BindingSource bindingSource3;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBudgetYear1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldProgram1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldRegion1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldQuarter1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMonth1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDistributedAmount1;
        public DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
    }
}
