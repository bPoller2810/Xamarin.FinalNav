using System.Threading.Tasks;

namespace Xamarin.FinalNav
{
    public interface IAsyncLifecycle
    {

        Task AppearingAsync();
        Task DisappearingAsync();
    }
}
