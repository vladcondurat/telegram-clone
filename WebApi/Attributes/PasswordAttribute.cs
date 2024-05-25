using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApi.Attributes;

public class PasswordAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        string? password = value as string;
        
        if (string.IsNullOrEmpty(password))
        {
            return false;
        }
        
        //has at least one capital letter
        bool hasCapitalLetter = Regex.IsMatch(password, "[A-Z]"); 
        //has at least one special character
        bool hasSpecialSymbol = Regex.IsMatch(password, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        return hasCapitalLetter && hasSpecialSymbol;
    }
}