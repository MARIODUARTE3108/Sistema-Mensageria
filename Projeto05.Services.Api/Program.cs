using Projeto05.Services.Api.Configurations;
using Projeto05.Services.Api.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlServerConfiguration.Add(builder);
RabbitMQConfiguration.Add(builder);
MailConfiguration.Add(builder);
LogConfiguration.Add(builder);

//registrando os consumidores do RabbitMQ
builder.Services.AddHostedService<EmailServiceConsumer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
