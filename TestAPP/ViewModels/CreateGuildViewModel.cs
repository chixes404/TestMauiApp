using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestAPP.Services;

namespace TestAPP.ViewModels
{
    public partial class CreateGuildViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        public ObservableCollection<EquipmentTag> EquipmentTags { get; set; }
        public ObservableCollection<string> LocationsList { get; set; }

        private string selectedLocation
        {
            get => location;
            set { SetProperty(ref location, value); }
        }

        public CreateGuildViewModel()
        {
            _apiService = new ApiService();

            EquipmentTags = new ObservableCollection<EquipmentTag>
            {
                new EquipmentTag { Name = "Dumbbells" },
                new EquipmentTag { Name = "Machines" },
                new EquipmentTag { Name = "Treadmill" },
                new EquipmentTag { Name = "Calisthenics" },
                new EquipmentTag { Name = "Other" }
            };

            LocationsList = new ObservableCollection<string>
            {
                "New York", "Los Angeles", "Chicago", "Houston", "San Francisco"
            };

            selectedLocation = LocationsList.FirstOrDefault();
        }

        [ObservableProperty] private string name;
        [ObservableProperty] private string location;
        [ObservableProperty] private string description;
        [ObservableProperty] private bool isPrivate;
        [ObservableProperty] private string password;
        [ObservableProperty] private string selectedEquipment;
        [ObservableProperty] private int members = 1;
        [ObservableProperty] private int rank = 1;

        public ICommand CreateGuildCommand => new AsyncRelayCommand(CreateGuildAsync);
        public ICommand ToggleEquipmentCommand => new RelayCommand<EquipmentTag>(ToggleEquipment);
        public ICommand GoBackCommand => new RelayCommand(GoBack);

        private void ToggleEquipment(EquipmentTag tag)
        {
            if (tag != null)
            {
                tag.IsSelected = !tag.IsSelected;
                SelectedEquipment = string.Join(", ", EquipmentTags.Where(e => e.IsSelected).Select(e => e.Name));
            }
        }

        private async Task CreateGuildAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Location) || string.IsNullOrWhiteSpace(Description))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            var newGuild = new
            {
                name = Name,
                location = Location,
                description = Description,
                equipment = SelectedEquipment,
                isPrivate = IsPrivate,
                password = IsPrivate ? Password : null,
                members = Members,
                rank = Rank
            };

            bool success = await _apiService.CreateGuildAsync(newGuild);

            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Guild created successfully!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to create guild.", "OK");
            }
        }

        public class EquipmentTag : ObservableObject
        {
            private bool _isSelected;
            public string Name { get; set; }

            public bool IsSelected
            {
                get => _isSelected;
                set => SetProperty(ref _isSelected, value);
            }
        }

        private async void GoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
