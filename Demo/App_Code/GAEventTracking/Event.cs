using System;
using System.Linq;
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
            if (content.DocumentTypeAlias != Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem
                && content.DocumentTypeAlias != Keys.DocumentTypes.GoogleAnalyticsStandardEventItem)
                throw new Exception(string.Format("Cannot construct event from doctype {0}", content.DocumentTypeAlias));

            Title = content.HasValue(Keys.PropertyAliases.Title)
                ? content.GetPropertyValue<string>(Keys.PropertyAliases.Title)
                : content.Name;
            Action = content.GetPropertyValue<string>(Keys.PropertyAliases.Action);
            Label = GetLabelValue(content.GetPropertyValue<string>(Keys.PropertyAliases.Label), content.DocumentTypeAlias);
            Category = content.GetPropertyValue<string>(Keys.PropertyAliases.Category);
            CssSelector = content.GetPropertyValue<string>(Keys.PropertyAliases.CssSelector);
        }

        private string GetLabelValue(string propertyValue, string docTypeAlias)
        {
            if(docTypeAlias == Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem)
            {
                return propertyValue;
            }
            else
            {
                return TranslateCheckboxValuesToPlaceholders(propertyValue);
            }
        }

        private string TranslateCheckboxValuesToPlaceholders(string propertyValue)
        {
            return string.Join(" - ", propertyValue.Replace(' ', '_').ToUpper().Split(',').Select(x => "{" + x + "}"));
        }

        public Event()
        {
            throw new NotImplementedException("Parameterless default constructor only for serialization");
        }
    }
}