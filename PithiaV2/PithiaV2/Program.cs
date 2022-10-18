using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PithiaV2.Data;
using PithiaV2.EndpointManager;
using PithiaV2.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointDefinitions(typeof(User));
builder.Services.AddEndpointDefinitions(typeof(StudentXCourse));
builder.Services.AddEndpointDefinitions(typeof(Course));
builder.Services.AddEndpointDefinitions(typeof(Professor));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseEndpointDefinitions();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();