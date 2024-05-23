using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TourOperator.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TourDbContext>(opt => opt
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(30);
    opt.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services
    .AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/forbidden";
        options.LoginPath = "/login";
    });

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<TourDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.Use(async (context, next) =>
          {
              context.Response.Headers.Add("Content-Security-Policy", "script-src 'self' https://cdnjs.cloudflare.com; style-src 'self'; img-src 'self'; connect-src 'self'; frame-src 'self'; frame-ancestors 'self'; font-src 'self'; media-src 'self'; object-src 'self'; manifest-src 'self'; form-action 'self'; data:");
             
              await next();
          });


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
