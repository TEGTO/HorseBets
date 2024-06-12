using FluentValidation;
using HorseBets.Bets.Models.Dto;

namespace HorseBetsServer.Bets.Validators
{
    public class BetDtoValidator : AbstractValidator<BetDto>
    {
        public BetDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Client.Balance).Must((bet, balance) => balance >= bet.BetAmount);
            RuleFor(x => x.Match.IsActive).Equal(false);
        }
    }
}
