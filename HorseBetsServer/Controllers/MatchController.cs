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
    public class MatchController : ControllerBase
    {
        private readonly IMatchService matchService;
        private readonly IMapper mapper;

        public MatchController(IMatchService matchService, IMapper mapper)
        {
            this.matchService = matchService;
            this.mapper = mapper;
        }
        [HttpGet, AllowAnonymous]
        [Route("{matchId}")]
        public async Task<ActionResult<MatchDto>> GetMatchById([FromRoute] int matchId, CancellationToken cancellationToken)
        {
            Match match = await matchService.GetMatchByIdAsync(matchId, cancellationToken);
            if (match == null)
                return NotFound();
            return Ok(mapper.Map<MatchDto>(match));
        }
        [HttpGet, AllowAnonymous]
        [Route("totalPages")]
        public async Task<ActionResult<int>> GetTotalAmountOfPages([FromQuery] int amountOnPage, [FromQuery] bool onlyActive, CancellationToken cancellationToken)
        {
            return Ok(await matchService.GetPagesAmountAsync(amountOnPage, cancellationToken, onlyActive));
        }
        [HttpGet, AllowAnonymous]
        [Route("matchesOnPage")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesOnPage([FromQuery] int page, [FromQuery] int amountOnPage, [FromQuery] bool onlyActive, CancellationToken cancellationToken)
        {
            IEnumerable<Match> matches = await matchService.GetMatchesOnPageAsync(page, amountOnPage, cancellationToken, onlyActive);
            if (matches == null)
                return NotFound();
            return Ok(matches.Select(mapper.Map<MatchDto>));
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<MatchDto>> CreateMatch([FromBody] MatchDto matchDto, CancellationToken cancellationToken)
        {
            if (matchDto == null)
                return BadRequest();
            Match match = mapper.Map<Match>(matchDto);
            var newMatch = await matchService.CreateMatchAsync(match, cancellationToken);
            return Ok(mapper.Map<MatchDto>(newMatch));
        }
        [HttpDelete]
        [Route("cancel/{matchId}")]
        public async Task<ActionResult> CancelMatch([FromRoute] int matchId, CancellationToken cancellationToken)
        {
            await matchService.CancelMatchAsync(matchId, cancellationToken);
            return Ok();
        }
    }
}
