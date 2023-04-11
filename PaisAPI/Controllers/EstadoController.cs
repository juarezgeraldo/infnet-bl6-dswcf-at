using Domain;
using EstadoAPI.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EstadoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;
        public EstadoController (IEstadoService estadoService)
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

        // POST api/<EstadoController>
        [HttpPost]
        public IActionResult IncluiEstado([FromBody] Estado estado)
        {
            if (ModelState.IsValid)
            {
                _estadoService.IncluiEstado(estado);
                return RedirectToAction(nameof(SelecionaEstados));
            }
            return new EmptyResult();
        }

        // PUT api/<EstadoController>/5
        [HttpPut("{id}")]
        public IActionResult AlteraEstado(int id, [FromBody] Estado estado)
        {
            if (ModelState.IsValid)
            {
                var estadoId = _estadoService.SelecionaEstadoId(id).Id;
                estado.Id = estadoId;
                _estadoService.AlteraEstado(estado);
                return RedirectToAction(nameof(SelecionaEstados));
            }
            return new EmptyResult();
        }

        // DELETE api/<EstadoController>/5
        [HttpDelete("{id}")]
        public IActionResult ExcluiEstado(int id)
        {
            if (ModelState.IsValid)
            {
                var estado = _estadoService.SelecionaEstadoId(id);
                _estadoService.ExcluiEstado(id);
                return RedirectToAction(nameof(SelecionaEstados));
            }
            return new EmptyResult();
        }
    }
}
