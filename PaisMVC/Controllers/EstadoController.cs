using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using PaisMVC.Models;
using PaisMVC.Models.Estado;
using PaisMVC.Models.Pais;

namespace PaisMVC.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string url = "https://paises.azurewebsites.net/api/";
        public EstadoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: EstadoController/Details/5
        public async Task<ActionResult> Detalhes(int id)
        {
            var estado = await $"{url}estado/{id}"
                .GetJsonAsync<Estado>();

            var pais = await $"{url}pais/{estado.PaisId}"
                .GetJsonAsync<Pais>();

            ViewBag.Pais = pais;

            return View(estado);
        }

        // GET: EstadoController/Create
        public async Task<ActionResult> Incluir(int id)
        {
            var paises = await $"{url}pais"
                .GetJsonAsync<IEnumerable<Pais>>();

            ViewBag.Paises = paises;

            return View(new IncluiEstado
            {
                PaisId = id
            });
        }

        // POST: EstadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Incluir(IncluiEstado incluiEstado)
        {
            try
            {
                var file = incluiEstado.FormFile;
                var base64 = Base64Utils.Base64(file);

                incluiEstado.BandeiraIdBase64 = base64;

                var response = await $"{url}estado"
                    .PostJsonAsync(incluiEstado);

                return RedirectToAction("Detalhes", "Pais", new { id = incluiEstado.PaisId });
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            var estado = await $"{url}estado/{id}"
                .GetJsonAsync<IncluiEstado>();

            var pais = await $"{url}pais/{estado.PaisId}"
                .GetJsonAsync<Pais>();

            ViewBag.Pais = pais;

            var paises = await $"{url}pais"
                .GetJsonAsync<IEnumerable<Pais>>();

            ViewBag.Paises = paises;


            return View(estado);
        }

        // POST: EstadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, IncluiEstado incluiEstado)
        {
            try
            {
                if (incluiEstado.FormFile != null)
                {
                    var file = incluiEstado.FormFile;
                    incluiEstado.BandeiraIdBase64 = Base64Utils.Base64(file);
                }

                var response = await $"{url}estado/{id}"
                    .PutJsonAsync(new
                    {
                        PhotoId = incluiEstado.BandeiraId ?? string.Empty,
                        incluiEstado.Nome,
                        incluiEstado.BandeiraIdBase64,
                        incluiEstado.PaisId
                    });

                return RedirectToAction("Detalhes", "Pais", new { id = incluiEstado.PaisId });
            }
            catch (FlurlHttpException ex)
            {
                ViewBag.ErrorMessage = ex.GetResponseStringAsync();
                return View(incluiEstado);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(incluiEstado);
            }
        }

        // GET: StatesController/Delete/5
        public async Task<ActionResult> Excluir(int id)
        {
            var estado = await $"{url}estado/{id}"
                .GetJsonAsync<Estado>();

            return View(estado);
        }

        // POST: StatesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(int id, Estado estado)
        {
            try
            {
                var estado1 = await $"{url}estado/{estado.Id}"
                    .DeleteAsync();

                return RedirectToAction("Detalhes", "Pais", new { id = estado.PaisId });
            }
            catch
            {
                return View();
            }
        }
    }
}
