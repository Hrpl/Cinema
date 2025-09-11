using СinemaSchedule.Domen.Options;
using СinemaSchedule.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.Configure<WorkTimeOptions>(builder.Configuration);
builder.Services.AddRepositories();

builder.AddOpenAPI();

builder.InitializeDatabase(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.Run();
