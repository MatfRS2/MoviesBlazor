using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using MoviesMauiApp.Models;
using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Services
{
    public interface IZanroviService
    {
        Task<List<Zanr>> GetZanroviAsync();
    }
}
