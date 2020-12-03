using System.ComponentModel;
using Xamarin.Forms;
using Schedule.App.ViewModels;

namespace Schedule.App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}