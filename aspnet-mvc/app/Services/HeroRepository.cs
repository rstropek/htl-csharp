using AspNetMvcDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace AspNetMvcDemo.Services
{
    public class HeroRepository : IHeroRepository
    {
        private readonly List<Hero> heroes = new List<Hero>()
        {
            new Hero { ID = 1, Name = "Batman", RealName = "Bruce Wayne" },
            new Hero { ID = 2, Name = "Superman", RealName = "Clark Kent" },
        };

        public IEnumerable<Hero> GetAll()
        {
            return heroes.ToArray();
        }

        public Hero GetById(int id)
        {
            return heroes.FirstOrDefault(h => h.ID == id);
        }

        public void Delete(int id)
        {
            heroes.Remove(heroes.First(h => h.ID == id));
        }
    }
}
