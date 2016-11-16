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

        public List<Event> GetEvents()
        {
            var eventRootNode = _content.AncestorsOrSelf(1)
                                        .FirstOrDefault(n => n.DocumentTypeAlias == Keys.DocumentTypes.GAEventTrackingRootAlias);

            if (eventRootNode != null)
            {
                return eventRootNode.Children(c => c.DocumentTypeAlias == Keys.DocumentTypes.GoogleAnalyticsEventItem)
                                    .Select(c => new Event(c))
                                    .ToList();
            }
            return null;
        }
    }
}