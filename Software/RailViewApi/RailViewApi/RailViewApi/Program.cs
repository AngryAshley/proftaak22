using Microsoft.EntityFrameworkCore;
using RailViewApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

//Includes all contexts
builder.Services.AddHttpClient<TrainContext>();
builder.Services.AddHttpClient<TrainRouteContext>();
builder.Services.AddDbContext<RailViewContext>();
builder.Services.AddDbContext<RailViewv2Context>();

//Origins need to be changed in the future!
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost",
                                "https://localhost",
                                "http://127.0.0.1",
                                "https://127.0.0.1",
                                "http://i478152.hera.fhict.nl/",
                                "https://i478152.hera.fhict.nl/")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

//Use CORS policy
app.UseCors(MyAllowSpecificOrigins); // allow any origin

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/alerts", async (RailViewContext db) => await db.Alerts.ToListAsync());

//app.MapGet("/api/alerts/{id}", async (RailViewContext db, int id) => await db.Alerts.FindAsync(id));

//app.MapPut("/api/alerts/{id}", async (RailViewContext db, int id, Alert alert) =>
//{
//    if (id != alert.Id) return Results.BadRequest();

//    db.Update(alert);
//    await db.SaveChangesAsync();

//    return Results.NoContent();
//});

app.MapGet("/api/alertsv2", (RailViewv2Context db2) =>
{
    //query that selects all the tables and connects them through ID. Eventually sends the result to requested source
    var x = (from n in db2.Notifications
             join m in db2.Cameras on n.CameraId equals m.CameraId
             join c in db2.Coordinates on m.CoordinatesId equals c.CoordinatesId
             join a in db2.Accidents on n.AccidentId equals a.AccidentId
             select new
             {
                 n.NotificationId,
                 n.CameraId,
                 n.EmployeeId,
                 n.AccidentId,
                 n.StatusType,
                 n.RequiredAction,
                 m.CoordinatesId,
                 m.CameraName,
                 m.StreamLink,
                 c.Longtitude,
                 c.Latitude,
                 a.AccidentDate,
                 a.AccidentType
             }
        ).ToList();

    return x;
});

//Calls external API's from NS (this is called here because of CORS-Policy)
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