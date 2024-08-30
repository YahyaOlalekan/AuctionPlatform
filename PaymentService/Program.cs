using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Commands;
using PaymentService.Application.UnitOfWork;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProcessPaymentCommand>());

//builder.Services.AddValidatorsFromAssemblyContaining<ProcessPaymentCommand.CommandValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuctionRoomCommand.CommandValidator>();

//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddSingleton<BidEventPublisher>();

builder.Services.AddDbContext<PaymentContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Payments")));


builder.Services.AddMassTransit(x =>
{
   // x.AddConsumersFromNamespaceContaining<NewHighestBidConsumer>();

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
