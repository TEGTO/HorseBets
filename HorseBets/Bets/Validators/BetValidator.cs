using FluentValidation;
using HorseBets.Bets.Models;
using HorseBets.Bets.Services.Api;

namespace HorseBets.Bets.Validators
{
    public class BetValidator : AbstractValidator<Bet>
    {
        private readonly IMatchApi matchService;
        private readonly HashSet<int> finishedMatchesId;

        public BetValidator(IMatchApi matchService)
        {
            this.matchService = matchService;
            this.finishedMatchesId = new HashSet<int>();
            Validate();
        }
        public void Validate()
        {
            RuleFor(x => x.Horse).NotEmpty();
            RuleFor(x => x.Client).NotEmpty();
            RuleFor(x => x.Match).NotEmpty();
            RuleFor(x => x.BetAmount).GreaterThanOrEqualTo(0.05m).WithMessage("Bet must be greater than or equal to 0.05$.");
            RuleFor(x => x.Match.IsActive).Equal(true).WithMessage("Match is over!");
            RuleFor(x => x.Match.Id).MustAsync(async (id, cancellation) =>
            {
                if (finishedMatchesId.Contains(id))
                    return false;
                else
                {
                    Match? matchInDb = await matchService.GetMatchByIdAsync(id, cancellation);
                    bool isMatchActive = matchInDb == null ? false : matchInDb.IsActive;
                    if (!isMatchActive)
                        finishedMatchesId.Add(id);
                    return isMatchActive;
                }
            }).WithMessage("Match is over, no new bets will be accepted!");
        }
    }
}
