using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorMoviesApp.Models;

namespace BlazorMoviesApp.Services
{
    public interface IFilmsService
    {
        Task<List<Film>> GetFilmsAsync();
    }
}
