using СinemaSchedule.Extensions;
using Microsoft.Extensions.Options;
using СinemaSchedule.Application.Extensions;
using СinemaSchedule.Domen.Entities.Options;
using СinemaSchedule.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.Configure<WorkTimeOptions>(builder.Configuration);
builder.Services.Configure<WorkTimeOptions>(opt =>
{
    opt.OpeningTime = "7:00";
    opt.ClosingTime = "23:00";
});
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();

builder.AddOpenAPI();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.InitializeDatabase(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();