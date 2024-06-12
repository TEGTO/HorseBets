﻿using AutoMapper;
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;
using HorseBets.Bets.Services;
using Microsoft.AspNetCore.Mvc;

namespace HorseBets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BetController : ControllerBase
    {
        private readonly BetService betManager;
        private readonly ILogger<ClientController> logger;
        private readonly IMapper mapper;

        public BetController(BetService betManager, ILogger<ClientController> logger, IMapper mapper)
        {
            this.betManager = betManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{betId}")]
        public async Task<ActionResult<BetDto>> GetBetById([FromRoute] string betId, CancellationToken cancelentionToken)
        {
            Bet bet = await betManager.GetBetById(betId, cancelentionToken);
            if (bet == null)
                return NotFound();
            return Ok(mapper.Map<BetDto>(bet));
        }
        [HttpGet]
        [Route("client")]
        public async Task<ActionResult<IEnumerable<BetDto>>> GetBetsByClientIdOnPage([FromQuery] string clientId,
            [FromQuery] int page, [FromQuery] int amount, CancellationToken cancelentionToken)
        {
            IEnumerable<Bet> matches = await betManager.GetBetsByClientIdOnPage(clientId, page, amount, cancelentionToken);
            if (matches == null)
                return NotFound();
            return Ok(matches.Select(mapper.Map<BetDto>));
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<BetDto>> CreateBet([FromBody] BetDto betDto, CancellationToken cancelentionToken)
        {
            Bet bet = mapper.Map<Bet>(betDto);
            await betManager.CreateBet(bet, cancelentionToken);
            return await GetBetById(bet.Id, cancelentionToken);
        }
    }
}