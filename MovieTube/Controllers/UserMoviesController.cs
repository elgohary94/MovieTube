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
        

        public UserMoviesController(IUserMovieRepository userMovieRepository, IMapper mapper)
        {
            _userMovieRepository = userMovieRepository;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllMovies()
        {
            var AllMovies = await _userMovieRepository.GetAllMoviesAsync();
            return View(AllMovies);
        }

        [HttpGet]
        public async Task<IActionResult> WatchMovie(int id) 
        {
            try
            {
                var Result = await _userMovieRepository.FindMovieByIdAsync(id);
                TempData["Poster"] = Result.Poster;
                var MappedMovie = _Mapper.Map<MovieDTO>(Result);
                return View(MappedMovie);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            
            }


        }

        [HttpGet]
        public async Task<IActionResult> AddMovie()
        {
            TempData["genre"] = await _userMovieRepository.GetAllGenreAsync();
            var movie = new MovieDTO();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie([FromForm] MovieDTO movie, IFormFile PosterFile)
        {
            if (ModelState.IsValid)
            {
            var result = _Mapper.Map<Movie>(movie);
            var genre = await _userMovieRepository.GetGenreByIdAsync(movie.GenreId);
            movie.Genre = genre;
                if (PosterFile != null && PosterFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await PosterFile.CopyToAsync(stream);
                        result.Poster = stream.ToArray();
                        
                    }
                }
                await _userMovieRepository.CreateMovieAsync(result);
                return RedirectToAction("ViewUserMovies");
            }
            TempData["genre"] = await _userMovieRepository.GetAllGenreAsync();
            return View("AddMovie", movie);
        }

        //TODO => Create View to update the movie
        public async Task<IActionResult> UpdateMovieView(int id)
        {
            var movie = await _userMovieRepository.FindMovieByIdAsync(id);
            TempData["Poster"] = _userMovieRepository.GetGenreByIdAsync(movie.GenreId);
            var moviedto = _Mapper.Map<MovieDTO>(movie);
            return View(moviedto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] MovieDTO movie, IFormFile PosterFile)
        {
            if (ModelState.IsValid)
            {
                var MappedMovie = _Mapper.Map<Movie>(movie);
                if (PosterFile is not null && PosterFile.Length > 0)
                {
                    using (var Stream = new MemoryStream())
                    {
                        await PosterFile.CopyToAsync(Stream);
                        MappedMovie.Poster= Stream.ToArray();
                    }
                }
                    await _userMovieRepository.UpdateMovieAsync(id, MappedMovie);
                    return RedirectToAction("WatchMovie", id);
            }
            else
            {
                ModelState.AddModelError("Error","Make Sure To Fill All The Fields!");
                return RedirectToAction("UpdateMovieView");   
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _userMovieRepository.DeleteMovieAsync(id);
            return RedirectToAction("ViewAllMovies");
        }

       
    }
}