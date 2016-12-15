using System;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Web.UI.Controls;
using UmbracoGAEventTracking;

namespace GAEventTracking.BackOffice
{
    public class Installer : UmbracoUserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            PublishContent();
        }

        private void PublishContent()
        {
            var root = GetRoot();

            if (root == null)
            {
                // If this happens, something is very wrong with the install
                LogHelper.Info<Installer>("Could not get GA Event Root!");
                return;
            }

            // Publish Root first
            LogHelper.Info<Installer>("Publishing GA Event Root");
            var rootPublishAttempt = Services.ContentService.SaveAndPublishWithStatus(root);
            if (!rootPublishAttempt.Success)
            {
                LogHelper.Info<Installer>(string.Format("Could not publish GA Event Root. Exception message: {0}", rootPublishAttempt.Exception.Message));
                return;
            }

            // Then, children because SaveAndPublishWithChildren doesnt seem to work
            LogHelper.Info<Installer>("Publishing GA Events");
            foreach (var child in root.Children())
            {
                var childrenPublishAttempt = Services.ContentService.SaveAndPublishWithStatus(child);
                if (!childrenPublishAttempt.Success)
                {
                    LogHelper.Info<Installer>(string.Format("Could not publish GA Event. Exception message: {0}", childrenPublishAttempt.Exception.Message));
                }
            }
        }

        private IContent GetRoot()
        {
            IContentType rootType = GetRootType();

            if (rootType == null)
            {
                LogHelper.Info<Installer>("Could not get Root type!");
                return null;
            }

            LogHelper.Info<Installer>("Getting GA Event root content");
            return Services.ContentService
                           .GetContentOfContentType(rootType.Id)
                           .FirstOrDefault();
        }

        private IContentType GetRootType()
        {
            LogHelper.Info<Installer>("Getting GA Event root Document Type");
            var rootType = Services.ContentTypeService.GetContentType(Keys.DocumentTypes.GAEventTrackingRootAlias);
            return rootType;
        }
    }
}