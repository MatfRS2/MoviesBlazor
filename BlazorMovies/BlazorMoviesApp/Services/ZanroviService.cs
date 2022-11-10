
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
using System.Text;

namespace BlazorMoviesApp.Services
{
    public class ZanroviService : IZanroviService
    {
        private readonly HttpClient _httpClient;

        public ZanroviService(HttpClient httpClient)
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

        public async Task<List<Zanr>> GetZanroviAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Zanrovi");
            request.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                JsonSerializerOptions options = new(JsonSerializerDefaults.Web)
                {
                    WriteIndented = true
                };
                var zanrovi = await JsonSerializer.DeserializeAsync(responseStream,
                    typeof(List<Zanr>), options);
                if (zanrovi != null)
                    return (List<Zanr>)zanrovi;
                return new List<Zanr>();
            }
            else
            {
                throw new Exception(this.GetType().Name + "::GetFilmsAsync():" + response.Content.ToString());
            }
        }
   
    }
}

