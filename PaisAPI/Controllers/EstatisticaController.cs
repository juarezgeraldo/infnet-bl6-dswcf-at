using Domain;
using EstadoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using PaisAPI.DTO;
using PaisAPI.Services;

namespace PaisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatisticaController : Controller
    {
        private readonly IEstatisticaService _estatisticaService;
        public EstatisticaController(IEstatisticaService estatisticaService)
        {
            _estatisticaService = estatisticaService;
        }

        [HttpGet]
        public EstatisticaDTO BuscaEstatistica()
        {
            return _estatisticaService.BuscaEstatisticas();
        }
    }
}
