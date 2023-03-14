using MovieService.Business;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;
using MovieService.Domain;
using MovieService.Domain.Actor;
using MovieService.Infrastructure;
using MovieService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPropertyService<Actor, ActorDto>, PropertyActorService>();

builder.Services.AddScoped<IPropertyRepository<Actor>, ActorRepository>();

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
