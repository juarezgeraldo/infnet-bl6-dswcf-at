using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using PaisMVC.Models;
using PaisMVC.Models.Amigo;
using PaisMVC.Models.Estado;
using PaisMVC.Models.Pais;

namespace PaisMVC.Controllers
{
    public class AmigoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string url = "https://amigosapi.azurewebsites.net/api/";
        private readonly string urlPais = "https://paises.azurewebsites.net/api/";
        public AmigoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: AmigoController
        public async Task<ActionResult> Index()
        {
            var amigos = await $"{url}amigo".GetJsonAsync<IEnumerable<Amigo>>();
            return View(amigos);
        }

        // GET: AmigoController/Details/5
        public async Task<ActionResult> Detalhes(int id, int amigoId)
        {
            var amigo = await $"{url}amigo/{id}"
                .GetJsonAsync<Amigo>();

            var amigoList = await $"{url}amigo/amigos/{id}"
                .GetJsonAsync<IEnumerable<Amigo>>();

            ViewBag.AmigoList = amigoList;
            ViewBag.QtdAmigoList = amigoList.Count();

            return View(amigo);
        }

        // GET: AmigoController/Create
        public async Task<ActionResult> Incluir()
        {
            var estados = await $"{urlPais}estado"
                .GetJsonAsync<IEnumerable<Estado>>();

            ViewBag.Estados = estados;

            return View();
        }

        // POST: AmigoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Incluir(IncluiAmigo incluiAmigo)
        {
            try
            {
                var arquivo = incluiAmigo.FormFile;
                var base64 = Base64Utils.Base64(arquivo);

                incluiAmigo.FotografiaIdBase64 = base64;

                var EstadoPais = incluiAmigo?.EstadoPais?.Split('-');

                incluiAmigo.EstadoId = Convert.ToInt32(EstadoPais[0]);
                incluiAmigo.PaisId = Convert.ToInt32(EstadoPais[1]);

                var response = await $"{url}/amigo"
                    .PostJsonAsync(incluiAmigo);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AmigoController/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            var amigo = await $"{url}amigo/{id}"
                .GetJsonAsync<IncluiAmigo>();

            var estados = await $"{urlPais}estado"
                .GetJsonAsync<IEnumerable<Estado>>();

            var amigoList = await $"{url}amigo/amigos/{id}"
                .GetJsonAsync<IEnumerable<Amigo>>();

            var amigos = await $"{url}amigo"
                .GetJsonAsync<IEnumerable<Amigo>>();

            var estado = await $"{urlPais}estado/{amigo.EstadoId}"
                .GetJsonAsync<Estado>();

            var pais = await $"{urlPais}pais/{amigo.PaisId}"
                .GetJsonAsync<Pais>();

            amigo.EstadoPais = $"{amigo.EstadoId}-{amigo.PaisId}";

            ViewBag.Estado = estado;
            ViewBag.Pais = pais;
            ViewBag.AmigoList = amigoList;
            ViewBag.Amigos = amigos;
            ViewBag.NumeroAmigos = amigos.Count();
            ViewBag.TotalAmigosList = amigoList.Count();

            ViewBag.Estados = estados;

            return View(amigo);
        }

        // POST: AmigoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, IncluiAmigo incluiAmigo)
        {
            try
            {
                if (incluiAmigo.FormFile != null)
                {
                    var file = incluiAmigo.FormFile;
                    incluiAmigo.FotografiaIdBase64 = Base64Utils.Base64(file);
                }

                var estadoPais = incluiAmigo?.EstadoPais?.Split('-');

                incluiAmigo.EstadoId = Convert.ToInt32(estadoPais[0]);
                incluiAmigo.PaisId = Convert.ToInt32(estadoPais[1]);

                var response = await $"{url}amigo/{id}"
                    .PutJsonAsync(incluiAmigo);

                return RedirectToAction(nameof(Index));
            }
            catch (FlurlHttpException ex)
            {
                ViewBag.ErrorMessage = ex.GetResponseStringAsync();
                return View(incluiAmigo);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(incluiAmigo);
            }
        }

        // GET: AmigoController/Delete/5
        public async Task<ActionResult> Excluir(int id)
        {
            var amigo = await $"{url}amigo/{id}"
                .GetJsonAsync<Amigo>();

            return View(amigo);
        }

        // POST: AmigoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Amigo amigo)
        {
            try
            {
                var amigoExcluido = await $"{url}amigo/{amigo.Id}"
                    .DeleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> IncluiAmigoLista(int id, int amigoId)
        {
            var amigo = await $"{url}amigo/{id}"
                .GetJsonAsync<Amigo>();

            var amigoList = await $"{url}amigo/{amigoId}"
                .GetJsonAsync<Amigo>();

            return View(new IncluiAmigoList
            {
                Amigo = amigo,
                AmigoList = amigoList
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IncluiAmigoLista(IncluiAmigoList incluiAmigoList)
        {
            try
            {
                var id = incluiAmigoList?.Id;
                var amigoId = incluiAmigoList?.AmigoId;

                var response = await $"{url}amigo/amigos/{id}/{amigoId}"
                    .PutJsonAsync(null);

                return RedirectToAction(nameof(Detalhes), new { id = id });
            }
            catch
            {
                return View(incluiAmigoList);
            }
        }

        public async Task<ActionResult> ExcluiAmigoLista(int id, int amigoId)
        {
            var amigo = await $"{url}amigo/{id}"
                .GetJsonAsync<Amigo>();

            var amigoList = await $"{url}amigo/{amigoId}"
                .GetJsonAsync<Amigo>();

            return View(new IncluiAmigoList
            {
                Amigo = amigo,
                AmigoList = amigoList
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExcluiAmigoLista(IncluiAmigoList incluiAmigoList)
        {
            try
            {
                var id = incluiAmigoList?.Id;
                var amigoId = incluiAmigoList?.AmigoId;

                var response = await $"{url}amigo/amigos/{id}/{amigoId}"
                    .DeleteAsync();

                return RedirectToAction(nameof(Detalhes), new { id = id });
            }
            catch
            {
                return View(incluiAmigoList);
            }
        }
    }
}
