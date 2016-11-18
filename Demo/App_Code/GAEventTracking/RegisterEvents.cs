using System.Text.RegularExpressions;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;

namespace UmbracoGAEventTracking.GAEventTracking
{
    public class RegisterEvents : ApplicationEventHandler
    {
        public RegisterEvents()
        {
            ContentService.Publishing += ContentService_Publishing;
        }

        private void ContentService_Publishing(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        {
            foreach (var content in e.PublishedEntities)
            {
                if (content.ContentType.Alias == Keys.DocumentTypes.GoogleAnalyticsAdvancedEventItem)
                {
                    if (content.HasProperty(Keys.PropertyAliases.Label))
                    {
                        string labelTemplate = content.GetValue<string>(Keys.PropertyAliases.Label);
                        if (!string.IsNullOrEmpty(labelTemplate))
                        {
                            var matches = Regex.Matches(labelTemplate, Keys.LabelPlaceholderRegex);
                            foreach (Match match in matches)
                            {
                                if (!Keys.LabelPlaceholders.ContainsValue(match.Value))
                                {
                                    e.Cancel = true;
                                    e.Messages.Add(new EventMessage("Cannot publish GA Event", "One of the Placeholders entered does not match our standard list of placeholders.", EventMessageType.Warning));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}