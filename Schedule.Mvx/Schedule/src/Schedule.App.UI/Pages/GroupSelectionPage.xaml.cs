using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Schedule.App.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule.App.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(Animated = true, WrapInNavigationPage = false)]
    public partial class GroupSelectionPage : MvxContentPage<GroupSelectionViewModel>
    {
        public GroupSelectionPage()
        {
            InitializeComponent();
        }
    }
}
