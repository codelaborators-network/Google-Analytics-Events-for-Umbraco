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

        public Event(IPublishedContent content)
        {
            if (content.DocumentTypeAlias != Keys.DocumentTypes.GoogleAnalyticsEventItem)
                throw new Exception(string.Format("Cannot construct event from doctype {0}", content.DocumentTypeAlias));

            Title = content.HasValue(Keys.PropertyAliases.Title)
                ? content.GetPropertyValue<string>(Keys.PropertyAliases.Title)
                : content.Name;
            Action = content.GetPropertyValue<string>(Keys.PropertyAliases.Action);
            Label = content.GetPropertyValue<string>(Keys.PropertyAliases.Label);
            Category = content.GetPropertyValue<string>(Keys.PropertyAliases.Category);
            CssSelector = content.GetPropertyValue<string>(Keys.PropertyAliases.CssSelector);
        }

        public Event()
        {
            throw new NotImplementedException("Parameterless default constructor only for serialization");
        }
    }
}