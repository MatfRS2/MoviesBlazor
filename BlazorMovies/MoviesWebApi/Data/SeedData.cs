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

                context.Film.AddRange(
                    new Film
                    {
                        Naslov = "When Harry Met Sally",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1989-2-12"),
                        Zanr = "Romantic Comedy",
                        Ulozeno = 7.99M
                    },

                    new Film
                    {
                        Naslov = "Ghostbusters ",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1984-3-13"),
                        Zanr = "Comedy",
                        Ulozeno = 8.99M
                    },

                    new Film
                    {
                        Naslov = "Ghostbusters 2",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1986-2-23"),
                        Zanr = "Comedy",
                        Ulozeno = 9.99M
                    },

                    new Film
                    {
                        Naslov = "Rio Bravo",
                        DatumPocetkaPrikazivanja = DateTime.Parse("1959-4-15"),
                        Zanr = "Western",
                        Ulozeno = 3.99M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
