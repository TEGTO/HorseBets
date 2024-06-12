namespace HorseBets.Bets.Services
{
    public abstract class BaseBetsApi
    {
        protected readonly IHttpClientFactory httpClientFactory;
        protected readonly ILogger<ClientService> logger;

        public BaseBetsApi(IHttpClientFactory httpClientFactory, ILogger<ClientService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        protected HttpClient CreateHttpClient()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("bets");
            return httpClient;
        }
    }
}
