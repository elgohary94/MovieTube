using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MovieTube.Controllers
{

    public class UserMoviesController : Controller
    {
        private readonly IUserMovieRepository _userMovieRepository;

        public UserMoviesController(IUserMovieRepository userMovieRepository)
        {
            _userMovieRepository = userMovieRepository;
        }

        public async Task<IActionResult> ViewUserMovies()
        {
            var AllMovies = await _userMovieRepository.GetAllMovies();
            return View(AllMovies);
        }

        public IActionResult hit()
        {
            return Content("hii");
        }

        public IActionResult AddMovie()
        {
            var movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie(Movie Movie)
        {
            
                return RedirectToAction("ViewUserMovies");

        }

        public async Task<IActionResult> WatchMovie(int id) 
        {
   //posibale exception if result is null => will make an error page and handle the exception in view
            var Result = await _userMovieRepository.FindMovieById(id);

            return View(Result);
        }

       
    }
}