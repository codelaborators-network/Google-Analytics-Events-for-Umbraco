using Archetype.Models;
using Umbraco.Core.Models;

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
    }
}