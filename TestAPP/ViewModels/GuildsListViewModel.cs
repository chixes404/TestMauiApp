using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using TestAPP.Helper;
using TestAPP.Models;
using TestAPP.Services;

namespace TestAPP.ViewModels
{
    public class GuildsListViewModel : BaseModel
    {
        private readonly ApiService _apiService;
        public ObservableCollection<Guilds> Guilds { get; set; } = new();

        public Command LoadGuildsCommand { get; }
        public Command ShowLocationPopupCommand { get; }

        public GuildsListViewModel(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));

            LoadGuildsCommand = new Command(async () => await LoadGuildsAsync());
            ShowLocationPopupCommand = new Command(() => ShowLocationPopup());

            LoadGuildsAsync();
        }


        public GuildsListViewModel()
        {

        }
        private async Task LoadGuildsAsync()
        {
            try
            {
                var response = await _apiService.GetGuildsListAsync();

                if (response?.allGuilds == null)
                {
                    Debug.WriteLine("API Response or Guilds list is NULL");
                    return;
                }

                Guilds.Clear();
                foreach (var guild in response.allGuilds)
                {
                    Guilds.Add(guild);
                }

                OnPropertyChanged(nameof(Guilds));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading guilds: {ex.Message}");
            }
        }

        private void ShowLocationPopup()
        {
            var popup = new TestAPP.Popup.LocationPermissionPopup(async (latitude, longitude) =>
            {
                await FilterGuildsByLocationAsync(latitude, longitude);
            });

            Application.Current.MainPage.Navigation.PushModalAsync(new ContentPage { Content = popup });
        }

        private async Task FilterGuildsByLocationAsync(double latitude, double longitude)
        {
            try
            {

                string userLocation = $"{latitude},{longitude}";
                var response = await _apiService.GetGuildsByLocationAsync(userLocation);

                if (response?.allGuilds == null)
                {
                    Debug.WriteLine("No guilds found near the user's location.");
                    return;
                }

                Guilds.Clear();
                foreach (var guild in response.allGuilds)
                {
                    Guilds.Add(guild);
                }

                OnPropertyChanged(nameof(Guilds));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering guilds by location: {ex.Message}");
            }
        }
    }
}
