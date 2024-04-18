using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Helpers;

//validering för checkbox
public class CheckboxRequired : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is bool b && b;
    }
}