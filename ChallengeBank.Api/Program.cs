using Microsoft.EntityFrameworkCore;
using ChallengeBank.Application.Services;
using ChallengeBank.Domain.Interfaces;
using ChallengeBank.Infrastructure.Data;
using ChallengeBank.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<ChallengeBankContext>(opt =>
    opt.UseSqlite("Data Source=challengebank.db"));

builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<ContaService>();





var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ChallengeBankContext>();
    db.Database.Migrate(); 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();