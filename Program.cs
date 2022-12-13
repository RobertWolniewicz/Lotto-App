using Lotto;
using Lotto.Actions;
using Lotto.Entity;
using Lotto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LottoDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("LottoConnectionString"))
        );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/Winner", (LottoDbContext Db) =>
{
    var Result= new Result()
    {
        Results = ResultGenerator.Randomizing(),
        DateTime = DateTime.Now
    };
    Db.Results.Add(Result);
    Db.SaveChanges();

   var Winners = Db.Tickets.Where(t => t.Result == Result.Results || t.Created == Result.DateTime)
    .Include(t => t.Player)
    .ToList();
    if(Winners.Any())
    {
        return Winners.SelectMany(t => t.Player.Name); 
    }
    return "No Winner";

    
});

app.MapPost("/Ticket/{Ticket}", (LottoDbContext Db, [FromBody]Player Player, [FromRoute]string Ticket ) =>
{
    if (Player.DateOfBirth.AddYears(18).Date < DateTime.Now.Date)
    {
        var NewTicket = new Ticket()
        {
            Result = Ticket,
            Created = DateTime.Now,
            Player = Player,
            PlayerId = Player.Id
        };
        Db.Players.Add(Player);
        Db.Tickets.Add(NewTicket);
        Db.SaveChanges();
        return "Good luck!";
    }
    return "You are too young to play!";
});
app.MapGet("/Results/{startDate}/{endDate}", LottoRequests.GettResults);

app.MapPost("/RandomTicket", (LottoDbContext Db, ModelPlayer Player) =>
{
    if (Player.DateOfBirth.AddYears(18).Date < DateTime.Now.Date)
    {
        var NewPlayer = new Player()
        {
            Name = Player.Name,
            LastName = Player.LastName,
            Email = Player.Email,
            PhoneNumber = Player.PhoneNumber,
            DateOfBirth = Player.DateOfBirth,

        };
        NewPlayer.Ticket = new Ticket()
        {
            Result = ResultGenerator.Randomizing(),
            Created = DateTime.Now,
            Player = NewPlayer,
            PlayerId = NewPlayer.Id
        };
        Db.Players.Add(NewPlayer);
        Db.SaveChanges();
        return "Good Luck";
    }
    return "You are too young to play!";
});


app.Run();

