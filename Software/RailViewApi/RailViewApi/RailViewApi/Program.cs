using Microsoft.EntityFrameworkCore;
//using RailViewApi;
using RailViewApi.Models;
//using RestSharp;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<TrainContext>();
builder.Services.AddHttpClient<TrainRouteContext>();
builder.Services.AddDbContext<RailViewContext>(o =>
{
    o.UseMySql(builder.Configuration.GetConnectionString("RailViewDb"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));

});

//Origins need to be changed in the future!
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost",
                                "http://127.0.0.1")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

//Use CORS policy
app.UseCors(MyAllowSpecificOrigins);

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

app.MapGet("/api/trains", async (HttpClient client) =>
{

    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("TrainUrl"));
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Add(builder.Configuration.GetConnectionString("ApiKeyType"), builder.Configuration.GetConnectionString("ApiKey"));

    HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
    response.EnsureSuccessStatusCode();
    var responseAsString = await response.Content.ReadAsStringAsync();
    var responseAsConcreteType = JsonConvert.DeserializeObject<Train>(responseAsString);
    return responseAsConcreteType;
});

app.MapGet("/api/routes", async (HttpClient client) =>
{

    client.BaseAddress = new Uri(builder.Configuration.GetConnectionString("RouteUrl"));
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Add(builder.Configuration.GetConnectionString("ApiKeyType"), builder.Configuration.GetConnectionString("ApiKey"));

    HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
    response.EnsureSuccessStatusCode();
    var responseAsString = await response.Content.ReadAsStringAsync();
    var responseAsConcreteType = JsonConvert.DeserializeObject<TrainRoute>(responseAsString);
    return responseAsConcreteType;
});

app.Run();