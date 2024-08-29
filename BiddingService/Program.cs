using BiddingService.Application.Commands;
using BiddingService.Application.Services.Events;
using BiddingService.Application.UnitOfWork;
using BiddingService.Infrastructure.Data;
using BiddingService.Infrastructure.Repositories;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PlaceBidCommand>());

builder.Services.AddValidatorsFromAssemblyContaining<PlaceBidCommand.CommandValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuctionRoomCommand.CommandValidator>();

//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<BidEventPublisher>();

builder.Services.AddDbContext<BiddingContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("BidsDb")));


builder.Services.AddMassTransit(x =>
{
    // Register consumers here
   // x.AddConsumer<AuctionCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", h =>
        {
            h.Username("user");  // Replace with your RabbitMQ username
            h.Password("password");  // Replace with your RabbitMQ password
        });

        // Configure the receive endpoint and associate it with the consumer
        cfg.ReceiveEndpoint("auction-created-queue", e =>
        {
           // e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });
    });
});

// This service runs MassTransit as a hosted service
builder.Services.AddMassTransitHostedService();


// RabbitMQ setup
//var factory = new ConnectionFactory() { HostName = configuration["RabbitMq:HostName"] };
//var connection = factory.CreateConnection();
//services.AddSingleton(connection);
//services.AddTransient<IEventPublisher, RabbitMqEventPublisher>();

//// Register the RabbitMQ Event Publisher
//services.AddSingleton<IEventPublisher>(provider =>
//    new RabbitMqEventPublisher("localhost", "AuctionEventsQueue"));



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







