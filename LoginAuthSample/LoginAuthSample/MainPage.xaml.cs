using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoginAuthSample
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource _cancel;

        public MainPage()
        {
            InitializeComponent();
        }

        private async Task AuthenticationAsync(string reason, string cancel = null, string fallback = null, string tooFast = null)
        {
            _cancel = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var dialogConfig = new AuthenticationRequestConfiguration(reason)
            {
                CancelTitle = cancel,
                FallbackTitle = fallback,
                AllowAlternativeAuthentication = true,
                UseDialog = true,
            };
            var result = await Plugin.Fingerprint.CrossFingerprint.Current.AuthenticateAsync(dialogConfig, _cancel.Token);
            await SetResultAsync(result);
        }
        private async Task SetResultAsync(FingerprintAuthenticationResult result)
        {
            if (result.Authenticated)
                await DisplayAlert("CairnFM", "User is verified", "Ok");
            else
                await DisplayAlert("CairnFM", "Unverified user", "Ok");
        }


        private async void OnAuthenticate(object sender, EventArgs e)
        {
            var availableResult = await CrossFingerprint.Current.GetAvailabilityAsync(false);

            bool result = CrossFingerprint.Current.IsAvailableAsync().Result;
            if (result)
            {
                await AuthenticationAsync("Check User", "cancel");
                return;
            }

            if ((availableResult.HasFlag(FingerprintAvailability.NoApi) || availableResult.HasFlag(FingerprintAvailability.Unknown) )&& (!availableResult.HasFlag(FingerprintAvailability.Available)))
            {
                await App.Current.MainPage.DisplayAlert("Fingerprint Unavailable", "Uh-oh your phone doesnt support it", "Ok");
                return;
            }

            if (availableResult.HasFlag(FingerprintAvailability.NoFingerprint) || availableResult.HasFlag(FingerprintAvailability.NoImplementation) && Device.RuntimePlatform != Device.iOS)
            {
                DependencyService.Get<ISettingsService>().OpenSettings();
                return;
            }            
        }
    }
}

