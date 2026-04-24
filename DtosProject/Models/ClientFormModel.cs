using System.ComponentModel.DataAnnotations;

namespace DtosProject.Models;

public class ClientFormModel
{
    [Required]
    public string FullName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}