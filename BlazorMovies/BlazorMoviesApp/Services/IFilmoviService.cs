using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using BlazorMoviesApp.Models;
using BlazorMoviesApp.ViewModels;

namespace BlazorMoviesApp.Services
{
    public interface IFilmoviService
    {
        Task<List<Film>> GetFilmsAsync();
        Task<int> Add(FilmAddDTO item);
        Task<Film> GetFilmAsync(int id);
        Task<int> Update(FilmUpdateDTO item);
    }
}
