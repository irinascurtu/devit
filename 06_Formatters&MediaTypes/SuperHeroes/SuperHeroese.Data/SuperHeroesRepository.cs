using System;
using System.Collections.Generic;
using System.Linq;
using SuperHeroes;
using SuperHeroes.Domain;
using SuperHeroese.Data.Dto;
using SuperHeroese.Data.Extensions;

namespace SuperHeroese.Data
{
    public interface ISuperHeroesRepository
    {
        SuperHero Add(SuperHero superHero);
        List<SuperHero> GetAll(Filter filter);
        SuperHero GetById(int id);
        SuperHero Update(SuperHero superHeroToUpdate);
        SuperHero Delete(SuperHero superHeroToDelete);
        bool IsUnique(SuperHero heroToAdd);
        List<HeroName> GetHeroNamesAndIds();
    }

    public class SuperHeroesRepository : ISuperHeroesRepository
    {
        public static List<SuperHero> SuperHeroes = new List<SuperHero>();

        private static SuperHeroesRepository instance;

        // private SuperHeroesRepository() { }
        public SuperHeroesRepository()
        {
            SuperHeroesRepository.InitializaList();
        }

        public static SuperHeroesRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SuperHeroesRepository();
                    SuperHeroesRepository.InitializaList();
                }
                return instance;
            }
        }



        public static void InitializaList()
        {
            var superHero = new SuperHero()
            {
                Id = 2,
                Gender = "Female",
                HasCape = true,
                FirstName = "Bruce",
                LastName = "Waine",
                Nickname = "Batman",
                Powers = "Fight Evil",
                Country = "GB"
            };

            var superHero2 = new SuperHero()
            {
                Id = 3,
                Gender = "Male",
                HasCape = false,
                Nickname = "Captain America",
                FirstName = "Steve",
                LastName = "Rogers",
                Powers = "Fights",
                Country = "USA"
            };


            var superHero3 = new SuperHero()
            {
                Id = 1,
                Gender = "Male",
                HasCape = false,
                Nickname = "Superman",
                FirstName = "Clark",
                LastName = "Kent",
                Powers = "Fights",
                Country = "USA"
            };

            var superHero4 = new SuperHero()
            {
                Id = 4,
                Gender = "Male",
                HasCape = false,
                Nickname = "Catwoman",
                FirstName = "Clark",
                LastName = "Kent",
                Powers = "Fights",
                Country = "USA"
            };
            SuperHeroes.Add(superHero);
            SuperHeroes.Add(superHero2);
            SuperHeroes.Add(superHero3);
            SuperHeroes.Add(superHero4);
        }

        public SuperHero Add(SuperHero superHero)
        {
            superHero.Id = SuperHeroes.Max(x => x.Id) + 1;
            SuperHeroes.Add(superHero);
            return superHero;
        }


        public List<SuperHero> GetAll(Filter filter)
        {
            return SuperHeroes.Filter(filter).ToList();
        }
        public SuperHero GetById(int id)
        {
            return SuperHeroes.FirstOrDefault(x => x.Id == id);
        }

        public SuperHero Update(SuperHero superHeroToUpdate)
        {
            return new SuperHero();
        }

        public SuperHero Delete(SuperHero superHeroToDelete)
        {
            SuperHeroes.Remove(superHeroToDelete);
            return new SuperHero();
        }


        public List<HeroName> GetHeroNamesAndIds()
        {
            return SuperHeroes.Select(x => new HeroName() { Id = x.Id, Nickname = x.Nickname }).ToList();
        }

        public bool IsUnique(SuperHero heroToAdd)
        {
            return !SuperHeroes.Exists(x => String.Equals(x.Nickname, heroToAdd.Nickname, StringComparison.CurrentCultureIgnoreCase)); ;
        }
    }
}
