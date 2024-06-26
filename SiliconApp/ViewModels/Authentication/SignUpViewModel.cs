﻿using SiliconApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SiliconApp.ViewModels.Authentication;

public class SignUpViewModel
{

    [Display(Name = "First name", Prompt = "Enter your first name", Order = 0)]
    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = null!;


    [Display(Name = "last name", Prompt = "Enter your last name", Order = 1)]
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = null!;


    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 2)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Your email address is invalid")]
    public string Email { get; set; } = null!;


    [Display(Name = "Password", Prompt = "Enter password", Order = 3)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is requried")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "You must enter a strong password")]
    public string Password { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password must be confirmed")]
    [Compare(nameof(Password), ErrorMessage = "Passwords does not match")]
    public string ConfirmPassword { get; set; } = null!;


    [Display(Name = "I Agree to the Terms & Conditions.", Order = 5)]
    [Required(ErrorMessage = "You must accept the terms and conditions")]
    [CheckboxRequired(ErrorMessage = "You must accept the terms and conditions")]
    public bool Terms { get; set; } = false;
}
