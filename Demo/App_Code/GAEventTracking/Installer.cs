using System;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web.UI.Controls;

namespace UmbracoGAEventTracking.GAEventTracking
{
    public class Installer : UmbracoUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PublishContent();
        }

        private IContentType GetRootType()
        {
            LogHelper.Info<Installer>("Getting GA Event root Document Type");
            var rootType = Services.ContentTypeService.GetContentType(Keys.DocumentTypes.GAEventTrackingRootAlias);
            return rootType;
        }

        private void PublishContent()
        {
            var root = GetRoot();

            if (root == null)
            {
                // If this happens, something is very wrong with the install
                LogHelper.Info<Installer>("Could not get GA Event Root!");
            }

            LogHelper.Info<Installer>("Publishing GA Event Root");
            Services.ContentService.PublishWithChildrenWithStatus(root);

            LogHelper.Info<Installer>("Getting GA Events");
            var children = root.Children().ToArray();
            foreach (var gaEvent in children)
            {
                LogHelper.Info<Installer>("Publishing events");
                Services.ContentService.SaveAndPublishWithStatus(gaEvent);
            }
        }

        private IContent GetRoot()
        {
            IContentType rootType = GetRootType();

            if (rootType == null)
                return null;

            LogHelper.Info<Installer>("Getting GA Event root content");
            return Services.ContentService
                               .GetContentOfContentType(rootType.Id)
                               .FirstOrDefault();
        }
    }
}