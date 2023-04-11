using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using PaisAPI.DTO;
using PaisMVC.Models;
using PaisMVC.Models.Estado;
using PaisMVC.Models.Pais;

namespace PaisMVC.Controllers
{
    public class PaisController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly string url = "https://paises.azurewebsites.net/api/";
        // GET: CountriesController
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaisController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
