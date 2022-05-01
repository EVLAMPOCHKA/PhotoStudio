using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EvlampochkaPhotoStudio.Data;
using EvlampochkaPhotoStudio.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EvlampochkaPhotoStudioContextConnection");


builder.Services.AddDbContext<EvlampochkaPhotoStudioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EvlampochkaPhotoStudioContext") ?? throw new InvalidOperationException("Connection string 'EvlampochkaPhotoStudioContext' not found.")));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
}
).AddEntityFrameworkStores<EvlampochkaPhotoStudioContext>().AddDefaultUI().AddDefaultTokenProviders();
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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
