using FluentValidation;
using HorseBets.Bets.Models;
using HorseBets.Bets.Services.Api;

namespace HorseBets.Bets.Services
{
    public class BetService : IBetService
    {
        private IBetApi betApi;
        private IValidator<Bet> validator;
        private IClientService clientService;

        public BetService(IBetApi betApi, IValidator<Bet> validator, IClientService clientService)
        {
            this.betApi = betApi;
            this.validator = validator;
            this.clientService = clientService;
        }

        public async Task<IEnumerable<Bet>> GetBetsByClientOnPageAsync(Client client, int page, int amountOnPage, CancellationToken cancellationToken = default)
        {
            return await betApi.GetBetsByClientOnPageAsync(client, page, amountOnPage, cancellationToken);
        }
        public async Task CreateBetAsync(Bet bet, CancellationToken cancellationToken = default)
        {
            await validator.ValidateAsync(bet);
            var createdBet = await betApi.CreateBetAsync(bet, cancellationToken);
            if (createdBet != null)
                clientService.UpdateClientBalance(createdBet.Client.Balance);
        }
    }
}
