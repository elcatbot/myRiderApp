using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace myRideApp.Extensions.Hosting;

public static class AuthtExtensions
{
    public static void AddAuth(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
         {
             options.TokenValidationParameters = new()
             {
                 ValidIssuer = builder.Configuration["JwtToken:Issuer"],
                 ValidAudience = builder.Configuration["JwtToken:Audience"],
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:Key"]!))
             };
             options.RequireHttpsMetadata = false;
             options.IncludeErrorDetails = true;
             options.SaveToken = true;
         });

        builder.Services.AddAuthorization(options =>
        {
            // Example policy: only drivers
            options.AddPolicy("Authenticated", policy =>
            {
                policy.RequireAuthenticatedUser();
            });

            options.AddPolicy("RiderOnly", policy =>
            {
                policy.RequireRole("rider");
            });
        });
    }

    public static void AddWebCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(o =>
        {
            o.AddPolicy("AllowWebAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });            
        });
    }
            
}