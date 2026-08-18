namespace MultiShop.SignalRRealTimeApi.Settings
{
    public class ClientSettings
    {
        public Client MultiShopSignalRClient { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
