using Microsoft.EntityFrameworkCore;
using RailViewApi;
using RailViewApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RailViewContext>(o =>
{
    o.UseMySql(builder.Configuration.GetConnectionString("RailViewDb"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));

});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/alerts", async (RailViewContext db) => await db.Alerts.ToListAsync());

app.MapGet("/api/alerts/{id}", async (RailViewContext db, int id) => await db.Alerts.FindAsync(id));

app.MapPost("/api/alerts", async (RailViewContext db, Alert alert) => 
{
    await db.Alerts.AddAsync(alert);
    await db.SaveChangesAsync();
    Results.Accepted();
});

app.MapPut("/api/alerts/{id}", async (RailViewContext db, int id, Alert alert) =>
{
    if (id != alert.Id) return Results.BadRequest();

    db.Update(alert);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/api/alerts/{id}", async (RailViewContext db, int id) =>
{
    var alert = await db.Alerts.FindAsync(id);
    if (alert == null) return Results.NotFound();

    db.Alerts.Remove(alert);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();