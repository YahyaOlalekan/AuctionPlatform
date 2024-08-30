

using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddValidatorsFromAssemblyContaining<CreateAuctionRoomCommand.CommandValidator>();

builder.Services.AddScoped<IAuctionRoomRepository, AuctionRoomRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AuctionContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Auction")));

builder.Services.AddMassTransit(x =>
{
    //x.AddConsumersFromNamespaceContaining<NewHighestBidConsumer>();

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
