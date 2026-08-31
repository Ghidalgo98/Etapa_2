using Capa_Datos;
using Capa_Logica;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//Configuracion del DB Context MySQL


builder.Services.AddDbContext<BaseContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("CadenaMySQL"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CadenaMySQL"))));



// Add services to the container.

builder.Services.AddControllersWithViews();

//Se Agraga capa de datos
builder.Services.AddScoped<UsuarioData>();

// Se agrega capa Lógica

builder.Services.AddScoped<Usuario_Login>();   


//Configurar Sesion

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // tiempo de expiración
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
