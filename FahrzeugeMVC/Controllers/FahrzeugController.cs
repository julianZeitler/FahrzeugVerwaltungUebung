using Microsoft.AspNetCore.Mvc;
using FahrzeugDatenbank;
using FahrzeugeMVC.Models;

namespace FahrzeugeMVC.Controllers
{
    public class FahrzeugController : Controller
    {
        public IActionResult Index()
        {
            string connectionString = this.GetConnectionString();
            var repository = new FahrzeugRepository(connectionString);
            List<FahrzeugDTO>? fahrzeuge =  repository.HoleAlleFahrzeuge();

            var model = new FahrzeugListeModel(fahrzeuge);

            return View(model);
        }

        private string GetConnectionString()
        {
            return "Server=localhost;User ID=root;Password=admin;Database=FahrzeugDB";
        }

        [HttpGet]
        public IActionResult Einfuegen()
        {
            var model = new FahrzeugEinfuegenModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Einfuegen(FahrzeugEinfuegenModel model)
        {
            if (ModelState.IsValid
                && !string.IsNullOrEmpty(model.Name)
                && !string.IsNullOrEmpty(model.Type))
            {
                string connectionString = this.GetConnectionString();
                var repository = new FahrzeugRepository(connectionString);
                repository.FuegeFahrzeugEin(model.Name, model.Type);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Loesche(int id)
        {
            string connectionString = this.GetConnectionString();
            var repository = new FahrzeugRepository(connectionString);
            repository.LoescheFahrzeug(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Aktualisieren(int id)
        {
            string connectionString = this.GetConnectionString();
            var repository = new FahrzeugRepository(connectionString);
            var fahrzeug = new FahrzeugDTO();
            fahrzeug = repository.holeFahrzeug(id);

            var model = new Aktualisieren();
            model.Id = id;
            model.Name = fahrzeug.Name;
            model.Type = fahrzeug.Typ;
            return View(model);
        }

        [HttpPost]
        public IActionResult Aktualisieren(Aktualisieren model)
        {
            if (ModelState.IsValid
                && !string.IsNullOrEmpty(model.Name)
                && !string.IsNullOrEmpty(model.Type))
            {
                string connectionString = this.GetConnectionString();
                var repository = new FahrzeugRepository(connectionString);
                repository.AktualisiereFahrzeug((int)model.Id, model.Name, model.Type);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
        }
    }
}
