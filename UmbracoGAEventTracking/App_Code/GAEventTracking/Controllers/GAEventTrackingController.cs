using System.Collections.Generic;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace UmbracoGAEventTracking.GAEventTracking.Controllers
{
    [PluginController("GoogleAnalytics")]
    public class GAEventTrackingController : UmbracoApiController
    {
        public List<Event> GetEvents(int eventRootId)
        {
            var helper = new EventContentHelper(eventRootId);
            return helper.GetEvents();
        }
    }
}