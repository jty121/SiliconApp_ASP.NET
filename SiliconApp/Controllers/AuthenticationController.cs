using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiliconApp.ViewModels.Authentication;
using System.Security.Claims;

namespace SiliconApp.Controllers
{
    public class AuthenticationController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager) : Controller
    {

        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;

        [HttpGet]
            [Route("/signup")]
            public IActionResult SignUp()
            {
                if (_signInManager.IsSignedIn(User)) //använder sig av claims delar 
                {
                    return RedirectToAction("Details", "Account");
                }
                return View();
            }

            [Route("/signup")]
            [HttpPost]
            //när vi postar formuläret kommer den gå till den här controllern via den här action delen där vi hämtar upp en modell
            public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
            {
                if (ModelState.IsValid)
                {
                    var exists = await _userManager.Users.AnyAsync(x => x.Email == viewModel.Email);
                    if (exists)
                    {
                        ModelState.AddModelError("AlreadyExists", "User whit the same email already exists");
                        ViewData["ErrorMessage"] = "User whit the same email already exists";
                        return View(viewModel); //finns det en användare så skicka tillbaka vyn
                    }

                    var userEntity = new UserEntity
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Email = viewModel.Email,
                        UserName = viewModel.Email
                    };

                    var result = await _userManager.CreateAsync(userEntity, viewModel.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(userEntity, "User");  //när en användare skapas, lägg till även en roll
                        return RedirectToAction("SignIn", "Authentication");
                    }
                }
                return View(viewModel);
            }

            [Route("/signin")]
            [HttpGet]
            public IActionResult SignIn(string returnUrl)
            {
                if (_signInManager.IsSignedIn(User)) //använder sig av claims delar 
                {
                    return RedirectToAction("Details", "Account");
                }

                ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/"); //om vi inte har en returnUrl så säger vi att vi ska gå tillbaka till grundsidan
                return View();
            }

            [Route("/signin")]
            [HttpPost]
            public async Task<IActionResult> SignIn(SignInViewModel viewModel, string returnUrl)
            {

                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Details", "Account");
                    }
                }
                ModelState.AddModelError("IncorrectValués", "Incorrect email or password");
                ViewData["ErrorMessage"] = "Incorrect email or password";

                return View(viewModel);
            }


            [HttpGet]
            [Route("/signout")]
            public new async Task<IActionResult> SignOut()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }


            //extern inloggning facebook, google. 
            [HttpGet]
            public IActionResult Facebook()
            {
                var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", Url.Action("FacebookCallback"));
                return new ChallengeResult("Facebook", authProps);
            }

            [HttpGet]
            public async Task<IActionResult> FacebookCallback()
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info != null)
                {
                    var userEntity = new UserEntity
                    {
                        FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                        LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                        IsExternalAccount = true
                    };

                    var user = await _userManager.FindByEmailAsync(userEntity.Email);
                    if (user == null)
                    {
                        var result = await _userManager.CreateAsync(userEntity); //löseonrds autentisering sker på facebooks sida
                        if (result.Succeeded)
                        {
                            user = await _userManager.FindByEmailAsync(userEntity.Email);
                        }
                    }
                    //ingen else sats, ska göra antingen det ena eller det andra. inte OM. 
                    if (user != null)
                    {
                        if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                        {
                            user.FirstName = userEntity.FirstName;
                            user.LastName = userEntity.LastName;
                            user.Email = userEntity.Email;

                            await _userManager.UpdateAsync(user);
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        if (HttpContext.User != null)
                            return RedirectToAction("Details", "Account");
                    }
                }
                ViewData["StatusMessage"] = "Failed to authenticate with Facebook";
                return RedirectToAction("SignIn", "Authentication");
            }


            [HttpGet]
            public IActionResult Google()
            {
                var authProps = _signInManager.ConfigureExternalAuthenticationProperties("Google", Url.Action("GoogleCallback"));
                return new ChallengeResult("Google", authProps);
            }

            [HttpGet]
            public async Task<IActionResult> GoogleCallback()
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info != null)
                {
                    var userEntity = new UserEntity
                    {
                        FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!,
                        LastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!,
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                        UserName = info.Principal.FindFirstValue(ClaimTypes.Email)!,
                        IsExternalAccount = true
                    };

                    var user = await _userManager.FindByEmailAsync(userEntity.Email);
                    if (user == null)
                    {
                        var result = await _userManager.CreateAsync(userEntity); //löseonrds autentisering sker på googles sida
                        if (result.Succeeded)
                        {
                            user = await _userManager.FindByEmailAsync(userEntity.Email);
                        }
                    }
                    //ingen else sats, ska göra antingen det ena eller det andra. inte OM. 
                    if (user != null)
                    {
                        if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                        {
                            user.FirstName = userEntity.FirstName;
                            user.LastName = userEntity.LastName;
                            user.Email = userEntity.Email;

                            await _userManager.UpdateAsync(user);
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        if (HttpContext.User != null)
                            return RedirectToAction("Details", "Account");
                    }
                }
                ViewData["StatusMessage"] = "Failed to authenticate with Google";
                return RedirectToAction("SignIn", "Authentication");
            }


            [HttpGet]
            public IActionResult Security()
            {
                return View();
            }
        }
}
