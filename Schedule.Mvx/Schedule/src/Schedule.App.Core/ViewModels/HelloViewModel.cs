using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Schedule.App.Core.ViewModels.Main;

namespace Schedule.App.Core.ViewModels
{
    public sealed class HelloViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public ICommand StartCommand { get; }

        public HelloViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            StartCommand = new MvxAsyncCommand(OnStartCommandAsync);
        }

        private Task OnStartCommandAsync()
        {
            return _navigationService.Navigate<GroupSelectionViewModel>();
        }
    }
}
