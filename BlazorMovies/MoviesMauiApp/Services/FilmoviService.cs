
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

using global::MoviesMauiApp.Services;
using System.Numerics;
using global::MoviesMauiApp.Models;
using System.Text.Json;
using MoviesMauiApp.ViewModels;
using System.Text;

namespace MoviesMauiApp.Services
{
    public class FilmoviService : IFilmoviService
    {
        private readonly HttpClient _httpClient;

        public FilmoviService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(
                nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7250");
            // using Microsoft.Net.Http.Headers;
            _httpClient.DefaultRequestHeaders.Add(
                "Accept", "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(
                "UserAgent", "HttpRequestsSample");
        }

        public async Task<List<Film>> GetFilmsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Filmovi");
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
                {
                    WriteIndented = true
                };
                var films = await JsonSerializer.DeserializeAsync(responseStream,
                    typeof(List<Film>), options);
                if(films!=null)
                    return (List<Film>)films;
                return new List<Film>();
            }
            else
            {
                throw new Exception(this.GetType().Name + "::GetFilmsAsync():" + response.Content.ToString());
            }
        }
        public async Task<int> Add(FilmAddDTO item)
        {
            item.Id = 0;
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Filmovi");
            request.Headers.Add("Accept", "application/json");
            request.Content = JsonContent.Create(new Film()
            {
                Id = item.Id,
                Naslov = item.Naslov,
                Zanr = item.Zanr,
                DatumPocetkaPrikazivanja = item.DatumPocetkaPrikazivanja,
                Ulozeno = item.Ulozeno,
            });

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                throw new Exception(this.GetType().Name + "::Add():" + response.Content.ToString());
            }
        }

        public async Task<Film> GetFilmAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Filmovi/" + id.ToString());
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
                {
                    WriteIndented = true
                };
                var film = await JsonSerializer.DeserializeAsync(responseStream,
                    typeof(Film), options);
                if (film != null)
                    return (Film)film;
                else
                    return new Film() {Id = -1};
            }
            else
            {
                throw new Exception(this.GetType().Name + "::GetFilmAsync():" + response.Content.ToString());
            }
        }

        public async Task<int> Update(FilmUpdateDTO item)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Filmovi/" + item.Id.ToString());
            request.Headers.Add("Accept", "application/json");
            request.Content = new StringContent(JsonSerializer.Serialize(
                new Film()
                {
                    Id = item.Id,
                    Naslov = item.Naslov,
                    Zanr = item.Zanr,
                    DatumPocetkaPrikazivanja = item.DatumPocetkaPrikazivanja,
                    Ulozeno = item.Ulozeno
                }),
                Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                throw new Exception(this.GetType().Name + "::Update():" + response.Content.ToString());
            }
        }

        public async Task<int> Delete(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Filmovi/" + id.ToString());
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                throw new Exception(this.GetType().Name + "::Delete():" + response.Content.ToString());
            }
        }
    }
}

