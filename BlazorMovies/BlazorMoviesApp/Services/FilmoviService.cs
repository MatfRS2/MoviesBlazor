
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

using global::BlazorMoviesApp.Services;
using System.Numerics;
using global::BlazorMoviesApp.Models;
using System.Text.Json;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using BlazorMoviesApp.ViewModels;

namespace BlazorMoviesApp.Services
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
                HeaderNames.Accept, "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, "HttpRequestsSample");
        }

        public async Task<List<Film>> GetFilmsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Films");
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
                return (List<Film>) films;
            }
            else
            {
                throw new Exception(this.GetType().Name + "::GetFilmsAsync():" + response.Content.ToString());
            }
        }
        public async Task<int> Add(FilmAddDTO item)
        {
            item.Id = 0;
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Films");
            request.Headers.Add("Accept", "application/json");
            request.Content = JsonContent.Create(item);

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



    }
}

