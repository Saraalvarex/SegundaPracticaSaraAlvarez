using Microsoft.AspNetCore.Mvc;
using SegundaPracticaSaraAlvarez.Models;
using SegundaPracticaSaraAlvarez.Repositories;

namespace SegundaPracticaSaraAlvarez.Controllers
{
    
    public class ComicsController : Controller
    {
        private IRepo repo;

        public ComicsController(IRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string nombre, string imagen, string descripcion)
        {
            this.repo.Insert(nombre, imagen, descripcion);
            return RedirectToAction("Index");
        }
    }
}
