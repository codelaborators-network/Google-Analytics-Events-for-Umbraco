using System.Collections.Generic;

namespace UmbracoGAEventTracking.GAEventTracking
{
    public static class Keys
    {
        public static Dictionary<string, string> LabelPlaceholders = new Dictionary<string, string>()
            {
                { "PageUrl", "{PAGE_URL}"},
                { "PageRelativeUrl", "{PAGE_RELATIVE_URL}"},
                { "TagName", "{TAG_NAME}"},
                { "ID", "{ID}"},
                { "Class", "{CLASS}"},
                { "Value", "{VALUE}"},
                { "Src", "{SRC}"},
                { "LinkUrl", "{LINK_URL}"},
                { "LinkRelativeUrl", "{LINK_RELATIVE_URL}"},
                { "Alt", "{ALT}"},
                { "Title", "{TITLE}"}
            };

        public static class DocumentTypes
        {
            // TODO - double check these
            public const string GAEventTrackingRootAlias = "gaEventTrackingRoot";
            public const string GAEventItemAlias = "gaEvent";

            public static class PropertyAliases
            {
                // TODO - add all the other aliases
                public const string Title = "gaEventTitle";
            }
        }
    }
}