<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="SSRS_Portal.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CATS: Commodity Allocation & Tracking System</title>
    <%--<link href="~/Scripts/angular/css/bootstrap.min.css" rel="stylesheet"/>--%>

   <%-- <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="~/Content/assets/css/font-awesome.css" rel="stylesheet" />
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/themes/dashboard.css" rel="stylesheet" />
    <link href="~/Content/themes/default/bootstrap.min.css" rel="stylesheet">
    <link href="~/Content/assets/css/font-awesome.css" rel="stylesheet" />
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/Site.css" rel="stylesheet" />
    <link href="~/Content/themes/dashboard.css" rel="stylesheet" />
    <link href="~/Content/kendo/2013.1.319/kendo.common.min.css" rel="stylesheet" />
    <link href="~/Content/kendo/2013.1.319/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/kendo/2013.1.319/kendo.bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!--
    <link href="~/Content/kendo/2013.1.319/kendo.default.min.css" rel="stylesheet" />--%>
	
   <%--     -->
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="~/Scripts/kendo/2013.1.319/jquery.min.js">
    </script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="~/Scripts/kendo/2013.1.319/kendo.all.min.js"></script>
    <script src="~/Scripts/kendo/2013.1.319/kendo.aspnetmvc.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="~/Scripts/angular/angular.js"> </script>
    <script src="~/Scripts/angular/angular-resource.js"></script>
    <%--<script src="Scripts/bootstrap.min.js"></script>--%>

<%--    <style type="text/css">
        body {
            padding-top: 0px;
            padding-bottom: 40px;
        }
        .navbar {
            width: 100%;
        }
    </style>--%>
</head>
<body>
    <%--<a class="btn" data-type="btn_back_to_list" href="Home/ReportListing" title="Back to Report List"><i class="icon-list"></i></a>--%>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%">
            </rsweb:ReportViewer>
        </div>
    </form>

    <%--<script src="~/Content/assets/js/bootstrap-transition.js"></script>
    <script src="~/Content/assets/js/bootstrap-alert.js"></script>
    <script src="~/Content/assets/js/bootstrap-modal.js"></script>
    <script src="~/Content/assets/js/bootstrap-dropdown.js"></script>
    <script src="~/Content/assets/js/bootstrap-scrollspy.js"></script>
    <script src="~/Content/assets/js/bootstrap-tab.js"></script>
    <script src="~/Content/assets/js/bootstrap-tooltip.js"></script>
    <script src="~/Content/assets/js/bootstrap-popover.js"></script>
    <script src="~/Content/assets/js/bootstrap-button.js"></script>
    <script src="~/Content/assets/js/bootstrap-collapse.js"></script>
    <script src="~/Content/assets/js/bootstrap-carousel.js"></script>
    <script src="~/Content/assets/js/bootstrap-typeahead.js"></script>
    <script src="~/Content/assets/js/bootstrap-datepicker.js"></script>

    <script src="~/Content/assets/js/jquery.validate.min.js"></script>
    <script src="~/Content/assets/js/jquery.validate.js"></script>


    <link href="~/Scripts/Beka.EthDate/jquery-ui-1.9.2.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="~/Scripts/Beka.EthDate/Beka.EthDate.js"> </script>
    <script type="text/javascript" src="~/Scripts/Beka.EthDate/jquery.Beka.EthCalDatePicker.js"> </script>
    <script type="text/javascript" src="~/Scripts/js/CatsUI.js"> </script>
    <script type="text/ecmascript" src="~/Scripts/ng-google-chart.js">   </script>
    <script src="Scripts/bootstrap.js"></script>--%>
</body>

</html>
