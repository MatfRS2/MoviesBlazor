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
        Task<List<FilmGetDto>> GetFilmsAsync();
        Task<int> Add(FilmAddDto item);
        Task<FilmGetDto> GetFilmAsync(int id);
        Task<int> Update(FilmUpdateDto item);
        Task<int> Delete(int id);
    }
}
