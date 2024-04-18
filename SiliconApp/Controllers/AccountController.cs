using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiliconApp.ViewModels.Account;

namespace SiliconApp.Controllers;

    [Authorize] //du måste vara inloggad för att få tillgång till sidan. kan sättas här om det ska gälla för allt, annars kan du även sätta på enstaka ställen. 
    public class AccountController(UserManager<UserEntity> userManager, AddressService addressService) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly AddressService _addressService = addressService;

        [HttpGet]
        [Route("/account")]
        public async Task<IActionResult> Details()
        {
            var viewModel = new AccountDetailsViewModel
            {
                ProfileInfo = await PopulateProfileInfoAsync()
            };
            viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
            viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

            return View(viewModel);
        }

        [HttpPost]
        [Route("/account")]
        public async Task<IActionResult> Details(AccountDetailsViewModel viewModel)
        {
            if (viewModel.BasicInfo != null)
            {

                if (viewModel.BasicInfo.FirstName != null && viewModel.BasicInfo.LastName != null && viewModel.BasicInfo.Email != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        user.FirstName = viewModel.BasicInfo.FirstName;
                        user.LastName = viewModel.BasicInfo.LastName;
                        user.Email = viewModel.BasicInfo.Email;
                        user.PhoneNumber = viewModel.BasicInfo.Phone;
                        user.Biography = viewModel.BasicInfo.Biography;

                        await _userManager.UpdateAsync(user);
                    }
                }
            }

            if (viewModel.AddressInfo != null)
            {
                if (viewModel.AddressInfo.StreetName != null && viewModel.AddressInfo.PostalCode != null && viewModel.AddressInfo.City != null)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user != null)
                    {
                        var address = await _addressService.GetAddressAsync(user.Id);
                        if (address != null)
                        {
                            address.StreetName = viewModel.AddressInfo.StreetName;
                            address.SecondStreetName = viewModel.AddressInfo.SecondStreetName;
                            address.PostalCode = viewModel.AddressInfo.PostalCode;
                            address.City = viewModel.AddressInfo.City;

                            await _addressService.UpdateAddressAsync(address); //om användaren har en befintlig adress så uppdatera 
                        }
                        else
                        {
                            address = new AddressEntity
                            {
                                UserId = user.Id,
                                StreetName = viewModel.AddressInfo.StreetName,
                                SecondStreetName = viewModel.AddressInfo.SecondStreetName,
                                PostalCode = viewModel.AddressInfo.PostalCode,
                                City = viewModel.AddressInfo.City,
                            };

                            await _addressService.CreateAddressAsync(address);
                        }
                    }
                }
            }

            viewModel.ProfileInfo = await PopulateProfileInfoAsync(); //inga kontroller, hämtar alltid in profilinformationen
            viewModel.BasicInfo ??= await PopulateBasicInfoAsync();
            viewModel.AddressInfo ??= await PopulateAddressInfoAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return RedirectToAction("Details", "Account");
        }

        private async Task<AccountProfileInfoViewModel> PopulateProfileInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return new AccountProfileInfoViewModel
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                IsExternalAccount = user.IsExternalAccount,
            };
        }


        private async Task<AccountBasicInfoViewModel> PopulateBasicInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            return new AccountBasicInfoViewModel
            {
                UserId = user!.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Biography = user.Biography,
            };
        }

        private async Task<AccountAddressInfoViewModel> PopulateAddressInfoAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var address = await _addressService.GetAddressAsync(user.Id);

                if (address != null)
                {
                    return new AccountAddressInfoViewModel
                    {
                        StreetName = address.StreetName,
                        SecondStreetName = address.SecondStreetName,
                        PostalCode = address.PostalCode,
                        City = address.City,
                    };
                }

            }
            return new AccountAddressInfoViewModel();
        }
    }
