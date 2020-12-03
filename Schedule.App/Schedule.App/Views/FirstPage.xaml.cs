using Schedule.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {
        public FirstPage()
        {
            InitializeComponent();
            BindingContext = new FirstScreenViewModel();
        }
    }
}