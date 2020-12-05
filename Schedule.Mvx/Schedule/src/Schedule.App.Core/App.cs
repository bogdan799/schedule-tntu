using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Schedule.App.Core.Services;
using Schedule.App.Core.ViewModels;
using Schedule.App.Core.ViewModels.Home;

namespace Schedule.App.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            ISettingsService settingsService = MvxIoCProvider.Instance.Resolve<ISettingsService>();


            RegisterAppStart<HelloViewModel>();

            //if (settingsService != null && settingsService.SelectedGroupId != 0)
            //{
            //    RegisterAppStart<HomeViewModel>();
            //}
            //else
            //{
            //    RegisterAppStart<HelloViewModel>();
            //}
        }
    }
}
