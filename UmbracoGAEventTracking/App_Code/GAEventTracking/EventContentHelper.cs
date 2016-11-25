using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoGAEventTracking
{
    public class EventContentHelper
    {
        private readonly IPublishedContent _content;

        public EventContentHelper(IPublishedContent content)
        {
            _content = content;
        }

        public EventContentHelper(int nodeId)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            _content = umbracoHelper.TypedContent(nodeId);
        }

        public bool AreGaEventsDisabled()
        {
            var root = GetGAEventRoot();
            if (root != null)
            {
                return root.GetPropertyValue<bool>(Keys.PropertyAliases.DisableGAEvents);
            }
            return false;
        }

        public List<Event> GetEvents()
        {
            int cachingTimeInMinutes = GetCachingTimeInMinutes();
            return cachingTimeInMinutes > 0
                ? Caching.GetObjectFromCache<List<Event>>("events", GetCachingTimeInMinutes(), GetEventsFromDatabase)
                : GetEventsFromDatabase();
        }

        public List<Event> GetEventsFromDatabase()
        {
            var eventRootNode = GetGAEventRoot();
            if (eventRootNode != null)
            {
                return eventRootNode.Children(c => c.DocumentTypeAlias == Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem || c.DocumentTypeAlias == Keys.DocumentTypes.GoogleAnalyticsStandardEventItem)
                                    .Select(c => new Event(c))
                                    .ToList();
            }
            return null;
        }

        private IPublishedContent GetGAEventRoot()
        {
            int cachingTimeInMinutes = GetCachingTimeInMinutes();
            return cachingTimeInMinutes > 0
                ? Caching.GetObjectFromCache<IPublishedContent>("event_root", GetCachingTimeInMinutes(), GetGAEventRootFromDatabase)
                : GetGAEventRootFromDatabase();
        }

        private IPublishedContent GetGAEventRootFromDatabase()
        {
            if (_content == null)
                return null;

            return _content.AncestorOrSelf(1)
                           .Siblings()
                           .FirstOrDefault(n => n.DocumentTypeAlias == Keys.DocumentTypes.GAEventTrackingRootAlias);
        }

        private int GetCachingTimeInMinutes()
        {
            int cacheTime = 0;
            if (int.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["GAEventTracking_CachingTimeInMinutes"], out cacheTime))
            {
                //cacheTime has been set
            }
            return cacheTime;
        }
    }
}