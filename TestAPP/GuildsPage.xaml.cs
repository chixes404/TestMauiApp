using TestAPP.ViewModels;

namespace TestAPP
{
    public partial class GuildsPage : ContentPage
    {

        public GuildsPage(GuildsListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnFilterButtonClicked(object sender, EventArgs e)
        {
            // Show the popup
            LocationPopup.IsVisible = true;
        }

        private async void OnAllowLocationClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the current location
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
                    // Send location to the API or process as needed
                    Console.WriteLine($"Location: {location.Latitude}, {location.Longitude}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting location: {ex.Message}");
            }

            // Hide the popup
            LocationPopup.IsVisible = false;
        }

        private void OnDenyLocationClicked(object sender, EventArgs e)
        {
            // Hide the popup
            LocationPopup.IsVisible = false;
        }
    }

}
