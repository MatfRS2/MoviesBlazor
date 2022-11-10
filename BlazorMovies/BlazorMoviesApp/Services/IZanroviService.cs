using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using BlazorMoviesApp.Models;
using BlazorMoviesApp.ViewModels;

namespace BlazorMoviesApp.Services
{
    public interface IZanroviService
    {
        Task<List<Zanr>> GetZanroviAsync();
    }
}
