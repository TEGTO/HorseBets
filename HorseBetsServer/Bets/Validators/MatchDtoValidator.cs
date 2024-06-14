using FluentValidation;
using HorseBets.Bets.Models.Dto;

namespace HorseBetsServer.Bets.Validators
{
    public class MatchDtoValidator : AbstractValidator<MatchDto>
    {
        public MatchDtoValidator()
        {
            RuleFor(x => x.Participants).NotNull();
            RuleFor(x => x.Participants).NotEmpty();
            RuleFor(x => x.StartTime.ToUniversalTime()).GreaterThanOrEqualTo(DateTime.UtcNow);
        }
    }
}
