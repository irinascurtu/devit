using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroes.Mvc.Models;

namespace SuperHeroes.Mvc.Controllers
{
    public class FavoritesController : Controller
    {

        // GET: Favorites/Create
        public ActionResult Create()
        {
            Favorite favoriteHero = new Favorite();

            return View(favoriteHero);
        }

    }
}