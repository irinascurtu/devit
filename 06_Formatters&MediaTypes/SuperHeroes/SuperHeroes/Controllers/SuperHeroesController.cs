using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;
using SuperHeroes.Domain;
using SuperHeroese.Data;

namespace SuperHeroes.Controllers
{
    //[Produces("application/json")]
    [Route("api/SuperHeroes")]
    public class SuperHeroesController : Controller
    {
        private SuperHeroesRepository repo;
       // private readonly ILinksService linksService;
        public SuperHeroesController()
        {
            repo = SuperHeroesRepository.Instance;
         //   this.linksService = linksService;
        }

        // GET api/superheroes
        [HttpGet(Name = "GetAllModelsRoute")]

        public async Task<IActionResult> Get([FromQuery]Filter filter)
        {
            List<SuperHero> mySuperHeroes = repo.GetAll(filter).ToList();

            if (!mySuperHeroes.Any())
            {
                return NoContent();
            }

            //var result = new ItemsLinkContainer<SuperHero>()
            //{
            //    Items = mySuperHeroes
            //};

            //await linksService.AddLinksAsync(result);
            return Ok(mySuperHeroes);
        }

        //GET api/superheroes/5
        [HttpGet("{id}", Name = "SuperHeroGet"),]
        [ProducesResponseType(405)]
        public IActionResult Get(int id)
        {
            var hero = repo.GetById(id);
            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }


        // GET api/superheroes/5
        //[HttpGet("{id}", Name = "SuperHeroGet")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //public SuperHero Get(int id)
        //{
        //    var hero = repo.GetById(id);
        //    if (hero == null)
        //    {
        //        return null;
        //    }

        //    return null;
        //}



        // POST api/superheroes
        [HttpPost]
        public IActionResult Post([FromBody] SuperHero superhero)
        {
            if (ModelState.IsValid)
            {
                if (repo.IsUnique(superhero))
                {
                    var newSuperhero = repo.Add(superhero);
                    if (newSuperhero != null)
                    {
                        var newUri = this.Url.Link("SuperHeroGet", new { id = newSuperhero.Id });
                        return Created(newUri, newSuperhero);
                    }
                }

                return StatusCode((int)HttpStatusCode.Conflict, "The  superhero you try to insert is not unique");



            }
            return BadRequest(ModelState);
        }

       // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SuperHero superhero)
        {
            var hero = repo.GetById(id);
            if (hero == null)
            {
                return NotFound();
            }
            else
            {
                TryUpdateModelAsync(hero);
            }

            return Ok(hero);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Forbid("you are not alowed to delete this resource");
        }
    }

}