using System;
using System.Collections.Generic;
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
            Label = content.DocumentTypeAlias == Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem
                ? content.GetPropertyValue<string>(Keys.PropertyAliases.Label)
                : TranslateCheckboxValuesToPlaceholders(
                    content.GetPropertyValue<IEnumerable<string>>(Keys.PropertyAliases.Label));
            Category = content.GetPropertyValue<string>(Keys.PropertyAliases.Category);
            CssSelector = content.GetPropertyValue<string>(Keys.PropertyAliases.CssSelector);
        }


        private string TranslateCheckboxValuesToPlaceholders(IEnumerable<string> propertyValues)
        {
            var enumerable = propertyValues as string[] ?? propertyValues.ToArray();

            return string.Join(" - ", enumerable.Select(x => x.Replace(" ", string.Empty) + ": " + "{" + x.Replace(" ", "_").ToUpper() + "}"));
        }

        public Event()
        {
            throw new NotImplementedException("Parameterless default constructor only for serialization");
        }
    }
}