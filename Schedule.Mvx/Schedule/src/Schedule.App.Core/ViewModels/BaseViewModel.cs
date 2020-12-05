using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MvvmCross.ViewModels;

namespace Schedule.App.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
