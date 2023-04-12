using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using PaisMVC.Models;
using PaisMVC.Models.Estado;
using PaisMVC.Models.Pais;

namespace PaisMVC.Controllers
{
    public class PaisController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string url = "https://paises.azurewebsites.net/api/";
        public PaisController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: PaisController
        public async Task<ActionResult> Index()
        {
            var paises = await $"{url}pais".GetJsonAsync<IEnumerable<Pais>>();
            return View(paises);
        }

        // GET: PaisController/Details/5
        public async Task<ActionResult> Detalhes(int id)
        {
            var pais = await $"{url}pais/{id}"
                .GetJsonAsync<Pais>();

            var estados = await $"{url}estado/pais/{id}"
                .GetJsonAsync<IEnumerable<Estado>>();

            ViewBag.Estados = estados;
            ViewBag.QtdEstados = estados.Count();

            return View(pais);
        }

        // GET: PaisController/Create
        public ActionResult Incluir()
        {
            return View();
        }

        // POST: PaisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Incluir(IncluiPais incluiPais)
        {
            try
            {
                var arquivo = incluiPais.FormFile;
                var base64 = Base64Utils.Base64(arquivo);

                incluiPais.BandeiraIdBase64 = base64;

                var response = await $"{url}/pais"
                    .PostJsonAsync(incluiPais);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaisController/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            var pais = await $"{url}pais/{id}"
                .GetJsonAsync<IncluiPais>();

            var estados = await $"{url}estado/pais/{id}"
                .GetJsonAsync<IEnumerable<Estado>>();

            ViewBag.Estados = estados;
            ViewBag.NumberOfEstados = estados.Count();

            return View(pais);
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(int id, IncluiPais incluiPais)
        {
            try
            {
                if (incluiPais.FormFile != null)
                {
                    var file = incluiPais.FormFile;
                    incluiPais.BandeiraIdBase64 = Base64Utils.Base64(file);
                }

                //BandeiraId = incluiPais.BandeiraId ?? string.Empty,
                var response = await $"{url}pais/{id}"
                    .PutJsonAsync(new
                    {
                        BandeiraId = incluiPais.BandeiraId ?? string.Empty,
                        Nome = incluiPais.Nome,
                        BandeiraIdBase64 = incluiPais.BandeiraIdBase64
                    });

                return RedirectToAction(nameof(Index));
            }
            catch (FlurlHttpException ex)
            {
                ViewBag.ErrorMessage = ex.GetResponseStringAsync();
                return View(incluiPais);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(incluiPais);
            }
        }

        // GET: PaisController/Delete/5
        public async Task<ActionResult> Excluir(int id)
        {
            var pais = await $"{url}pais/{id}"
                .GetJsonAsync<Pais>();

            return View(pais);
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(int id, Pais pais)
        {
            try
            {
                var response = await $"{url}pais/{pais.Id}"
                    .DeleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
