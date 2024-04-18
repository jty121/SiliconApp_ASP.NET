//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

namespace SiliconWebApi.Configurations;

public static class JwtConfiguration
{
    //public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
    //{
    //    services.AddAuthentication(x =>
    //    {
    //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //    }).AddJwtBearer(x =>
    //        {
    //            x.TokenValidationParameters = new TokenValidationParameters
    //            {
    //                //här validerar vi utgivare, som finns med i appsettings.json
    //                ValidateIssuer = true,
    //                ValidIssuer = configuration["Token:Issuer"],

    //                ValidateAudience = true,
    //                ValidAudience = configuration["Token:Audience"],

    //                ValidateIssuerSigningKey = true,
    //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]!)),

    //                ValidateLifetime = true, //livslängden på token nyckeln

    //                ClockSkew = TimeSpan.FromSeconds(5), //säger att tiden/klockan får skilja sig åt max 5 sec, eftersom olika system kan ha viss differans tidsmässigt
    //            };
    //    });

    //}
}
