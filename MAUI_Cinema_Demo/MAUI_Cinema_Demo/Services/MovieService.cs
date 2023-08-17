using MAUI_Cinema_Demo.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MAUI_Cinema_Demo.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _client;

        public MovieService()
        {
            _client = new HttpClient();
        }

        public async Task<List<Movies>> GetMoviesAsync()
        {
            int pageNumber = 1;
            int pagesMax = 10;
            List<Movies> list = new List<Movies>();

            if (await ConnectToWebAPI())
            {
                while (pageNumber < pagesMax)
                {
                    Root listeOfPopularMovies = await GetListOfPopularMovies(pageNumber);
                    pageNumber++;
                    list.AddRange(listeOfPopularMovies.results);
                }
            }

            return list;
        }

        private async Task<Root> GetListOfPopularMovies(int pageNumber)
        {
            pageNumber = 1;
            Uri adressToCall = new Uri($"{_client.BaseAddress}movie/popular?language=fr-FR&page={pageNumber}");

            var listeOfPopularMovies = await _client.GetStringAsync(adressToCall);
            return JsonConvert.DeserializeObject<Root>(listeOfPopularMovies);
        }

        private async Task<bool> ConnectToWebAPI()
        {
            _client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0YmQ3OTlhYWUwNmFhMjdjOTE2ZjRmZjIyNDkzY2ViMSIsInN1YiI6IjVkMTYwMmE4NTBmN2NhMDAxNTE1Yzg2ZiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.zEcC7Hg2DRaG425SBxTfORAg5jlTcR8QgL7h7nWyHXM");
            Uri adressToCall = new Uri($"{_client.BaseAddress}authentication");

            // Execute
            HttpResponseMessage response = await _client.GetAsync(adressToCall);

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
