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
        public async Task<ActionResult<ClientDto>> GetClientByUserId([FromRoute] string userId, CancellationToken cancellationToken)
        {
            Client? client = await clientManager.GetClientByUserIdAsync(userId, cancellationToken);
            if (client == null)
                return NotFound();
            return Ok(mapper.Map<ClientDto>(client));
        }
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CreateClientDto>> CreateClientForUser([FromBody] CreateClientDto clientDto, CancellationToken cancellationToken)
        {
            var newClient = await clientManager.CreateClientForUserIdAsync(clientDto.UserId, cancellationToken);
            return Ok(mapper.Map<ClientDto>(newClient));
        }
        [HttpPatch]
        [Route("balance/add")]
        public async Task<IActionResult> AddValueToClientBalance([FromBody] ChangeClientBalanceDto changeBalanceDto, CancellationToken cancellationToken)
        {
            await clientManager.AddValueToClientBalanceAsync(changeBalanceDto.ClientId, changeBalanceDto.Value, cancellationToken);
            return Ok();
        }
        [HttpPatch]
        [Route("balance/reduce")]
        public async Task<IActionResult> ReduceValueToClientBalance([FromBody] ChangeClientBalanceDto changeBalanceDto, CancellationToken cancellationToken)
        {
            await clientManager.ReduceValueFromClientBalanceAsync(changeBalanceDto.ClientId, changeBalanceDto.Value, cancellationToken);
            return Ok();
        }
    }
}
