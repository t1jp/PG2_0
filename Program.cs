using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Proyecto_Graduacion.Models;
using Utilities_Net_6_MVC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Pg20Context>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));
builder.Services.AddScoped<IAppDbContext, Pg20Context>();
builder.Services.AddScoped(typeof(IRepositoryAsync<>),typeof(RepositoryAsync<>));
var app = builder.Build();


app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
