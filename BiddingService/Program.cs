using BiddingService.Application.Commands;
using BiddingService.Application.Consumers;
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

builder.Services.AddScoped<IBidRepository, BidRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<BiddingContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("BidsDb")));


builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<StartAuctionConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

//builder.Services.AddMassTransit(x =>
//{
//   x.AddConsumer<StartAuctionConsumer>();

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("rabbitmq", h =>
//        {
//            h.Username("guest");  
//            h.Password("guest");  
//        });

//        // Configure the receive endpoint and associate it with the consumer
//        cfg.ReceiveEndpoint("start-auction-queue", e =>
//        {
//            e.ConfigureConsumer<StartAuctionConsumer>(context);
//        });
//    });
//});

//// This service runs MassTransit as a hosted service
//builder.Services.AddMassTransitHostedService();

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







