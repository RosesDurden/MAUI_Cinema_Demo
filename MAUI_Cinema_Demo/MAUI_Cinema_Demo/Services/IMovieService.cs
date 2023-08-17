using MAUI_Cinema_Demo.Models;

namespace MAUI_Cinema_Demo.Services
{
    public interface IMovieService
    {
        public Task<List<Movies>> GetMoviesAsync();
    }
}
