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
            if (_content == null)
                return null;

            return _content.AncestorOrSelf(1)
                           .Siblings()
                           .FirstOrDefault(n => n.DocumentTypeAlias == Keys.DocumentTypes.GAEventTrackingRootAlias);
        }
    }
}