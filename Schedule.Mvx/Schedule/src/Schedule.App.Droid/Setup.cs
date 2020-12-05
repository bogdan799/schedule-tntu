using Android.App;
using MvvmCross.Forms.Platforms.Android.Core;
using Application = Xamarin.Forms.Application;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace Schedule.App.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
    {
        public override Application FormsApplication
        {
            get
            {
                if (!Xamarin.Forms.Forms.IsInitialized)
                {
                    global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
                }

                return base.FormsApplication;
            }
        }
    }
}
