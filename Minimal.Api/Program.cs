using Minimal.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
app.MapGet("handlers",  handler);
app.MapGet("fromclass", Example.SomeMethod);


app.Run();