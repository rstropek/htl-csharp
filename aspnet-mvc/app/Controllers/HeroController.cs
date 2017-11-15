using AspNetMvcDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetMvcDemo.Controllers
{
    public class HeroesController : Controller
    {
        private IHeroRepository heroRepository;

        // Note dependency injection in controller here. Learn more at 
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/dependency-injection
        public HeroesController(IHeroRepository heroRepository)
        {
            this.heroRepository = heroRepository;
        }

        public IActionResult Index()
        {
            return View(heroRepository.GetAll());
        }

        public IActionResult Details(int id)
        {
            var hero = heroRepository.GetById(id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        public IActionResult Delete(int id)
        {
            var hero = heroRepository.GetById(id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            heroRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
