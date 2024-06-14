namespace HorseBets.Bets.Services.Api
{
    public abstract class BaseBetApiService
    {
        protected readonly IHttpClientFactory httpClientFactory;
        protected readonly ILogger<BaseBetApiService> logger;

        public BaseBetApiService(IHttpClientFactory httpClientFactory, ILogger<BaseBetApiService> logger)
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
