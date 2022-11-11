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
                if (context.FilmPaket.Any())
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
                Film[] filmovi = new Film[] {
                    new Film{
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
                };
                context.Film.AddRange( filmovi );
                Paket[] paketi = new Paket[] {
                    new Paket{ 
                        Naziv = "Standardni",
                        Opis = "Standardni paket (sa komedijama)",
                        DatumFormiranja = DateTime.Today
                    },
                    new Paket{
                        Naziv = "Promotivni",
                        Opis = "Promotivni paket (prošireni skup)",
                        DatumFormiranja = DateTime.Today
                    }

                };
                context.Paket.AddRange(paketi);
                FilmPaket[] filmPaketi = new FilmPaket[] {
                    new FilmPaket{
                        PaketId = 1,
                        Paket = paketi[0],
                        FilmId = 2,
                        Film = filmovi[1]
                    },
                    new FilmPaket{
                        PaketId = 1,
                        Paket = paketi[0],
                        FilmId = 3,
                        Film = filmovi[2]
                    },
                    new FilmPaket{
                        PaketId = 2,
                        Paket = paketi[1],
                        FilmId = 4,
                        Film = filmovi[3]
                    },
                    new FilmPaket{
                        PaketId = 2,
                        Paket = paketi[1],
                        FilmId = 1,
                        Film = filmovi[0]
                    }
                };
                context.FilmPaket.AddRange(filmPaketi);
                Korisnik[] korisnici = new Korisnik[] {
                    new Korisnik{
                        Ime = "Marko",
                        Prezime = "Marković",
                        Email = "marko_markovic@gmail.com",
                        Potroseno = 300
                    },
                    new Korisnik{
                        Ime = "Janko",
                        Prezime = "Janković",
                        Email = "janko_jankovic@gmail.com",
                        Potroseno = 10
                    },
                };
                context.Korisnik.AddRange(korisnici);
                Pretplata[] pretplate = new Pretplata[] {
                    new Pretplata
                    {
                        KorisnikId = 1,
                        Korisnik = korisnici[0],
                        DatumIsteka = new DateTime(2022, 12, 25),
                        Iznos = 10,
                        PaketId = 1,
                        Paket = paketi[0],
                        Status = PretplataStatus.Aktivirana
                    },
                    new Pretplata
                    {
                        KorisnikId = 2,
                        Korisnik = korisnici[1],
                        DatumIsteka = new DateTime(2023, 1, 15),
                        Iznos = 20,
                        PaketId = 2,
                        Paket = paketi[1],
                        Status = PretplataStatus.Aktivirana
                    }
                };
                context.Pretplata.AddRange(pretplate);
                context.SaveChanges();
            }
        }
    }
}
