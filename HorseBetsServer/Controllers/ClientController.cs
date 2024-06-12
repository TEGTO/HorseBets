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
        private readonly ClientService clientManager;
        private readonly ILogger<ClientController> logger;
        private readonly IMapper mapper;

        public ClientController(ClientService clientManager, ILogger<ClientController> logger, IMapper mapper)
        {
            this.clientManager = clientManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<ClientDto>> GetClientByUserId([FromRoute] string userId, CancellationToken cancelentionToken)
        {
            Client client = await clientManager.GetClientByUserIdAsync(userId, cancelentionToken);
            if (client == null)
                return NotFound();
            return Ok(mapper.Map<ClientDto>(client));
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateClientForUser([FromBody] CreateClientDto clientDto, CancellationToken cancelentionToken)
        {
            await clientManager.CreateClientForUserAsync(clientDto.UserId, cancelentionToken);
            return Ok();
        }
        [HttpPatch]
        [Route("balance/add")]
        public async Task<IActionResult> AddValueToClientBalance([FromBody] ChangeClientBalanceDto changeBalanceDto, CancellationToken cancelentionToken)
        {
            await clientManager.AddValueToClientBalance(changeBalanceDto.ClientId, changeBalanceDto.Value, cancelentionToken);
            return Ok();
        }
        [HttpPatch]
        [Route("balance/reduce")]
        public async Task<IActionResult> ReduceValueToClientBalance([FromBody] ChangeClientBalanceDto changeBalanceDto, CancellationToken cancelentionToken)
        {
            await clientManager.ReduceValueFromClientBalance(changeBalanceDto.ClientId, changeBalanceDto.Value, cancelentionToken);
            return Ok();
        }
    }
}
