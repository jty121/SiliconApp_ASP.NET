using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(x => x.LowercaseUrls = true);//s�kv�garna uppe i url�n blandar inte stora och sm�.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext_WebApp>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDatabase")));
builder.Services.AddHttpClient();

// ROLLHANTERING MED IDENTITY                           //separera om du vill ha st�rre tillg�ng ist�llet f�r att enbart k�ra default-identity
builder.Services.AddDefaultIdentity<UserEntity>(x =>
{
    x.User.RequireUniqueEmail = true;
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;

}).AddRoles<IdentityRole>()                           //f�rdig rolltj�nst som hanterar rollerna med hj�lp av identity
    .AddEntityFrameworkStores<DataContext_WebApp>();


// COOKIES
builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/signin";                            //har med authorize att g�ra. 
    x.LogoutPath = "/signout";
    x.AccessDeniedPath = "/denied";                     // kan s�ttas om man vill s�kra upp tillg�ng, ex om du inte �r administrat�r ska du inte f� tillg�ng till viss information/kunna g�ra �ndringar
    x.Cookie.HttpOnly = true;                           //f�rhindrar risker f�r cross side scripting - f�r inte l�sas av webbl�saren. Ingen kan l�sa ut fr�n cookie delen
    x.ExpireTimeSpan = TimeSpan.FromMinutes(60);        //�r anv�ndaren inaktiv i 60 min s� loggas man ut atuomatiskt
    x.SlidingExpiration = true;                         //om anv�ndaren varit inaktiv men efter ett tag g�r n�got p� sidan, d� nollst�lls tiden och r�knas om. 
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;  //se till att alla dina request g�r via HTTPS. OBS ska ALLTID vara detta. k�rs aldrig via http l�ngre.  
});


// EXTERN AUTENTISERING
builder.Services.AddAuthentication().AddFacebook(x =>   //konfigurering till facebook, fr�n meta. 
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
app.UseStatusCodePagesWithReExecute("/error");          //sidor som inte existerar g�r till 404-error sidan
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseUserSession();                                   //middleware f�r usersession
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = ["Admin", "User"];                 //en array p� rollerna som vi vill ha med, g�r att l�gga till fler
    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))    //skapa bara roller som inte existerar, startar du upp sidan s� l�ggs det till i databasen om rollen inte finns
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

