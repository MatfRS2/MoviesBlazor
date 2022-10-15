using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorMoviesApp.Models;
using BlazorMoviesApp.ViewModels;

namespace BlazorMoviesApp.Services
{
    public interface IFilmsService
    {
        Task<List<Film>> GetFilmsAsync();
        Task<int> Add(FilmAddDTO item);
    }
}
