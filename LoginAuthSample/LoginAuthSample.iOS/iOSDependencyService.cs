using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using LoginAuthSample.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSDependencyService))]
namespace LoginAuthSample.iOS
{
    public class iOSDependencyService : ISettingsService
    {
        public void OpenSettings()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
        }
    }
}