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
        public PageParameter(object parameter) : this()
        {
            Parameter = parameter;
        }
    }

    public class ViewModelParameter : NavigationParameter
    {
        public ViewModelParameter()
        {
            Type = EParameterType.ViewModel;
        }
        public ViewModelParameter(object parameter) : this()
        {
            Parameter = parameter;
        }
    }

}
