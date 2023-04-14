using AmigoAPI.DTO;
using AmigoAPI.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmigoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        private readonly IAmigoService _amigoService;
        public AmigoController(IAmigoService amigoService)
        {
            _amigoService = amigoService;
        }

        // GET: api/<AmigoController>
        [HttpGet]
        public IEnumerable<Amigo> SelecionaAmigos()
        {
            return _amigoService.SelecionaAmigos();
        }

        // GET api/<AmigoController>/5
        [HttpGet("{id}")]
        public Amigo SelecionaAmigoId(int id)
        {
            return _amigoService.SelecionaAmigoId(id);
        }

        // POST api/<AmigoController>
        [HttpPost]
        public IActionResult IncluiAmigo([FromBody] IncluiAmigoDTO incluiAmigoDTO)
        {
            if (ModelState.IsValid)
            {
                _amigoService.IncluiAmigo(incluiAmigoDTO);
                return RedirectToAction(nameof(SelecionaAmigos));
            }
            return new EmptyResult();
        }

        // PUT api/<AmigoController>/5
        [HttpPut("{id}")]
        public IActionResult AlteraAmigo(int id, [FromBody] IncluiAmigoDTO incluiAmigoDTO)
        {
            if (ModelState.IsValid)
            {
                var alteraAmigoDTO = new AlteraAmigoDTO();
                alteraAmigoDTO.Id = id;
                alteraAmigoDTO.Nome = incluiAmigoDTO.Nome;
                alteraAmigoDTO.Sobrenome = incluiAmigoDTO.Sobrenome;
                alteraAmigoDTO.Email = incluiAmigoDTO.Email;
                alteraAmigoDTO.Telefone = incluiAmigoDTO.Telefone;
                alteraAmigoDTO.Nascimento = incluiAmigoDTO.Nascimento;
                alteraAmigoDTO.FotografiaIdBase64 = incluiAmigoDTO.FotografiaIdBase64;
                alteraAmigoDTO.EstadoId = incluiAmigoDTO.EstadoId;
                alteraAmigoDTO.PaisId = incluiAmigoDTO.PaisId;

                _amigoService.AlteraAmigo(alteraAmigoDTO);
                return Ok();
            }
            return new EmptyResult();
        }

        // DELETE api/<AmigoController>/5
        [HttpDelete("{id}")]
        public IActionResult ExcluiAmigo(int id)
        {
            _amigoService.ExcluiAmigo(id);
            return Ok();
        }

        [HttpGet("amigos/{amigoId}")]
        public IEnumerable<Amigo> SelecionaAmigosAmigo(int amigoId)
        {
            return _amigoService.SelecionaAmigosAmigo(amigoId);
        }

        [HttpPut("amigos/{Id}/{amigoId}")]
        public IActionResult IncluiAmigoList(int Id, int amigoId)
        {
            if (ModelState.IsValid)
            {
                _amigoService.IncluiAmigoList(Id, amigoId);
                return Ok();
            }
            return new EmptyResult();
        }
        [HttpDelete("amigos/{Id}/{amigoId}")]
        public IActionResult ExcluiAmigoList(int Id, int amigoId)
        {
            _amigoService.ExcluiAmigoList(Id, amigoId);
            return Ok();
        }
    }
}
