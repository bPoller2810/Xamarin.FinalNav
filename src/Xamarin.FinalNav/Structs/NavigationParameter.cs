using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.FinalNav
{
    public class NavigationParameter
    {

        public object Parameter { get; init; }
        public EParameterType Type { get; init; }

    }

    public class PageParameter : NavigationParameter
    {
        public PageParameter()
        {
            Type = EParameterType.Page;
        }
    }

    public class ViewModelParameter : NavigationParameter
    {
        public ViewModelParameter()
        {
            Type = EParameterType.ViewModel;
        }
    }

}
