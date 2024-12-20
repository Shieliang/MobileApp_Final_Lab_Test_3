using MauiApp3.ViewModel;
namespace MauiApp3
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
            // Set the BindingContext to the ViewModel
            BindingContext = new MainPageViewModel();
        }

    }

}
