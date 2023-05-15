using Amazon.SQS;
using AutoMapper;
using FluentValidation.AspNetCore;
using Users.Api.Database;
using Users.Api.Mapping;
using Users.Api.Messaging;
using Users.Api.Repositories;
using Users.Api.Services;
//using Users.Api.Services;
using Users.Api.Validation;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(x =>
{
    x.RegisterValidatorsFromAssemblyContaining<Program>();
    x.DisableDataAnnotationsValidation = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new SqliteConnectionFactory(config.GetValue<string>("Database:ConnectionString")!));
builder.Services.AddSingleton<DatabaseInitializer>();


builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));
builder.Services.AddAutoMapper(typeof(DomainToApiContractMapper).Assembly);


builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddSingleton<ISqsMessenger, SqsMessenger>();

builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapControllers();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
