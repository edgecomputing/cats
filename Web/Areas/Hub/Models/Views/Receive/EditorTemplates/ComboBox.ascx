<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().ComboBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
    .ClientEvents(ev => ev.OnDataBinding("onComBinding")
                            .OnChange("OnComChange")
                            .OnClose("OnComClose")
                            .OnDataBound("OnComDataBound")
                            .OnLoad("OnComLoad")
                            .OnOpen("OnComOpen")
                            .OnError("OnComError"))
%>