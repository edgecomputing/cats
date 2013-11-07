<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().ComboBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
    .ClientEvents(ev => ev//.OnDataBinding("onComBinding")
                           // .OnChange("OnCommodityChange")
                            //.OnClose("OnComClose")
                           // .OnDataBound("OnComDataBound")
                            .OnLoad("OnSubCommodityGridLoad")
                            .OnOpen("OnComOpen")
                            //.OnError("OnComError")
                            )
    .Filterable(filtering => filtering.FilterMode(AutoCompleteFilterMode.Contains)).OpenOnFocus(true)
    
%>