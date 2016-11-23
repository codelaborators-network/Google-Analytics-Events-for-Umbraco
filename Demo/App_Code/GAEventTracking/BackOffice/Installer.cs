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
                return;
            }

            LogHelper.Info<Installer>("Publishing GA Event Root with children");
            Services.ContentService.PublishWithChildrenWithStatus(root);
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
    }
}