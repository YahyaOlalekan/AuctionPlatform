using FluentValidation;
using InvoiceService.Application.Commands;
using InvoiceService.Application.UnitOfWork;
using InvoiceService.Infrastructure.Data;
using InvoiceService.Infrastructure.Repositories;
using MassTransit;
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

builder.Services.AddMassTransit(x =>
{
   // x.AddConsumersFromNamespaceContaining<StartAuctionConsumer>();

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
//    //x.AddConsumer<StartAuctionConsumer>();

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
//           // e.ConfigureConsumer<StartAuctionConsumer>(context);
//        });
//    });
//});

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
