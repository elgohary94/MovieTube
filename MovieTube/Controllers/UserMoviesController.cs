using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTube.DTOs;

namespace MovieTube.Controllers
{

    public class UserMoviesController : Controller
    {
        private readonly IUserMovieRepository _userMovieRepository;

        private readonly IMapper _Mapper;
        

        public UserMoviesController(IUserMovieRepository userMovieRepository,IMapper mapper)
        {
            _userMovieRepository = userMovieRepository;
            _Mapper = mapper;
        }




        public async Task<IActionResult> ViewUserMovies()
        {
            var AllMovies = await _userMovieRepository.GetAllMovies();
            return View(AllMovies);
        }

        public async Task<IActionResult> AddMovie()
        {
            TempData["genre"] = await _userMovieRepository.GetAllGenre();
            var movie = new MovieDTO();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie([FromForm] MovieDTO movie,IFormFile PosterFile)
        {
            //var newmovie = new Movie();
            if (ModelState.IsValid)
            {
            var result = _Mapper.Map<Movie>(movie);
            var genre = await _userMovieRepository.GetGenreById(movie.GenreId);
            movie.Genre = genre;
                if (PosterFile != null && PosterFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await PosterFile.CopyToAsync(stream);
                        result.Poster = stream.ToArray();
                        
                    }

                    //var destinationObject = _mapper.Map<DestinationObject>(sourceObject);
                }
                //newmovie.Genre = movie.Genre;
                await _userMovieRepository.CreateMovie(result);
                return RedirectToAction("ViewUserMovies");
            }
            TempData["genre"] = await _userMovieRepository.GetAllGenre();
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