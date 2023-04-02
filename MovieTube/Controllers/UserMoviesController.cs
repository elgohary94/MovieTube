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

        public async Task<IActionResult> AddMovie()
        {
            ViewBag.genre = await _userMovieRepository.GetAllGenre();
            var movie = new Movie();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie([FromForm] Movie movie,IFormFile posterFile)
        {
            
            
                if (posterFile != null && posterFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await posterFile.CopyToAsync(stream);
                        movie.Poster = stream.ToArray();
                    }
                }



                // Save the movie to the repository and redirect to the view movies action
                await _userMovieRepository.CreateMovie(movie);
                return RedirectToAction("ViewUserMovies");
            

            return View("AddMovie", movie);
        }


        public async Task<IActionResult> WatchMovie(int id) 
        {
   //posibale exception if result is null => will make an error page and handle the exception in view
            var Result = await _userMovieRepository.FindMovieById(id);

            return View(Result);
        }

       
    }
}