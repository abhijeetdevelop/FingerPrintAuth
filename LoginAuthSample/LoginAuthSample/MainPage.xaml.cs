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
            _cancel = swAutoCancel.IsToggled ? new CancellationTokenSource(TimeSpan.FromSeconds(10)) : new CancellationTokenSource();
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
                await DisplayAlert("CairnFM", "Unverified user", "Ok");
        }

        private async void OnAuthenticate(object sender, EventArgs e)
        {
            if (FingerprintAvailability.Available == 0)
                await AuthenticationAsync("Check User", "cancel");
            else
                await App.Current.MainPage.DisplayAlert("Fingerprint Unavailable","Uh-oh your phone doesnt support it", "Ok");
        }
    }
}
