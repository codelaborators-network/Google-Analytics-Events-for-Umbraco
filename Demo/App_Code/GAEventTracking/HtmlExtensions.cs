using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace UmbracoGAEventTracking
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Includes google analytics.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="analyticsKey"></param>
        /// <returns></returns>
        public static HtmlString IncludeGoogleAnalytics(this HtmlHelper helper)
        {
            return helper.Partial("_GoogleAnalytics");
        }

        /// <summary>
        /// An HTML Helper that renders Google GA Tracking information
        /// </summary>
        /// <param name="helper"></param>
        /// <returns>MvcHtmlString</returns>
        public static HtmlString IncludeGaEventTracking(this HtmlHelper helper)
        {
            return helper.Partial("_GAEventTracking");
        }
    }
}