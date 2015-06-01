<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmharicReportViewer.aspx.cs" Inherits="Cats.AmharicReportViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManagerAmh" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewerAmh" runat="server" Height="149px" Width="805px">
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
