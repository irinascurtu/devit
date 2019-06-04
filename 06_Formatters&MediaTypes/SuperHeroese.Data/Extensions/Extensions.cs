using System.Collections.Generic;
using System.Linq;
using SuperHeroes.Domain;

namespace SuperHeroese.Data.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<SuperHero> Filter(this IEnumerable<SuperHero> heroes, Filter filter)
        {
            if (filter.Country != null)
            {
                heroes = heroes.Where(x => filter.Country.Contains(x.Country)).Select(x => x);
            }

            if (filter.Page == null)
            {
                return heroes.OrderBy(x=>x.Id).ToList();
            }
            return heroes.OrderBy(x => x.Id).Skip((filter.Page.Value - 1) * filter.PageSize).Take(filter.PageSize).ToList();
        }
    }
}
