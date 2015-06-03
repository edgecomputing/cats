<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmharicReportViewer.aspx.cs" Inherits="Cats.AmharicReportViewer" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript" src="~/Scripts/Beka.EthDate/Beka.EthDate.js"> </script>
    <script type="text/javascript" src="~/Scripts/Beka.EthDate/jquery.Beka.EthCalDatePicker.js"> </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManagerAmh" runat="server">
        </asp:ScriptManager>
        Start Date:&nbsp;<asp:TextBox ID="txtStartDate" class="cats-datepicker2  input-medium" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        End Date:
        <asp:TextBox ID="txtEndDate" class="cats-datepicker2  input-medium" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <rsweb:ReportViewer ID="ReportViewerAmh"  runat="server" SizeToReportContent="True" ShowPrintButton="true" KeepSessionAlive="true"  ZoomMode="PageWidth" Height="100%" Width="100%">
            </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
