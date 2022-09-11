using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HrDbProject.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HrDbProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HrDbProjectContext") ?? throw new InvalidOperationException("Connection string 'HrDbProjectContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// var host = CreateHostBuilder(args).Build(); <<< not needed because build is assigned to 'app' above
using (var scope = app.Services.CreateScope())
// this will end up as a local function to Main()
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<HrDbProjectContext>();
        context.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex,"An error occurred creating the database.");
    }
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
