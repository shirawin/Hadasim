using Microsoft.EntityFrameworkCore;
using Repositories.GeneratedModels;
using Services.members;
using Services.CoronaVirus;
using Services.Vaccinations;

var builder = WebApplication.CreateBuilder(args);

var MyAppOrigin = "MyAppOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAppOrigin,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader().AllowAnyMethod(); ;
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration["MyDBConnectionString"];

builder.Services.AddDbContext<MyDBContext>(options => options.UseNpgsql(cs));

builder.Services.AddScoped<IMemberData,MemberData>();
builder.Services.AddScoped<ICoronaVirusData, CoronaVirusData>();
builder.Services.AddScoped<IVaccinationData, VaccinationData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAppOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
