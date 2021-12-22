using Microsoft.EntityFrameworkCore;
using RailViewApi;
using RailViewApi.Models;
using RestSharp;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RailViewContext>(o =>
{
    o.UseMySql(builder.Configuration.GetConnectionString("RailViewDb"),
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.23-mysql"));

});

builder.Services.AddHttpClient<TestContext>(o =>
{
    o.BaseAddress = new Uri("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
    o.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");


    //o.GetStringAsync("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
    //var client = new RestClient("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
    //client.Timeout = -1;
    //var request = new RestRequest(Method.GET);
    //request.AddHeader("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");
    //IRestResponse response = client.Execute(request);
    //Train train = new Train();
    //train = JsonConvert.DeserializeObject<Train>(response.Content);
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

//test
//app.MapGet("/api/test", async (TestContext test) => await test.Trains.ToListAsync());
////{
////    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

////    var client = new RestClient("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
////    client.Timeout = -1;
////    var request = new RestRequest(Method.GET);
////    request.AddHeader("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");
////    IRestResponse response = client.Execute(request);

////    test.Trains.Add(train);

////    return await test.Trains.ToListAsync();
////});

app.MapGet("/api/test", async (HttpClient client) => {

    client.BaseAddress = new Uri("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c4f11ff5e4ea4e13981610420db4a3b1");

    HttpResponseMessage response = await client.GetAsync("https://gateway.apiportal.ns.nl/virtual-train-api/api/vehicle");
    response.EnsureSuccessStatusCode();
    var responseAsString = await response.Content.ReadAsStringAsync();
    var responseAsConcreteType = JsonConvert.DeserializeObject<Train>(responseAsString);
    return responseAsConcreteType;
});

app.Run();