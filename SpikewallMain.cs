using spikewall.Debug;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DebugHelper.ColorfulWrite(new ColorfulString(ConsoleColor.Green, Console.BackgroundColor, $"spikewall - process started at " + DateTime.Now + "\n"));
DebugHelper.ColorfulWrite(new ColorfulString(ConsoleColor.Red, Console.BackgroundColor, "This is experimental software. Normal operation is not guaranteed. Do not use with real databases until the software is stable.\n\n"));

spikewall.Db.Initialize();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("/robots.txt", async context =>
{
    await context.Response.WriteAsync("User-agent: *\nDisallow: /");
});

app.MapGet("/generate204", async context =>
{
    // for uptime monitoring
    context.Response.StatusCode = 204;
    await context.Response.WriteAsync("");
});

app.MapControllers();

app.Run();
