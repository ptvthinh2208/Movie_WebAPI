using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Actor;
using MovieService.Domain.Country;
using MovieService.Domain.Director;
using MovieService.Domain.Genre;
using MovieService.Domain.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.EF
{
    public class MovieContext : DbContext
    {
        public MovieContext()
        {
        }

        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ADMIN;Database=MovieServiceDB;User ID=sa;Password=passWord@123;MultipleActiveResultSets=True;TrustServerCertificate=True");
        }
        public virtual DbSet<Actor> Actor { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.ActorName).HasMaxLength(64);
            });
            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryName).HasMaxLength(64);
            });
            modelBuilder.Entity<Director>(entity =>
            {
                entity.Property(e => e.DirectorName).HasMaxLength(64);
            });
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreName).HasMaxLength(64);
            });
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieName).HasMaxLength(254);

                entity.Property(e => e.Title).HasMaxLength(254);

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.ActorId)
                    .HasConstraintName("FK_Movie_Actor");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Movie_Genre");
            });
        }


    }
}
