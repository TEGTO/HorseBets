using AutoMapper;
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;
using HorseBets.Bets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorseBets.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BetController : ControllerBase
    {
        private readonly IBetService betManager;
        private readonly IMapper mapper;

        public BetController(IBetService betManager, IMapper mapper)
        {
            this.betManager = betManager;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{betId}")]
        public async Task<ActionResult<BetDto>> GetBetById([FromRoute] string betId, CancellationToken cancellationToken)
        {
            Bet? bet = await betManager.GetBetByIdAsync(betId, cancellationToken);
            if (bet == null)
                return NotFound();
            return Ok(mapper.Map<BetDto>(bet));
        }
        [HttpGet]
        [Route("client")]
        public async Task<ActionResult<IEnumerable<BetDto>>> GetBetsByClientIdOnPage([FromQuery] string clientId,
            [FromQuery] int page, [FromQuery] int amount, CancellationToken cancellationToken)
        {
            IEnumerable<Bet> matches = await betManager.GetBetsByClientIdOnPageAsync(clientId, page, amount, cancellationToken);
            if (matches == null)
                return NotFound();
            return Ok(matches.Select(mapper.Map<BetDto>));
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<BetDto>> CreateBet([FromBody] BetDto betDto, CancellationToken cancellationToken)
        {
            Bet bet = mapper.Map<Bet>(betDto);
            Bet newBet = await betManager.CreateBetAsync(bet, cancellationToken);
            return mapper.Map<BetDto>(newBet);
        }
    }
}
