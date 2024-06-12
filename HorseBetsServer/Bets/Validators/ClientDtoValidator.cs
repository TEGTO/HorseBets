using FluentValidation;
using HorseBets.Bets.Models.Dto;

namespace HorseBetsServer.Bets.Validators
{
    public class ClientDtoValidator : AbstractValidator<ClientDto>
    {
        public ClientDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        }
    }
}
