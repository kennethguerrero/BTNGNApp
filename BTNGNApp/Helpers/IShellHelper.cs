using System.Threading.Tasks;

namespace BTNGNApp.Helpers
{
    public interface IShellHelper
    {
        Task DisplayAlert(string message);
        Task GotoAsync(string param);
    }
}