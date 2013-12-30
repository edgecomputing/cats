<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="SSRS_Portal.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="~/Scripts/angular/css/bootstrap.min.css" rel="stylesheet"/>--%>

    <link href="Content/bootstrap.css" rel="stylesheet" />
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
    <link href="~/Content/kendo/2013.1.319/kendo.default.min.css" rel="stylesheet" />
	
        -->
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="~/Scripts/kendo/2013.1.319/jquery.min.js">
    </script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="~/Scripts/kendo/2013.1.319/kendo.all.min.js"></script>
    <script src="~/Scripts/kendo/2013.1.319/kendo.aspnetmvc.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="~/Scripts/angular/angular.js"> </script>
    <script src="~/Scripts/angular/angular-resource.js"></script>
    <%--<script src="Scripts/bootstrap.min.js"></script>--%>

    <style type="text/css">
        body {
            padding-top: 60px;
            padding-bottom: 40px;
        }
        .navbar {
            width: 100%;
        }
    </style>
</head>
<body>
    <div class="navbar" style="position: absolute;top:0px;">
        <div class="navbar-inner">
            <div class="container" style="padding-left: 70px">
                <button type="button" class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="brand" href="/">CATS</a>
                <div class="nav-collapse collapse">
                    <ul class="nav">
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Early Warning <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <a href="EarlyWarning/GiftCertificate/">Gift Certificate</a>
                                </li>
                                <li class="divider"></li>
                                <li>
                                    <a href="EarlyWarning/Plan/">Plan</a>
                                </li>

                                <li>
                                    <a href="EarlyWarning/NeedAssessment/">Needs Assessment</a>
                                </li>


                                <li>
                                    <a href="EarlyWarning/HRD/">HRD</a>
                                </li>

                                <li class="divider"></li>

                                <li class="dropdown-submenu">
                                    <a href="#" data-toggle="dropdown">Request</a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a href="/EarlyWarning/Request/">Requests</a>
                                        </li>
                                        <li>
                                            <a href="/EarlyWarning/Request/New">New Request</a>
                                        </li>
                                        <li>
                                            <a href="/EarlyWarning/Request/NewIdps">New IDPS Request</a>
                                        </li>
                                    </ul>
                                </li>

                                <li>
                                    <a href="/EarlyWarning/ReliefRequisition/">Requisitions</a>
                                </li>


                                <li class="divider"></li>

                                <li>
                                    <a href="/EarlyWarning/Ration/">Rations</a>
                                </li>

                                <li class="divider"></li>
                                <li><a href="#">Reports</a></li>

                            </ul>
                        </li>

                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">FSCD <b class="caret"></b></a>
                            <ul class="dropdown-menu">

                                <li>
                                    <a href="/EarlyWarning/RegionalPSNPPlan/">PSNP Plan</a>
                                </li>
                                <li class="divider"></li>

                                <li>
                                    <a href="/EarlyWarning/Request/">Requests</a>
                                </li>

                                <li>
                                    <a href="/EarlyWarning/ReliefRequisition/">Requisitions</a>
                                </li>
                                <li class="divider"></li>

                                <li>
                                    <a href="/EarlyWarning/Ration/">Rations</a>
                                </li>
                                <li class="divider" />

                                <li><a href="#">Reports</a></li>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Logistics<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <a href="/Logistics/Home/">Logistics Dashboard</a>
                                </li>

                                <li>
                                    <a href="/Logistics/DispatchAllocation/">Dispatch Allocation</a>
                                </li>

                                <li>
                                    <a href="/Logistics/TransportRequisition/">Transport Requisition</a>
                                </li>

                                <li class="divider" />
                                <li class="dropdown-submenu">
                                    <a href="#" data-toggle="dropdown">Reports</a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <a href="/Logistics/LogisticsStockStatus/">Free Stock Status</a>
                                        </li>


                                        <li>
                                            <a href="/Logistics/LogisticsStockStatus/ReceivedCommodity">Commodity Received Stock</a>
                                        </li>
                                        <li>
                                            <a href="/Logistics/LogisticsStockStatus/CarryOverStock">Carry Over Stock</a>
                                        </li>
                                        <li>
                                            <a href="/Logistics/LogisticsStockStatus/TransferredStock">Transferred Stock</a>
                                        </li>
                                    </ul>
                                </li>

                            </ul>

                        </li>

                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Procurement<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <a href="/Procurement/Bid/">Manage Bids</a>
                                </li>

                                <li>
                                    <a href="/Procurement/TransportBidPlan/">Bid Planning</a>
                                </li>

                                <li>
                                    <a href="/Procurement/TransportOrder/">Transport Order</a>
                                </li>
                                <li>
                                    <a href="/Procurement/PaymentRequest/">Payment Request</a>
                                </li>
                                <li class="divider" />

                                <li>
                                    <a href="/Procurement/Transporter/">Transport Suppliers</a>
                                </li>

                                <li>
                                    <a href="/Procurement/RFQ/">Request for Quotations (RFQ)</a>
                                </li>

                                <li>
                                    <a href="/Procurement/PriceQuotation/BidProposal">Price Quotation</a>
                                </li>


                                <li>
                                    <a href="/Procurement/BidWinner/">Dispatch Locations</a>
                                </li>

                                <li>
                                    <a href="/Procurement/BidWinner/BidWinningTransporters">Contract Admin</a>
                                </li>
                                <li>
                                    <a href="/Procurement/Bid/WoredasBidStatus">Woreda Bid Status</a>
                                </li>
                            </ul>
                        </li>

                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Hub <b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <a href="/Hub/StartingBalance/">Starting Balance</a>
                                </li>
                                <li>
                                    <a href="/Hub/Receive/">Receipts</a>
                                </li>
                                <li>
                                    <a href="/Hub/Dispatch/">Dispatch</a>
                                </li>
                                <li class="divider" />
                                <li>
                                    <a href="/Hub/InternalMovement/">Internal Movements</a>
                                </li>
                                <li>
                                    <a href="/Hub/LossesAndAdjustments/">Losses and Adjustments</a>
                                </li>
                                <li>
                                    <a href="/Hub/StackEvent/">Stack Events</a>
                                </li>
                                <li class="divider" />
                                <li>
                                    <a href="/Hub/BinCard/">Bin Card</a>
                                </li>
                                <li>
                                    <a href="/Hub/StockStatus/">Store Report</a>
                                </li>
                                <li>
                                    <a href="/Hub/StockStatus/FreeStock">Free Stock</a>
                                </li>
                                <li>
                                    <a href="/Hub/StockStatus/Receipts">Receipt Status</a>
                                </li>
                                <li>
                                    <a href="/Hub/StockStatus/Dispatch">Dispatch Status</a>
                                </li>
                                <li class="divider" />
                                <li>
                                    <a href="/Hub/TransportationReport/">Transportation Reports</a>
                                </li>
                                <li>
                                    <a href="/Hub/Admin/Home">Admin</a>
                                </li>

                                <li class="divider" />
                                <li>
                                    <a href="/Hub/TransportOrder/ReturnListOfApprovedListFromMainMenu">Transport Order List</a>
                                </li>
                                <li class="divider" />
                                <li>
                                    <a href="/Hub/LogisticsStockStatus/DispatchCommodity">Dispatch Stock Status</a>
                                </li>
                            </ul>
                        </li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Report <b class="caret"></b></a>
                            <%=Cats.Helpers.ReportMenuHelper.ReportMenu() %>
                        </li>
                        <li>
                            <a href="/Settings/AdminDashboard/">Settings</a>
                        </li>
                    </ul>
                    <ul class="nav pull-right">
                        <li class="divider-vertical"></li>

                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><i class="icon-envelope icon-white"><span class="badge badge-info"><%=Cats.Helpers.NotificationHelper.GetUnreadNotifications() %></span></i><b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><%=Cats.Helpers.NotificationHelper.GetActiveNotifications() %></li>
                            </ul>
                        </li>

                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><%=Cats.Helpers.UserAccountHelper.GetUserName() %><b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <a href="/Home/Preference">Preference</a>
                                </li>
                                <li>
                                    <a href="/Account/Administration">Administration</a>
                                </li>
                                <li class="divider"></li>
                                <li>
                                    <a href="/Settings/Users/ChangePassword">Change Password</a>
                                </li>
                                <li>
                                    <a href="/Logout">Logout</a>
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
        <div>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%">
            </rsweb:ReportViewer>
        </div>
    </form>

    <script src="~/Content/assets/js/bootstrap-transition.js"></script>
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
    <script src="Scripts/bootstrap.js"></script>
</body>

</html>
