﻿namespace UserManagement.Web.Models.Users;

public class UserIndexViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
}