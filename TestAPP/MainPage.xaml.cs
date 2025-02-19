using TestAPP.Services;
using TestAPP.ViewModels;
using TestAPP;

namespace TestAPP
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage(UserGuildsViewModel viewModel, ApiService apiService)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _apiService = apiService;
            AddGestureRecognizers();
        }

        public MainPage(UserGuildsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            AddGestureRecognizers();
        }

        private void AddGestureRecognizers()
        {
            var createGuildFrame = this.FindByName<Frame>("CreateGuildFrame");
            if (createGuildFrame != null)
            {
                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += OnCreateGuildTapped;
                createGuildFrame.GestureRecognizers.Add(tapGesture);
            }
        }

        private async void OnGoToListTapped(object sender, EventArgs e)
        {
            var guildsViewModel = new GuildsListViewModel(new ApiService());
            await Navigation.PushAsync(new GuildsPage(guildsViewModel));
        }

        private async void OnCreateGuildTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateGuildPage());
        }
    }
}
