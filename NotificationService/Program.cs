using Microsoft.EntityFrameworkCore;
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
//builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuctionRoomCommand.CommandValidator>();

//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddSingleton<BidEventPublisher>();

builder.Services.AddDbContext<NotificationContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Notifications")));


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
