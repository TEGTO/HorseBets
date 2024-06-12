using AutoMapper;
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;
using HorseBets.Bets.Services;
using Microsoft.AspNetCore.Mvc;

namespace HorseBets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly MatchService matchService;
        private readonly ILogger<MatchController> logger;
        private readonly IMapper mapper;

        public MatchController(MatchService matchService, ILogger<MatchController> logger, IMapper mapper)
        {
            this.matchService = matchService;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{matchId}")]
        public async Task<ActionResult<MatchDto>> GetMatchById([FromRoute] int matchId, CancellationToken cancelentionToken)
        {
            Match match = await matchService.GetMatchById(matchId, cancelentionToken);
            if (match == null)
                return NotFound();
            return Ok(mapper.Map<MatchDto>(match));
        }
        [HttpGet]
        [Route("totalPages")]
        public async Task<ActionResult<int>> GetTotalAmountOfPages([FromQuery] int amountOnPage, [FromQuery] bool onlyActive, CancellationToken cancelentionToken)
        {
            return Ok(await matchService.GetPagesAmountAsync(amountOnPage, cancelentionToken, onlyActive));
        }
        [HttpGet]
        [Route("matchesOnPage")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesOnPage([FromQuery] int page, [FromQuery] int amountOnPage, [FromQuery] bool onlyActive, CancellationToken cancelentionToken)
        {
            IEnumerable<Match> matches = await matchService.GetMatchesOnPageAsync(page, amountOnPage, cancelentionToken, onlyActive);
            if (matches == null)
                return NotFound();
            return Ok(matches.Select(mapper.Map<MatchDto>));
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<MatchDto>> CreateMatch([FromBody] MatchDto matchDto, CancellationToken cancelentionToken)
        {
            if (matchDto == null)
                return BadRequest();
            Match match = mapper.Map<Match>(matchDto);
            await matchService.CreateMatch(match, cancelentionToken);
            return await GetMatchById(match.Id, cancelentionToken);
        }
        [HttpDelete]
        [Route("cancel/{matchId}")]
        public async Task<ActionResult> CancelMatch([FromRoute] int matchId, CancellationToken cancelentionToken)
        {
            await matchService.CancelMatch(matchId, cancelentionToken);
            return Ok();
        }
    }
}
