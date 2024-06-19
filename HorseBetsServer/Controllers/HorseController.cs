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
    public class HorseController : ControllerBase
    {
        private readonly IHorseService horseService;
        private readonly IMapper mapper;

        public HorseController(IHorseService horseService, IMapper mapper)
        {
            this.horseService = horseService;
            this.mapper = mapper;
        }
        [HttpGet, AllowAnonymous]
        [Route("horses")]
        public async Task<ActionResult<IEnumerable<HorseDto>>> GetAllHorses(CancellationToken cancellationToken)
        {
            IEnumerable<Horse> horses = await horseService.GetHorsesAsync(cancellationToken);
            if (horses == null)
                return NotFound();
            return Ok(horses.Select(mapper.Map<HorseDto>));
        }
    }
}
