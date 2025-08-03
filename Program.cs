var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // required for MapControllers
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles(); // ✅ add this if you're serving static files like CSS, JS, images

app.UseRouting();
app.UseAuthorization();

app.MapControllers();   // for your API routes
app.MapRazorPages();    // for your Razor pages

app.Run();
