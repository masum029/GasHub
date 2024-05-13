using GasHub.Services.Abstractions;
using GasHub.Services.Implemettions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWorkClientServices, UnitOfWorkClientServices>();
builder.Services.AddHttpClient("GasHubClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7128/api/");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
