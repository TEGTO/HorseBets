using FluentValidation;
using HorseBets.Bets.Models.Dto;

namespace HorseBetsServer.Bets.Validators
{
    public class HorseDtoValidator : AbstractValidator<HorseDto>
    {
        public HorseDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
