using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Schedule.App.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.App.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(Animated = true, WrapInNavigationPage = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            }
        }
    }
}
