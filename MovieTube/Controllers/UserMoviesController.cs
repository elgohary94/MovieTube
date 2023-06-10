using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTube.ViewModels;

namespace MovieTube.Controllers
{

    public class UserMoviesController : Controller
    {
        private readonly IUserMovieRepository _userMovieRepository;

        private readonly IMapper _Mapper;

        private readonly ILogger<UserMoviesController> _logger;


        public UserMoviesController(IUserMovieRepository userMovieRepository, IMapper mapper, ILogger<UserMoviesController> logger)
        {
            _userMovieRepository = userMovieRepository;
            _Mapper = mapper;
            _logger = logger;
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
                var Result = await _userMovieRepository.GetMovieByIdAsync(id);
                TempData["Post"] = Result.Poster;
                var MappedMovie = _Mapper.Map<MovieWithoutPosterViewModel>(Result);
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
            var movie = new MovieWithoutPosterViewModel();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie([FromForm] MovieWithoutPosterViewModel movie, IFormFile PosterFile)
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
                return RedirectToAction("ViewAllMovies");
            }
            TempData["genre"] = await _userMovieRepository.GetAllGenreAsync();
            return View("AddMovie", movie);
        }

        //TODO => Create View to update the movie
        [HttpGet]
        public async Task<IActionResult> EditMovieInfo(int ID)
        {
            var movie = await _userMovieRepository.GetMovieByIdAsync(ID);
            var MovieWithPosterViewModel = _Mapper.Map<MovieIncludingPosterViewModel>(movie);
            //TempData["Genre"] = await _userMovieRepository.GetAllGenreAsync();
            ViewBag.Poster = MovieWithPosterViewModel.Poster;
            
            return View(MovieWithPosterViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovie([FromRoute]int ID, [FromForm] MovieWithoutPosterViewModel movie, IFormFile? PosterFile)
        {
            var MappedMovie = _Mapper.Map<Movie>(movie);
            if(PosterFile is null)
            {
                try
                {

                    var DBMovie = await _userMovieRepository.GetMovieByIdAsync(ID);
                    if(DBMovie is not null)
                    {
                        MappedMovie.Poster = DBMovie.Poster;
                    }

                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error", "Home");

                }
            }
            if (ModelState.IsValid)
            {
                if (PosterFile is not null && PosterFile.Length > 0)
                {
                    using (var Stream = new MemoryStream())
                    {
                        await PosterFile.CopyToAsync(Stream);
                        MappedMovie.Poster= Stream.ToArray();
                    }
                }
                    await _userMovieRepository.UpdateMovieAsync(ID, MappedMovie);
                    return RedirectToAction("WatchMovie", ID);
            }
            else
            {
                ModelState.AddModelError("Error","Make Sure To Fill All The Fields!");
                //TempData["Poster"] = movie.Poster;
                return RedirectToAction("EditMovieInfo");   
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