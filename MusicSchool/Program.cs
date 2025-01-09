using Microsoft.EntityFrameworkCore;
using MusicSchool;
using MusicSchool.Exceptions;
using MusicSchool.Services;
using MusicSchool.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<MusicSchoolDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicSchoolDbContext"));
});
builder.Services.AddTransient<IInstrumentService, InstrumentService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ISearchService, SearchService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
