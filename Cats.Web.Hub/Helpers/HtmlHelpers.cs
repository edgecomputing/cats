using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cats.Models.Hubs;

namespace Cats.Web.Hub.Helpers
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// Creates a link that will open a jQuery UI dialog form.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">The inner text of the anchor element</param>
        /// <param name="dialogContentUrl">The url that will return the content to be loaded into the dialog window</param>
        /// <param name="dialogId">The id of the div element used for the dialog window</param>
        /// <param name="dialogTitle">The title to be displayed in the dialog window</param>
        /// <param name="updateTargetId">The id of the div that should be updated after the form submission</param>
        /// <param name="updateUrl">The url that will return the content to be loaded into the traget div</param>
        /// <returns></returns>
        public static MvcHtmlString DialogFormLink(this HtmlHelper htmlHelper, string linkText, string dialogContentUrl,
             string dialogTitle, string updateTargetId, string updateUrl)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.SetInnerText(linkText);
            builder.Attributes.Add("href",  dialogContentUrl);
            //builder.Attributes.Add("href", "javascript:OpenDialog('" + dialogTitle + "','" + dialogContentUrl + "')");
            builder.Attributes.Add("data-dialog-title", dialogTitle);
            builder.Attributes.Add("data-update-target-id", updateTargetId);
            builder.Attributes.Add("data-update-url", updateUrl);

            // Add a css class named dialogLink that will be
            // used to identify the anchor tag and to wire up
            // the jQuery functions
            builder.AddCssClass("dialogLink"); 
           // builder.Attributes.Add("id",linkId);

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString DialogFormLink(this HtmlHelper htmlHelper, string linkText, string dialogContentUrl,
     string dialogTitle, string updateTargetId, string updateUrl, string linkId)
        {
            TagBuilder builder = new TagBuilder("a");
            builder.SetInnerText(linkText);
            builder.Attributes.Add("href", dialogContentUrl);
            //builder.Attributes.Add("href", "javascript:OpenDialog('" + dialogTitle + "','" + dialogContentUrl + "')");
            builder.Attributes.Add("data-dialog-title", dialogTitle);
            builder.Attributes.Add("data-update-target-id", updateTargetId);
            builder.Attributes.Add("data-update-url", updateUrl);

            // Add a css class named dialogLink that will be
            // used to identify the anchor tag and to wire up
            // the jQuery functions
            builder.AddCssClass("dialogLink");
            builder.Attributes.Add("id", linkId);

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString HubSelectionLink(this HtmlHelper htmlHelper, string linkText, string dialogContentUrl,
             string dialogTitle)
        {
            
            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = @"<img src='/Images/change.png' />";
            builder.Attributes.Add("title", linkText);
            builder.Attributes.Add("href", dialogContentUrl);
            //builder.Attributes.Add("href", "javascript:OpenDialog('" + dialogTitle + "','" + dialogContentUrl + "')");
            builder.Attributes.Add("data-dialog-title", dialogTitle);

            // Add a css class named dialogLink that will be
            // used to identify the anchor tag and to wire up
            // the jQuery functions
            builder.AddCssClass("hubChangeLink");

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString LedgerLink(this HtmlHelper htmlHelper, string linkText, string dialogContenturl, string dialogTitle, string updateUrl)
        {

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = @"<img src='/Images/change.png' />";
            builder.Attributes.Add("title",linkText);
            builder.Attributes.Add("href", dialogContenturl);
            builder.Attributes.Add("data-dialog-title",dialogTitle);
            builder.Attributes.Add("data-update-url", updateUrl);
            builder.AddCssClass("hubChangeLink");
            return new MvcHtmlString(builder.ToString());
        }

        /// <summary>
        /// Audits the trial.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="id">The id.</param>
        /// <param name="foreignTable">The foreign table.</param>
        /// <param name="foreignFeildName">Name of the foreign feild.</param>
        /// <param name="foreignFeildKey">The foreign feild key.</param>
        /// <returns></returns>
        public static MvcHtmlString AuditTrial(this HtmlHelper htmlHelper, string tableName, string fieldName, object id, string foreignTable, string foreignFeildName, string foreignFeildKey)
        {
            if (HttpContext.Current.User.IsInRole(Role.RoleEnum.Admin.ToString()))
            {
                bool hasChange = Audit.HasUpdated(id, tableName, fieldName);
                if (hasChange)
                {
                    TagBuilder builder = new TagBuilder("a");
                    string url = "";
                    if (foreignTable != null && foreignFeildName != null)
                    {
                        url =
                            string.Format(
                                "/Audit/Audits?id={0}&tableName={1}&fieldName={2}&foreignTable={3}&foreignFeildName={4}&foreignFeildKey={5}",
                                id, tableName, fieldName, foreignTable, foreignFeildName, foreignFeildKey);
                    }
                    else
                    {
                        url = string.Format("/Audit/Audits?id={0}&tableName={1}&fieldName={2}", id, tableName, fieldName);
                    }
                    builder.InnerHtml = @"<img src='/Images/change.png' />";
                    builder.Attributes.Add("href", url);
                    builder.AddCssClass("auditLink");
                    return new MvcHtmlString(builder.ToString());
                }
            }
            return null;
        }




        /// <summary>
        /// Audits the trial.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static MvcHtmlString AuditTrial(this HtmlHelper htmlHelper, string tableName, string fieldName, object id)
        {
            if (HttpContext.Current.User.IsInRole(Role.RoleEnum.Admin.ToString()))
            {
                bool hasChange = Audit.HasUpdated(id, tableName, fieldName);
                if (hasChange)
                {
                    TagBuilder builder = new TagBuilder("a");
                    string url = "";
                    url = string.Format("/Audit/Audits?id={0}&tableName={1}&fieldName={2}", id, tableName, fieldName);
                    builder.InnerHtml = @"<img src='/Images/change.png' />";
                    builder.Attributes.Add("href", url);
                    builder.AddCssClass("auditLink");
                    return new MvcHtmlString(builder.ToString());
                }
            }
            return null;
        }


        /// <summary>
        /// Requireds the specified helper.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="text">The text.</param>
        /// <param name="forceRequired">if set to <c>true</c> [force required].</param>
        /// <returns></returns>
        public static MvcHtmlString Required<T, U>(this HtmlHelper<T> helper,
    Expression<Func<T, U>> expression, string text, bool forceRequired)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = !string.IsNullOrEmpty(text)
                ? text : metaData.DisplayName
                ?? metaData.PropertyName
                ?? htmlFieldName.Split('.').Last();

            var label = new TagBuilder("label");

            label.Attributes.Add("for"
                , helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            label.SetInnerText(labelText);
            if (metaData.IsRequired || forceRequired)
            {
                label.InnerHtml += "<span class='style'> *</span>";;
            }
            return MvcHtmlString.Create(label.ToString());
        }


        /// <summary>
        /// Gets the current theme.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string GetCurrentTheme(this HtmlHelper html)
        {
            return html.ViewContext.HttpContext.Request.QueryString["theme"] ?? "metro";
        }
    }
}
