using Archetype.Models;
using System;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace UmbracoGAEventTracking
{
    public class Event
    {
        public string Title { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public string Category { get; set; }
        public string CssSelector { get; set; }

        public Event(ArchetypeFieldsetModel field)
        {
            Title = field.GetValue<string>("titleDescription");
            Action = field.GetValue<string>("action");
            Label = field.GetValue<string>("label");
            Category = field.GetValue<string>("category");
            CssSelector = field.GetValue<string>("selectorElement");
        }

        public Event(IPublishedContent content)
        {
            if (content.DocumentTypeAlias != "gaEvent")
                throw new Exception(string.Format("Cannot construct event from doctype {0}", content.DocumentTypeAlias));

            Title = content.HasValue("gaEventTitle")
                ? content.GetPropertyValue<string>("gaEventTitle")
                : content.Name;
            Action = content.GetPropertyValue<string>("gaEventAction");
            Label = content.GetPropertyValue<string>("gaEventLabel");
            Category = content.GetPropertyValue<string>("gaEventCategory");
            CssSelector = content.GetPropertyValue<string>("gaEventCssSelector");
        }
    }
}