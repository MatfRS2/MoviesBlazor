
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
using AutoMapper;

namespace BlazorMoviesApp.Services
{
    public class FilmoviService : IFilmoviService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public FilmoviService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7250");
            // using Microsoft.Net.Http.Headers;
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, "HttpRequestsSample");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<FilmGetDto>> GetFilmsAsync()
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
                    typeof(List<FilmGetDto>), options);
                if(films!=null)
                    return (List<FilmGetDto>)films;
                return new List<FilmGetDto>();
            }
            else
            {
                throw new Exception(this.GetType().Name + "::GetFilmsAsync():" + response.Content.ToString());
            }
        }
        public async Task<int> Add(FilmAddDto item)
        {
            item.FilmId = 0;
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Films");
            request.Headers.Add("Accept", "application/json");
            request.Content = JsonContent.Create(_mapper.Map<Film>(item));
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

        public async Task<FilmGetDto> GetFilmAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Films/" + id.ToString());
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
                    typeof(FilmGetDto), options);
                if (film != null)
                    return (FilmGetDto)film;
                else
                    return new FilmGetDto() {FilmId = -1};
            }
            else
            {
                throw new Exception(this.GetType().Name + "::GetFilmAsync():" + response.Content.ToString());
            }
        }

        public async Task<int> Update(FilmUpdateDto item)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/Films/" + item.FilmId.ToString());
            request.Headers.Add("Accept", "application/json");
            Film film = _mapper.Map<Film>(item);
            request.Content = new StringContent(JsonSerializer.Serialize(film),Encoding.UTF8, "application/json");
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
            var request = new HttpRequestMessage(HttpMethod.Delete, "/api/Films/" + id.ToString());
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

