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

            modelBuilder.Entity<FilmPaket>().HasKey(fp => new { fp.FilmId, fp.PaketId });
            modelBuilder.Entity<FilmPaket>()
                    .Property(z => z.FilmId)
                    .ValueGeneratedNever()
                    .IsRequired();
            modelBuilder.Entity<FilmPaket>()
                    .Property(z => z.PaketId)
                    .ValueGeneratedNever()
                    .IsRequired();

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
        }
    }
}
