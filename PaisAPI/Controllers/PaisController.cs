using Domain;
using Microsoft.AspNetCore.Mvc;
using PaisAPI.DTO;
using PaisAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisService;
        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        // GET: api/<PaisController>
        [HttpGet]
        public IEnumerable<Pais> SelecionaPaises()
        {
            return _paisService.SelecionaPaises();
        }

        // GET api/<PaisController>/5
        [HttpGet("{id}")]
        public Pais SelecionaPaisId(int id)
        {
            return _paisService.SelecionaPaisId(id);
        }

        // POST api/<PaisController>
        [HttpPost]
        public IActionResult IncluiPais([FromBody] IncluiPaisDTO incluiPaisDTO)
        {
            if (ModelState.IsValid)
            {
                _paisService.IncluiPais(incluiPaisDTO);
                return RedirectToAction(nameof(SelecionaPaises));
            }
            return new EmptyResult();
        }

        // PUT api/<PaisController>/5
        [HttpPut("{id}")]
        public IActionResult AlteraPais(int id, [FromBody] IncluiPaisDTO incluiPaisDTO)
        {
            if (ModelState.IsValid)
            {
                var alteraPaisDTO = new AlteraPaisDTO();
                alteraPaisDTO.Id = id;
                alteraPaisDTO.Nome = incluiPaisDTO.Nome;
                alteraPaisDTO.BandeiraIdBase64 = incluiPaisDTO.BandeiraIdBase64;

                _paisService.AlteraPais(alteraPaisDTO);
                return Ok();
            }
            return new EmptyResult();
        }

        // DELETE api/<PaisController>/5
        [HttpDelete("{id}")]
        public IActionResult ExcluiPais(int id)
        {
            _paisService.ExcluiPais(id);
            return Ok();
        }
    }
}
