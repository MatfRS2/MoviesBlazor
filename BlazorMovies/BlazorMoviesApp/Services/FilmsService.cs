
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

namespace BlazorMoviesApp.Services
{
    public class FilmsService : IFilmsService
    {
        private readonly HttpClient _httpClient;

        public FilmsService(HttpClient httpClient)
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
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            //request.SetBrowserRequestMode(BrowserRequestMode.NoCors);
            //request.SetBrowserRequestCache(BrowserRequestCache.NoStore);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var films = await JsonSerializer.DeserializeAsync<IEnumerable<Film>>(responseStream);
                return films.ToList();
            }
            else
            {
                throw new Exception(response.Content.ToString());
            }
        }

    }
}

