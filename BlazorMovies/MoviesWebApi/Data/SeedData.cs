using Microsoft.EntityFrameworkCore;
using MoviesWebApi.Models;

namespace MoviesWebApi.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesWebApiContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MoviesWebApiContext>>()))
            {
                // Look for any Films.
                if (context.Film.Any())
                {
                    return;   // DB has been seeded
                }
                Zanr[] zanrovi = {
                    new Zanr{
                        ZanrId = 1,
                        Naziv = "Romantic Comedy",
                    },
                    new Zanr{
                        ZanrId = 2,
                        Naziv = "Comedy",
                    },
                    new Zanr{
                        ZanrId = 3,
                        Naziv = "Western",
                    }

                };
                context.Zanr.AddRange(zanrovi);
                context.Film.AddRange(
                    new Film
                    {
                        Naslov = "When Harry Met Sally",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1989-2-12"),
                        Zanr = zanrovi[0],
                        Ulozeno = 7.99M
                    },

                    new Film
                    {
                        Naslov = "Ghostbusters ",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1984-3-13"),
                        Zanr = zanrovi[1],
                        Ulozeno = 8.99M
                    },

                    new Film
                    {
                        Naslov = "Ghostbusters 2",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1986-2-23"),
                        Zanr = zanrovi[1],
                        Ulozeno = 9.99M
                    },

                    new Film
                    {
                        Naslov = "Rio Bravo",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1959-4-15"),
                        Zanr = zanrovi[2],
                        Ulozeno = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
