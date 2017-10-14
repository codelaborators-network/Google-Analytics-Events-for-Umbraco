using System;
using System.Collections.Generic;
using System.Linq;
using umbraco;
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
        private const string PropertyValueConvertersSetting = "/settings/content/EnablePropertyValueConverters";

        public Event(IPublishedContent content)
        {
            if (content.DocumentTypeAlias != Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem
                && content.DocumentTypeAlias != Keys.DocumentTypes.GoogleAnalyticsStandardEventItem)
                throw new Exception(string.Format("Cannot construct event from doctype {0}", content.DocumentTypeAlias));

            Title = content.HasValue(Keys.PropertyAliases.Title)
                ? content.GetPropertyValue<string>(Keys.PropertyAliases.Title)
                : content.Name;
            Action = content.GetPropertyValue<string>(Keys.PropertyAliases.Action);
            Label = GetLabelValue(content.GetPropertyValue(Keys.PropertyAliases.Label), content.DocumentTypeAlias);
            Category = content.GetPropertyValue<string>(Keys.PropertyAliases.Category);
            CssSelector = content.GetPropertyValue<string>(Keys.PropertyAliases.CssSelector);
        }

        private string GetLabelValue(object propertyValue, string docTypeAlias)
        {
            bool _propertyValueConvertersEnabled = GetPropertyValueConvertersEnabled();

            if (docTypeAlias == Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem)
            {
                return (string)propertyValue;
            }
            else
            {
                return _propertyValueConvertersEnabled ? TranslateCheckboxValuesToPlaceholders((IEnumerable<string>)propertyValue) : TranslateCheckboxValuesToPlaceholders((string)propertyValue);
            }
        }

        private bool GetPropertyValueConvertersEnabled()
        {
            bool isEnabled = false;
            var xmlPVCSetting = UmbracoSettings._umbracoSettings.SelectNodes(PropertyValueConvertersSetting);

            if (xmlPVCSetting != null && xmlPVCSetting.Count > 0)
            {
                Boolean.TryParse(xmlPVCSetting.Item(0).InnerText, out isEnabled);
            }

            return isEnabled;
        }
        
        /// <summary>
        /// Transforms Checkboxes to Placeholder string when PropertyValueConverts are disabled
        /// </summary>
        /// <param name="propertyValue"></param>
        /// <returns>formatted placeholder string</returns>
        private string TranslateCheckboxValuesToPlaceholders(string propertyValue)
        {
            return string.Join(" - ", propertyValue.Replace(' ', '_').ToUpper().Split(',').Select(x => x.Replace(" ", string.Empty) + ": " + "{" + x.Replace(" ", "_").ToUpper() + "}"));
        }

        /// <summary>
        /// Transforms Checkboxes to Placeholder string when PropertyValueConverts are enabled
        /// </summary>
        /// <param name="propertyValues"></param>
        /// <returns>formatted placeholder string</returns>
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