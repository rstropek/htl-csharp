using AspNetMvcDemo.Models;
using System.Collections.Generic;

namespace AspNetMvcDemo.Services
{
    public interface IHeroRepository
    {
        IEnumerable<Hero> GetAll();
        Hero GetById(int id);
        void Delete(int id);
    }
}
