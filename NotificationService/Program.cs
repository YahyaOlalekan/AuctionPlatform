using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Consumers;
using NotificationService.Application.UnitOfWork;
using NotificationService.Infrastructure.Data;
using NotificationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<PlaceBidCommand>());

//builder.Services.AddValidatorsFromAssemblyContaining<PlaceBidCommand.CommandValidator>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<NotificationContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Notifications")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<NewHighestBidConsumer>();

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
//    x.AddConsumer<NewHighestBidConsumer>();

//    x.UsingRabbitMq((context, cfg) =>
//    {
//        cfg.Host("rabbitmq", h =>
//        {
//            h.Username("guest");
//            h.Password("guest");
//        });

//        // Configure the receive endpoint and associate it with the consumer
//        cfg.ReceiveEndpoint("new-highest-bid-queue", e =>
//        {
//            e.ConfigureConsumer<NewHighestBidConsumer>(context);
//        });
//    });
//});

// This service runs MassTransit as a hosted service
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
