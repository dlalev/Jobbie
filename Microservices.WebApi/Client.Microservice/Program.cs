using Client.Microservice.Consumer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PostConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
    {
        //cur.UseHealthCheck(provider);
        cur.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cur.ReceiveEndpoint("postQueue", oq =>
        {
            oq.PrefetchCount = 20;
            oq.UseMessageRetry(r => r.Interval(2, 100));
            oq.ConfigureConsumer<PostConsumer>(provider);
        });
    }));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
