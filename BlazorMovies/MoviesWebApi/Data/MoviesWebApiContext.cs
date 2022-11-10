using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Models;

namespace MoviesWebApi.Data
{
    public class MoviesWebApiContext : DbContext
    {
        public MoviesWebApiContext (DbContextOptions<MoviesWebApiContext> options)
            : base(options)
        {
        }

        public DbSet<Zanr> Zanr { get; set; } = default!;
        public DbSet<Film> Film { get; set; } = default!;
    }
}
