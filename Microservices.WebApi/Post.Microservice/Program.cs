using MassTransit;
using MassTransit.AspNetCoreIntegration;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Post.Microservice.Context;
using System;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        //config.UseHealthCheck(provider);
        config.Host(new Uri("rabbitmq://localhost:15672"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    }));
});
//builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Post.Microservice", Version = "v1" });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IApplicationContext, ApplicationContext>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Post.Microservice v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
