using AutoMapper;
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;
using HorseBets.Bets.Services;
using Microsoft.AspNetCore.Mvc;

namespace HorseBets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientManager;
        private readonly IMapper mapper;

        public ClientController(IClientService clientManager, IMapper mapper)
        {
            this.clientManager = clientManager;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<ClientDto>> GetClientByUserId([FromRoute] string userId, CancellationToken cancelentionToken)
        {
            Client? client = await clientManager.GetClientByUserIdAsync(userId, cancelentionToken);
            if (client == null)
                return NotFound();
            return Ok(mapper.Map<ClientDto>(client));
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateClientForUser([FromBody] CreateClientDto clientDto, CancellationToken cancelentionToken)
        {
            await clientManager.CreateClientForUserIdAsync(clientDto.UserId, cancelentionToken);
            return Ok();
        }
        [HttpPatch]
        [Route("balance/add")]
        public async Task<IActionResult> AddValueToClientBalance([FromBody] ChangeClientBalanceDto changeBalanceDto, CancellationToken cancelentionToken)
        {
            await clientManager.AddValueToClientBalanceAsync(changeBalanceDto.ClientId, changeBalanceDto.Value, cancelentionToken);
            return Ok();
        }
        [HttpPatch]
        [Route("balance/reduce")]
        public async Task<IActionResult> ReduceValueToClientBalance([FromBody] ChangeClientBalanceDto changeBalanceDto, CancellationToken cancelentionToken)
        {
            await clientManager.ReduceValueFromClientBalanceAsync(changeBalanceDto.ClientId, changeBalanceDto.Value, cancelentionToken);
            return Ok();
        }
    }
}
