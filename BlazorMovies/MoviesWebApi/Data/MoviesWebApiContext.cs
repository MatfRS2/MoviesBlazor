using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        public DbSet<Paket> Paket { get; set; } = default!;
        public DbSet<FilmPaket> FilmPaket { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // enititeti
            PodesiZanr(modelBuilder);
            PodesiFilm(modelBuilder);
            PodesiPaket(modelBuilder);
            PodesiFilmPaket(modelBuilder);
            PodesiPretplata(modelBuilder);
            PodesiKorisnik(modelBuilder);

            // relacije
            modelBuilder.Entity<Film>()
                    .HasOne(f => f.Zanr)
                    .WithMany(z => z.Filmovi)
                    .HasForeignKey(f => f.ZanrId);

            modelBuilder.Entity<Film>()
                    .HasMany(f => f.Paketi)
                    .WithMany(p => p.Filmovi)
                    .UsingEntity<FilmPaket>(
                        j => j
                            .HasOne(fp => fp.Paket)
                            .WithMany(p => p.FilmPaketi)
                            .HasForeignKey(fp => fp.PaketId),
                        j => j
                            .HasOne(fp => fp.Film)
                            .WithMany(f => f.FilmPaketi)
                            .HasForeignKey(fp => fp.FilmId));

            modelBuilder.Entity<Paket>()
                    .HasMany(p => p.Filmovi)
                    .WithMany(f => f.Paketi)
                    .UsingEntity<FilmPaket>(
                        j => j
                            .HasOne(fp => fp.Film)
                            .WithMany(f => f.FilmPaketi)
                            .HasForeignKey(fp => fp.FilmId),
                        j => j
                            .HasOne(fp => fp.Paket)
                            .WithMany(p => p.FilmPaketi)
                            .HasForeignKey(fp => fp.PaketId));

            modelBuilder.Entity<Paket>()
                    .HasMany(p => p.Korisnici)
                    .WithMany(k => k.Paketi)
                    .UsingEntity<Pretplata>(
                        j => j
                            .HasOne(pr => pr.Korisnik)
                            .WithMany(k => k.Pretplate)
                            .HasForeignKey(pr => pr.KorisnikId),
                        j => j
                            .HasOne(pr => pr.Paket)
                            .WithMany(p => p.Pretplate)
                            .HasForeignKey(pr => pr.PaketId));

            modelBuilder.Entity<Korisnik>()
                            .HasMany(k => k.Paketi)
                            .WithMany(p => p.Korisnici)
                            .UsingEntity<Pretplata>(
                                j => j
                                    .HasOne(pr => pr.Paket)
                                    .WithMany(k => k.Pretplate)
                                    .HasForeignKey(pr => pr.PaketId),
                                j => j
                                    .HasOne(pr => pr.Korisnik)
                                    .WithMany(p => p.Pretplate)
                                    .HasForeignKey(pr => pr.KorisnikId));
        }

        private static void PodesiKorisnik(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnik>().HasKey(x => x.KorisnikId);
            modelBuilder.Entity<Korisnik>()
                   .Property(x => x.KorisnikId)
                   .HasColumnName("Id")
                   .IsRequired();
            modelBuilder.Entity<Korisnik>()
                  .Property(x => x.Email)
                  .HasMaxLength(250)
                  .IsRequired();
            modelBuilder.Entity<Korisnik>()
                  .Property(x => x.Ime)
                  .HasMaxLength(150)
                  .IsRequired();
            modelBuilder.Entity<Korisnik>()
                  .Property(x => x.Prezime)
                  .HasMaxLength(150)
                  .IsRequired();
            modelBuilder.Entity<Korisnik>()
                   .Property(p => p.Potroseno)
                   .HasColumnType("money");
        }

        private static void PodesiPretplata(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pretplata>().HasKey(p => p.PretplataId);
            modelBuilder.Entity<Pretplata>()
                   .Property(z => z.PretplataId)
                   .HasColumnName("Id")
                   .IsRequired();
            modelBuilder.Entity<Pretplata>()
                    .Property(p => p.Status)
                    .HasConversion(
                        v => v.ToString(),
                        v => (PretplataStatus)Enum.Parse(typeof(PretplataStatus), v));
            modelBuilder.Entity<Pretplata>()
                   .Property(p => p.DatumIsteka)
                   .HasColumnType("date");
            modelBuilder.Entity<Pretplata>()
                   .Property(p => p.Iznos)
                   .HasColumnType("money");
        }

        private static void PodesiFilmPaket(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FilmPaket>().HasKey(fp => new { fp.FilmId, fp.PaketId });
            modelBuilder.Entity<FilmPaket>()
                    .Property(z => z.FilmId)
                    .ValueGeneratedNever()
                    .IsRequired();
            modelBuilder.Entity<FilmPaket>()
                    .Property(z => z.PaketId)
                    .ValueGeneratedNever()
                    .IsRequired();
        }

        private static void PodesiPaket(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paket>().HasKey(p => p.PaketId);
            modelBuilder.Entity<Paket>()
                    .Property(z => z.PaketId)
                    .HasColumnName("Id")
                    .IsRequired();
            modelBuilder.Entity<Paket>()
                  .Property(p => p.Naziv)
                  .HasMaxLength(250)
                  .IsRequired();
            modelBuilder.Entity<Paket>()
                  .Property(p => p.Opis)
                  .HasMaxLength(1000)
                  .IsRequired();
            modelBuilder.Entity<Paket>()
                   .Property(p => p.DatumFormiranja)
                   .HasColumnType("date");
        }

        private static void PodesiFilm(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>().HasKey(f => f.FilmId);
            modelBuilder.Entity<Film>()
                     .Property(f => f.FilmId)
                     .HasColumnName("Id")
                     .IsRequired();
            modelBuilder.Entity<Film>()
                   .Property(f => f.Naslov)
                   .HasMaxLength(250)
                   .IsRequired();
            modelBuilder.Entity<Film>()
                   .Property(f => f.DatumPocetkaPrikazivanja)
                   .HasColumnType("date");
            modelBuilder.Entity<Film>()
                   .Property(f => f.Ulozeno)
                   .HasColumnType("money");
        }

        private static void PodesiZanr(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zanr>().HasKey(z => z.ZanrId);
            modelBuilder.Entity<Zanr>()
                    .Property(z => z.ZanrId)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
            modelBuilder.Entity<Zanr>()
                   .Property(z => z.Naziv)
                   .HasMaxLength(250)
                   .IsRequired();
        }
    }
}
