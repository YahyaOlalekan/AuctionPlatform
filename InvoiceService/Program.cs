using FluentValidation;
using InvoiceService.Application.Commands;
using InvoiceService.Application.UnitOfWork;
using InvoiceService.Infrastructure.Data;
using InvoiceService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateInvoiceCommand>());

//builder.Services.AddValidatorsFromAssemblyContaining<CreateInvoiceCommand.CommandValidator>();
//builder.Services.AddValidatorsFromAssemblyContaining<UpdateAuctionRoomCommand.CommandValidator>();

//builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddSingleton<BidEventPublisher>();

builder.Services.AddDbContext<InvoiceContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Invoice")));


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
