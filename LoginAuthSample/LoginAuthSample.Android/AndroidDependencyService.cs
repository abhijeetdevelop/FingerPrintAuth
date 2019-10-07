using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LoginAuthSample.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidDependencyService))]
namespace LoginAuthSample.Droid
{
    public class AndroidDependencyService : ISettingsService
    {
        public void OpenSettings()
        {
            Xamarin.Forms.Forms.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionSecuritySettings));
        }
    }
}