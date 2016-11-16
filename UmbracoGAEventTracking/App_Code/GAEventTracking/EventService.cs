using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoGAEventTracking
{
    public class EventHelper
    {
        private readonly IPublishedContent _content;

        public EventHelper(IPublishedContent content)
        {
            _content = content;
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
            var eventRootNode = GetGAEventRoot();
            if (eventRootNode != null)
            {
                return eventRootNode.Children(c => c.DocumentTypeAlias == Keys.DocumentTypes.GoogleAnalyticsEventItem)
                                    .Select(c => new Event(c))
                                    .ToList();
            }
            return null;
        }

        private IPublishedContent GetGAEventRoot()
        {
            return _content.AncestorOrSelf(1)
                                        .Siblings()
                                        .FirstOrDefault(n => n.DocumentTypeAlias == Keys.DocumentTypes.GAEventTrackingRootAlias);
        }
    }
}