using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;

var builder = WebApplication.CreateBuilder(args);
// Set JSON to enable cycling
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
//Setup the dbContext class and usr our PRSConnection string
builder.Services.AddDbContext<PrsDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("PRSConnection"))
        );
// Build the app
var app = builder.Build();

// Allows access to files like pictures and HTML. Defaults to wwwroot
app.UseStaticFiles();
// Redirects all HTTP requests to HTTPS
app.UseHttpsRedirection();
//Enables the ability to use authorization
app.UseAuthorization();
//Maps the controllers in the application
app.MapControllers();
//Run the app
app.Run();
