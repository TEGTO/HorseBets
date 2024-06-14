using FluentValidation;
using HorseBets.Bets.Models;

namespace HorseBets.Bets.Validators
{
    public class MatchValidator : AbstractValidator<Match>
    {
        public MatchValidator()
        {

        }
        public void Validate()
        {
            RuleFor(x => x.Participants).NotNull().NotEmpty();
        }
    }
}
