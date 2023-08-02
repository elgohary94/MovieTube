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

    public class MoviesController : Controller
    {
        private readonly IMovieRepository _MovieRepository;

        private readonly IMapper _Mapper;

        private readonly ILogger<MoviesController> _logger;


        public MoviesController(IMovieRepository MovieRepository, IMapper mapper, ILogger<MoviesController> logger)
        {
            _MovieRepository = MovieRepository;
            _Mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllMovies()
        {
            var AllMovies = await _MovieRepository.GetAllMoviesAsync();
            return View(AllMovies);
        }
 
        [HttpGet]
        public async Task<IActionResult> WatchMovie([FromRoute]int id)
        {
            try
            {
                var Result = await _MovieRepository.GetMovieByIdAsync(id);
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
            TempData["genre"] = await _MovieRepository.GetAllGenreAsync();
            var movie = new MovieWithoutPosterViewModel();
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewMovie([FromForm] MovieWithoutPosterViewModel movie, IFormFile PosterFile)
        {
            if (ModelState.IsValid)
            {
                var result = _Mapper.Map<Movie>(movie);
                var genre = await _MovieRepository.GetGenreByIdAsync(movie.GenreId);
                movie.Genre = genre;
                if (PosterFile != null && PosterFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await PosterFile.CopyToAsync(stream);
                        result.Poster = stream.ToArray();

                    }
                }
                await _MovieRepository.CreateMovieAsync(result);
                return RedirectToAction("ViewAllMovies");
            }
            TempData["genre"] = await _MovieRepository.GetAllGenreAsync();
            return View("AddMovie", movie);
        }

        //TODO => Create View to update the movie
        [HttpGet]
        public async Task<IActionResult> EditMovieInfo(int ID)
        {
            var movie = await _MovieRepository.GetMovieByIdAsync(ID);
            var MovieWithPosterViewModel = _Mapper.Map<MovieIncludingPosterViewModel>(movie);
            //TempData["Genre"] = await _MovieRepository.GetAllGenreAsync();
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

                    var DBMovie = await _MovieRepository.GetMovieByIdAsync(ID);
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
                    await _MovieRepository.UpdateMovieAsync(ID, MappedMovie);
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
            await _MovieRepository.DeleteMovieAsync(id);
            return RedirectToAction("ViewAllMovies");
        }

       
    }
}