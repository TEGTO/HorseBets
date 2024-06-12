using AutoMapper;
using HorseBets.Bets.Models;
using HorseBets.Bets.Models.Dto;
using HorseBets.Bets.Services;
using Microsoft.AspNetCore.Mvc;

namespace HorseBets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HorseController : ControllerBase
    {
        private readonly HorseService horseService;
        private readonly ILogger<HorseController> logger;
        private readonly IMapper mapper;

        public HorseController(HorseService horseService, ILogger<HorseController> logger, IMapper mapper)
        {
            this.horseService = horseService;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("horses")]
        public async Task<ActionResult<IEnumerable<HorseDto>>> GetAllHorses(CancellationToken cancelentionToken)
        {
            IEnumerable<Horse> horses = await horseService.GetHorsesAsync(cancelentionToken);
            if (horses == null)
                return NotFound();
            return Ok(horses.Select(mapper.Map<HorseDto>));
        }
    }
}
