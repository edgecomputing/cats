<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AmharicReportViewer.aspx.cs" Inherits="Cats.AmharicReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Scripts/Beka.EthDate/jquery-ui-1.9.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/Beka.EthDate/calendar.css" rel="stylesheet" type="text/css" />
    <script src="/Content/assetss/global/plugins/jquery.min.js" type="text/javascript"></script>
    <link href="~/Content/assetss/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/>

    <script type="text/javascript" src="/Scripts/Beka.EthDate/Beka.EthDate.js"> </script>
    <script type="text/javascript" src="/Scripts/Beka.EthDate/jquery.Beka.EthCalDatePicker.js"> </script>
    <script src="/Content/assets/js/bootstrap-tooltip.js"></script>
    <script src="/Content/assetss/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="/Scripts/js/CatsUI.js"> </script>--%>
    <script>
        $(function () {
            $(".cats-datepicker2").ethcal_datepicker();
        });
        //init_datepicker("EN");
    </script>
    <style>
        input[type="text"] {
            padding: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManagerAmh" runat="server">
            </asp:ScriptManager>
            <div class="row">
                <div class="col-sm-4">
                    <label class="" for="txtStartDate">Start Date:</label>
                    <asp:TextBox ID="txtStartDate" class="cats-datepicker2  input-medium" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <label class="" for="txtStartDate">End Date:</label>
                    <asp:TextBox ID="txtEndDate" class="cats-datepicker2  input-medium" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-1">
                    <asp:Button ID="Button1" runat="server" Text="View Report" OnClick="Button1_Click" />
                </div>
            </div>
            <%--Start Date:&nbsp;--%>
            <%--<asp:TextBox ID="txtStartDate" class="cats-datepicker2  input-medium" runat="server"></asp:TextBox>--%>
            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; --%>
        <%--End Date:--%>
        <%--<asp:TextBox ID="txtEndDate" class="cats-datepicker2  input-medium" runat="server"></asp:TextBox>--%>
            <%--<asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />--%>
            <rsweb:ReportViewer ID="ReportViewerAmh" runat="server" SizeToReportContent="True" ShowPrintButton="true" KeepSessionAlive="true" ZoomMode="PageWidth" Height="100%" Width="100%">
            </rsweb:ReportViewer>

        </div>
    </form>
    
</body>
</html>
