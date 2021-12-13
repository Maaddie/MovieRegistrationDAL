using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRegistrationDAL.Models;
using System.Diagnostics;
using MySqlConnector;
using Dapper;
using System.ComponentModel.DataAnnotations;

namespace MovieRegistrationDAL.Controllers
{
    public class MovieController : Controller
    {
        public MovieDAL MovieDb = new MovieDAL();
        public IActionResult Index()
        {
            List<Movie> movie = MovieDb.GetMovies();

            return View(movie);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Movie m)
        {
            if (ModelState.IsValid)
            {
                MovieDb.RegisterMovie(m);
                return RedirectToAction("Index", "Movie"); //pass action and controller
            }
            else
            {
                ViewBag.ErrorMessage  = "Movie registration is not valid. Please try again.";
                return View(m);
                
                
            }
        }

        //[HttpPost]
        public IActionResult Search()
        {
            //List<Movie> mList = MovieDb.SearchMoviesByTitle(title);
            return View();
        }

        //[HttpPost]
        public IActionResult SearchResultTitle(string title)
        {
            List<Movie> mList = MovieDb.SearchMoviesByTitle(title);
            return View("SearchResult",mList);
        }

        public IActionResult SearchResultGenre(string genre)
        {
            List<Movie> mList = MovieDb.SearchMoviesByGenre(genre);
            return View("SearchResult",mList);
        }

        //[HttpPost]
        //public IActionResult SearchByTitle(string SearchTitle)
        //{

        //}

        public IActionResult Edit(int id)
        {
            Movie m = MovieDb.GetMovie(id);
            return View(m);
        }

        public IActionResult Delete(int id)
        {
            Movie m = MovieDb.GetMovie(id);
            return View(m);
            //if (ModelState.IsValid)
            //{
            //    MovieDb.DeleteMovie(id);
            //    return View(id);
            //}
            //else
            //{
            //    return View();
            //}
        }

        public IActionResult DeleteFromDb(int id)
        {
            MovieDb.DeleteMovie(id);
            return RedirectToAction("index", "home");
        }


        [HttpPost]
        public IActionResult Edit(Movie m)
        {
            if (ModelState.IsValid)
            {
                MovieDb.EditMovie(m);
                return View(m);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            Movie m = MovieDb.GetMovie(id);
            return View(m);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
