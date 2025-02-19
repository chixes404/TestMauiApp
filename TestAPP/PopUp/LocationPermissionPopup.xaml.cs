using System;
using System.Threading.Tasks;


namespace TestAPP.Popup
{
    public partial class LocationPermissionPopup : Frame
    {
        private readonly Action<double, double> _onLocationGranted;
        public LocationPermissionPopup(Action<double, double> onLocationGranted)
        {
            InitializeComponent();
            _onLocationGranted = onLocationGranted;
        }

        private async void OnAllowLocationClicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(10)
                    });
                }

                if (location != null)
                {
                    _onLocationGranted?.Invoke(location.Latitude, location.Longitude);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnDenyLocationClicked(object sender, EventArgs e)
        {
             Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void ClosePopup()
        {
            this.IsVisible = false;
        }
    }
}
