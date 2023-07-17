using Client.Contracts;
using Client.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

// Jwt Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.RequireHttpsMetadata = false; // For development, jiak production maka true
           options.SaveToken = true; // untuk mengharuskan menyimpan token di req header
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               ValidateIssuer = true,
               ValidIssuer = builder.Configuration["JWTService:Issuer"],
               ValidateAudience = true,
               ValidAudience = builder.Configuration["JWTService:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:Key"])),
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
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

//app.UseStatusCodePages(async context =>
//{
//    var response = context.HttpContext.Response;

//    if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
//    {
//        response.Redirect("/unauthorized");
//    }
//    else if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
//    {
//        response.Redirect("/notfound");
//    }
//    else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
//    {
//        response.Redirect("/forbidden");
//    }
//});



////Add JWToken to all incoming HTTP Request Header
//app.Use(async (context, next) =>
//{
//    var JWToken = context.Session.GetString("JWToken");

//    if (!string.IsNullOrEmpty(JWToken))
//    {
//        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
//    }

//    await next();
//});

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
