using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using MoviesMauiApp.Models;
using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Services
{
    public interface IFilmoviService
    {
        Task<List<Film>> GetFilmsAsync();
        Task<int> Add(FilmAddDTO item);
        Task<Film> GetFilmAsync(int id);
        Task<int> Update(FilmUpdateDTO item);
        Task<int> Delete(int id);
    }
}
