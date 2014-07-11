using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Helpers
{
    public static class VersionHelper
    {
        public static MvcHtmlString CTSVersion(this HtmlHelper htmlHelper)
        {
            
            ReleaseNoteService releaseNoteService=new ReleaseNoteService();
            ReleaseNote rnote = releaseNoteService.GetAllReleaseNote().OrderBy(o => o.ReleaseNoteID).LastOrDefault();
            if (rnote != null)
            {
                string buildNumber = string.Format("<p>Build: {2} - {0}, Released on: {1:dd-MM-yyyy} click here to read <a href='/ReleaseNotes/'>the release notes</a></p>", rnote.BuildNumber, rnote.Date, rnote.ReleaseName);
                return new MvcHtmlString(buildNumber);
            }
            return new MvcHtmlString(string.Empty);
        }
    }
}