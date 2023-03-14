using Microsoft.EntityFrameworkCore;
using MovieService.Business;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;
using MovieService.Domain;
using MovieService.Domain.Actor;
using MovieService.Domain.Country;
using MovieService.Domain.Director;
using MovieService.Domain.Genre;
using MovieService.Infrastructure;
using MovieService.Infrastructure.EF;
using MovieService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));

});
// Add services to the container.

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPropertyService<Actor, ActorDto>, PropertyActorService>();
builder.Services.AddScoped<IPropertyService<Country, CountryDto>, PropertyCountryService>();
builder.Services.AddScoped<IPropertyService<Director, DirectorDto>, PropertyDirectorService>();
builder.Services.AddScoped<IPropertyService<Genre, GenreDto>, PropertyGenreService>();


builder.Services.AddScoped<IPropertyRepository<Actor>, ActorRepository>();
builder.Services.AddScoped<IPropertyRepository<Country>, CountryRepository>();
builder.Services.AddScoped<IPropertyRepository<Director>, DirectorRepository>();
builder.Services.AddScoped<IPropertyRepository<Genre>, GenreRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
