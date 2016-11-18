using System.Collections.Generic;

namespace UmbracoGAEventTracking
{
    public static class Keys
    {
        public const string LabelPlaceholderRegex = "/{[A-Za-z0-9_-]+}+/g";

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
                { "Title", "{TITLE}"},
                { "InnerHTML", "{INNER_HTML}"}
            };

        public static class DocumentTypes
        {
            public const string GAEventTrackingRootAlias = "googleAnalyticsEventRoot";
            public const string GoogleAnalyticsAdvancedEventItem = "googleAnalyticsEventItem";
            public const string GoogleAnalyticsStandardEventItem = "googleAnalyticsStandardEvent";
        }

        public static class PropertyAliases
        {
            public const string DisableGAEvents = "googleAnalyticsEvent_DisableEvents";

            public const string Title = "googleAnalyticsEvent_Title";
            public const string CssSelector = "googleAnalyticsEvent_CssSelector";
            public const string Category = "googleAnalyticsEvent_Category";
            public const string Action = "googleAnalyticsEvent_Action";
            public const string Label = "googleAnalyticsEvent_Label";
        }
    }
}