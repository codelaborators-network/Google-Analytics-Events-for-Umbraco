using Archetype.Models;
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
            var homePage = _content.AncestorOrSelf(1);
            if (homePage.HasProperty("eventTrackingSettings"))
            {
                ArchetypeModel eventTracking = homePage.GetPropertyValue<ArchetypeModel>("eventTrackingSettings");
                return eventTracking.Select(c => new Event(c)).ToList();
            }
            return null;
        }
    }
}