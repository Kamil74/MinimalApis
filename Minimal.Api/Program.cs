using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);
//Service registration start here

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PeopleService>();
builder.Services.AddSingleton<GuidGenerator>();


//Service registration end here
{
    var app = builder.Build();
    //Middleware registration end here
    app.UseSwagger();
    app.UseSwaggerUI();
        //app.UseMiddleware<CookiePolicyMiddleware>();

    app.MapGet("get-example", () => "Hello from GET");
    app.MapPost("get-example", () => "Hello from POST");

    app.MapGet("ok-object", () => Results.Ok(new
    {
        Name = "Rafael Milewski"

    }));

    app.MapGet("slow-request", async () =>
    {
        await Task.Delay(1000);
        return Results.Ok(new
        {
            Name = "Rafael Milewski"
        });
    });
    app.MapGet("get", () => "Hello from GET");
    app.MapPost("post", () => "Hello from POST");
    app.MapPut("put", () => "Hello from PUT");
    app.MapDelete("delete", () => "Hello from DELETE");

    app.MapMethods("options-or-head", new[] { "HEAD", "OPTIONS" }, () => "Hello from either options or head");

    var handler = () => "This is coming from a var";
    app.MapGet("handlers", handler);
    app.MapGet("fromclass", Example.SomeMethod);


    app.MapGet("get-params/{age:int}", (int age) => { return $"Age provided was {age}"; });
    app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) => { return $"Car id provided was: {carId}"; });
    app.MapGet("books/{isbn:length(13)}", (string isbn)
        =>
    {
        return $"ISBN was: {isbn}";
    });


    app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
    {
        if (searchTerm is null)
        {
            Results.NotFound();
        }

        var result = peopleService.Search(searchTerm);
        return Results.Ok(result);

    });
    app.MapGet("mix/{routeParam}",
        (
            string routeParam,
            [FromQuery(Name = "query")] int queryParam,
            GuidGenerator guidGenerator,
            [FromHeader(Name = "Accept-Encoding")] string encoding) =>
        {
            return $"{routeParam} {queryParam} {guidGenerator.NewGuid}";
        });
    app.MapPost("people", (Person person) => { return Results.Ok(person); });
    app.MapGet("httpcontext-2",
        async (HttpContext context) => { await context.Response.WriteAsync("Hello from HttpContext 1"); });
    app.MapGet("http", async (HttpRequest request, HttpResponse response) =>
    {
        var queries = request.QueryString.Value;
        await response.WriteAsync($"Hello from HttpResponse. Queries were: {queries}");

    });
    app.MapGet("claims", (ClaimsPrincipal user) =>
    {

    });
    app.MapGet("cancel", (CancellationToken token) =>
    {
        return Results.Ok();

    });





    app.MapGet("map-point", (MapPoint point) =>
    {
        return Results.Ok(point);
    });



    app.MapGet("simple-string", () => "Hello world");
    app.MapGet("json-raw-obj", () => new { message = "Hello world" });
    app.MapGet("ok-obj", () => Results.Ok(new { message = "Hello world" }));
    app.MapGet("json-obj", () => Results.Json(new { message = "Hello world" }));
    app.MapGet("text-string", () => Results.Text("Hello world"));

    app.MapGet("stream-result", () =>
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        streamWriter.Write("Hello world");
        streamWriter.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);
        return Results.Stream(memoryStream);
    });

    app.MapGet("redirect", () => Results.Redirect("https://google.com"));
    app.MapGet("download", () => Results.File("./myfile.text"));

    app.MapGet("logging", (ILogger<Program> logger) =>
    {
        logger.LogInformation("Hello from endpoint");
        return Results.Ok();
    });


//Middleware registration stops here
    app.Run();
}
    





    



