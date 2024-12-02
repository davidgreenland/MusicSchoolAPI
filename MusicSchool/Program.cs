using Microsoft.EntityFrameworkCore;
using MusicSchool;
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
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
