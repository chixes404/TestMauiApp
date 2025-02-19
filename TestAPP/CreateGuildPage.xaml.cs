using TestAPP.ViewModels;
using static TestAPP.ViewModels.CreateGuildViewModel;

namespace TestAPP
{
    public partial class CreateGuildPage : ContentPage
    {
        public CreateGuildPage()
        {
            InitializeComponent();
            BindingContext = new CreateGuildViewModel();
        }


  
    }
}
