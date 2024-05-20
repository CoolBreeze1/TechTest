using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users;

public class UserInformationViewModel
{
    public long? Id { get; set; }
    public string Forename { get; set; } = default!;
    public string Surname { get; set; } = default!;
    public DateTime DateOfBirth { get; set; } = default!;

    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
}
