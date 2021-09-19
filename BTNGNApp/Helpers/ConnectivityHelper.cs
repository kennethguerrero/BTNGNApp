using Xamarin.Essentials;

namespace BTNGNApp.Helpers
{
    public class ConnectivityHelper : IConnectivityHelper
    {
        public bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
