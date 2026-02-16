using System.Text;
using Common;
using Common.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarRentalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ,b=>b.MigrationsAssembly("Common"));
    options.UseLazyLoadingProxies(); 
});


builder.Services.AddControllers()

    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = "radostin",
            ValidAudience = "university",
            IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes("!Password123!Password123!Password123"))
        };
    });

builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<RentalService>();
builder.Services.AddScoped<PaymentService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();




var app = builder.Build();




app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();



app.Run();
