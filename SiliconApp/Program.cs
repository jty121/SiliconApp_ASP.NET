using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);//sökvägarna uppe i url´n blandar inte stora och små.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext_WebApp>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase")));
builder.Services.AddHttpClient();

// ROLLHANTERING MED IDENTITY                           //separera om du vill ha större tillgång istället för att enbart köra default-identity
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;

}).AddRoles<IdentityRole>()                           //färdig rolltjänst som hanterar rollerna med hjälp av identity
    .AddEntityFrameworkStores<DataContext_WebApp>();


// COOKIES
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";                            //har med authorize att göra. 
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";                     // kan sättas om man vill säkra upp tillgång, ex om du inte är administratör ska du inte få tillgång till viss information/kunna göra ändringar
    x.Cookie.HttpOnly = true;                           //förhindrar risker för cross side scripting - får inte läsas av webbläsaren. Ingen kan läsa ut från cookie delen
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);        //är användaren inaktiv i 60 min så loggas man ut atuomatiskt
    x.SlidingExpiration = true;                         //om användaren varit inaktiv men efter ett tag gör något på sidan, då nollställs tiden och räknas om. 
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;  //se till att alla dina request går via HTTPS. OBS ska ALLTID vara detta. körs aldrig via http längre.  
});


// EXTERN AUTENTISERING
builder.Services.AddAuthentication().AddFacebook(x =>   //konfigurering till facebook, från meta. 
{
    x.AppId = "781356204056022";
    x.AppSecret = "717b7f783ba4eeb1df0d1b75bcbcf7b5";
    x.Fields.Add("first_name");
    x.Fields.Add("last_name");
});
builder.Services.AddAuthentication().AddGoogle(x =>
{
    x.ClientId = "539456252257-kbtsvapaau1sp5ik2j3prl8v395a8lpb.apps.googleusercontent.com";
    x.ClientSecret = "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";
});


// DEPENDECY INJECTIONS - REGISTRERA SERVICES, REPOSITORIES
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<AccountManager>();
var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// MIDDLEWARES
app.UseHsts();
app.UseStatusCodePagesWithReExecute("/error");          //sidor som inte existerar går till 404-error sidan
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSession();                                   //middleware för usersession
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = ["Admin", "User"];                 //en array på rollerna som vi vill ha med, går att lägga till fler
    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))    //skapa bara roller som inte existerar, startar du upp sidan så läggs det till i databasen om rollen inte finns
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

