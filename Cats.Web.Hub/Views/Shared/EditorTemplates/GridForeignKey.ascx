<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%= Html.Telerik().ComboBox()
    .Name(ViewData.TemplateInfo.GetFullHtmlFieldName(""))
    .BindTo((SelectList)ViewData[ViewData.TemplateInfo.GetFullHtmlFieldName("") + "_Data"])
    .Filterable(filtering =>
            {
                filtering.FilterMode(AutoCompleteFilterMode.Contains);
            })
    
%>