namespace InternetDetection
{
    public interface IInternetTools
    {
        event InternetTools.InternetConnectionChangedHandler InternetConnectionChanged;
        bool IsConnected { get; }
    }
}