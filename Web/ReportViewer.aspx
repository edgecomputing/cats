<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="SSRS_Portal.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CATS: Commodity Allocation & Tracking System</title>
    <%--<script src="/Scripts/kendo/2013.1.319/jquery.min.js"></script>
    <link href="/Content/themes/default/bootstrap.min.css" rel="stylesheet"/>--%>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="True" ShowPrintButton="true" KeepSessionAlive="true"  ZoomMode="PageWidth" Height="100%" Width="100%">
            </rsweb:ReportViewer>
        </div>
    </form>
    <%--<script >
        $(document).ready(function() {
            //alert("hello report");
        });
        $("#ReportViewer1_ctl04").css("background", "none");
        $("#ParameterTable_ReportViewer1_ctl04").css("background", "none");
    </script>--%>
    

</body>

</html>
