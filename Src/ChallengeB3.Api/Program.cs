using ChallengeB3.Api.Producer;
using ChallengeB3.Domain.Extesions;
using ChallengeB3.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.WebHooks;
using ProtoBuf.Meta;
using ChallengeB3.Infra.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

// Add services to the container.
builder.Services
    .AddCors()
    .AddControllers()
    .AddWebHooks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppConfiguration(config);
builder.Services.AddSingleton<IQueueProducer, QueueProducer>();

NativeInjectorBootStrapper.RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => { 
    options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader(); 
});

app.UseAuthorization();

app.MapControllers();

app.Run();
