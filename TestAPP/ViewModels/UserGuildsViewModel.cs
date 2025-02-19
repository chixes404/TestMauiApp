using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPP.Helper;
using TestAPP.Models;
using TestAPP.Services;

namespace TestAPP.ViewModels
{
    public partial class UserGuildsViewModel : BaseModel
    {

        private readonly ApiService _apiService;


        public ObservableCollection<Guilds> YourGuilds{ get; set; } = new();
        public ObservableCollection<Guilds> RecommendedGuilds { get; set; } = new();

        public UserGuildsViewModel(ApiService apiService)
        {
            _apiService = apiService;
            LoadGuildsAsync();
        }
        public UserGuildsViewModel()
        {
        
        }


        public void ToggleGuildExpansion(Guilds guild)
        {
            if (guild != null)
            {
                guild.IsExpanded = !guild.IsExpanded;
            }
        }


        private async void LoadGuildsAsync()
        {
            try
            {
                var response = await _apiService.GetGuildsAsync();

                if (response != null)
                {
                    Debug.WriteLine($"API Response: YourGuilds Count = {response.YourGuild?.Count}");


                    YourGuilds.Clear();
                    if (response.YourGuild != null)
                    {

                        YourGuilds = new ObservableCollection<Guilds>(response.YourGuild);
                        OnPropertyChanged(nameof(YourGuilds));  // Manually notify UI

                    }

                    RecommendedGuilds.Clear();
                    if (response.RecommendedGuild != null)
                    {

                        RecommendedGuilds = new ObservableCollection<Guilds>(response.RecommendedGuild);
                        OnPropertyChanged(nameof(RecommendedGuilds));  // Manually notify UI

                    }

                }
                else
                {
                    Debug.WriteLine("API Response is NULL");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading guilds: {ex.Message}");
            }


        }





    }
}
