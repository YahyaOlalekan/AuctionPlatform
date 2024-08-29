

using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RoomService.Application.Commands;
using RoomService.Application.UnitOfWork;
using RoomService.Infrastructure.Data;
using RoomService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateAuctionRoomCommand>());
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateAuctionRoomCommand>());
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DeleteAuctionRoomCommand>());

builder.Services.AddValidatorsFromAssemblyContaining<CreateAuctionRoomCommand.CommandValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuctionRoomCommand.CommandValidator>();

//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IAuctionRoomRepository, AuctionRoomRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AuctionContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("AuctionDb")));

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    // No consumers to add here since RoomService is only publishing the event
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", h =>
        {
            h.Username("guest");  
            h.Password("guest");  
        });
    });
});

builder.Services.AddMassTransitHostedService();


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
