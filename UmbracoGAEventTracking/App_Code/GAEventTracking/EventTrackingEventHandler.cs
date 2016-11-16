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
                if (content.ContentType.Alias == Keys.DocumentTypes.GAEventItemAlias)
                {
                    if (content.HasProperty(Keys.DocumentTypes.PropertyAliases.Title))
                    {
                        string title = content.GetValue<string>(Keys.DocumentTypes.PropertyAliases.Title);
                        MatchCollection labelPlaceholders = Regex.Matches(title, "{[A-Za-z0-9]+}+");
                        foreach (string match in labelPlaceholders)
                        {
                            if (!Keys.LabelPlaceholders.ContainsValue(match))
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