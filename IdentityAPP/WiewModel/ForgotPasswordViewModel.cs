using System.ComponentModel.DataAnnotations;

namespace IdentityAPP.WiewModel
{
    public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}

}