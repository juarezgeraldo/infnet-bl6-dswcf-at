using Domain;
using EstadoAPI.Services;
using PaisAPI.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EstadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;
        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        // GET: api/<EstadoController>
        [HttpGet]
        public IEnumerable<Estado> SelecionaEstados()
        {
            return _estadoService.SelecionaEstados();
        }

        // GET api/<EstadoController>/5
        [HttpGet("{id}")]
        public Estado SelecionaEstadoId(int id)
        {
            return _estadoService.SelecionaEstadoId(id);
        }
        [HttpGet("pais/{paisId}")]
        public IEnumerable<Estado> SelecionaEstadosPais(int paisId)
        {
            return _estadoService.SelecionEstadosPais(paisId);
        }

        // POST api/<EstadoController>
        [HttpPost]
        public IActionResult IncluiEstado([FromBody] IncluiEstadoDTO incluiEstadoDTO)
        {
            if (ModelState.IsValid)
            {
                _estadoService.IncluiEstado(incluiEstadoDTO);
                return RedirectToAction(nameof(SelecionaEstados));
            }
            return new EmptyResult();
        }

        // PUT api/<EstadoController>/5
        [HttpPut("{id}")]
        public IActionResult AlteraEstado(int id, [FromBody] IncluiEstadoDTO incluiEstadoDTO)
        {
            if (ModelState.IsValid)
            {
                var alteraEstadoDTO = new AlteraEstadoDTO();
                alteraEstadoDTO.Id = id;
                alteraEstadoDTO.Nome = incluiEstadoDTO.Nome;
                alteraEstadoDTO.BandeiraIdBase64 = incluiEstadoDTO.BandeiraIdBase64;
                alteraEstadoDTO.PaisId = incluiEstadoDTO.PaisId;

                _estadoService.AlteraEstado(alteraEstadoDTO);
                return Ok();
            }
            return new EmptyResult();
        }

        // DELETE api/<EstadoController>/5
        [HttpDelete("{id}")]
        public IActionResult ExcluiEstado(int id)
        {
            _estadoService.ExcluiEstado(id);
            return Ok();
        }
    }
}
