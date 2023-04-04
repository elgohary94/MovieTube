using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTube.DTOs;

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

        public async Task<IActionResult> AddMovie()
        {
            TempData["genre"] = await _userMovieRepository.GetAllGenre();
            var movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie([FromForm] MovieDTO movie,IFormFile posterFile)
        {
            var newmovie = new Movie();
            if (ModelState.IsValid)
            {
                if (posterFile != null && posterFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await posterFile.CopyToAsync(stream);
                        newmovie.Poster = stream.ToArray();
                    }
                }
                newmovie.Genre = movie.Genre;
                await _userMovieRepository.CreateMovie(newmovie);
                return RedirectToAction("ViewUserMovies");
            }
            TempData["genre"] = await _userMovieRepository.GetAllGenre();
            return View("AddMovie", newmovie);
        }


        public async Task<IActionResult> WatchMovie(int id) 
        {
   //posibale exception if result is null => will make an error page and handle the exception in view
            var Result = await _userMovieRepository.FindMovieById(id);

            return View(Result);
        }

       
    }
}