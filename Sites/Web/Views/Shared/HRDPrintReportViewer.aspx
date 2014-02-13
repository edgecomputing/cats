<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register assembly="Telerik.ReportViewer.WebForms, Version=7.0.13.228, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" namespace="Telerik.ReportViewer.WebForms" tagprefix="telerik" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta name="viewport" content="width=device-width" />
    <title>Report Viewer</title>
</head>
<body>
    <script runat="server">
        public override void VerifyRenderingInServerForm(Control control)
        {
            // to avoid the server form (<form runat="server"> requirement
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var instanceReportSource = new Telerik.Reporting.InstanceReportSource
                {ReportDocument = new ReportLib.HRDPrintable()};

            ReportViewer1.ReportSource = instanceReportSource;
        }

    </script>
    <form id="form1" runat="server">
        <telerik:ReportViewer ID="ReportViewer1" runat="server" Height="681px" Width="981px">
        </telerik:ReportViewer>
    </form>
</body>
</html>
