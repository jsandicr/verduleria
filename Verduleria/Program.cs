using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Verduleria.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VerduleriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VerduleriaContext") ?? throw new InvalidOperationException("Connection string 'VerduleriaContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Verduleria}/{action=Index}/{id?}");

app.Run();
