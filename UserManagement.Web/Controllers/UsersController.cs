using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(bool? isActive = null)
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            DateOfBirth = p.DateOfBirth.ToShortDateString(),
            Email = p.Email,
            IsActive = p.IsActive
        });

        if (isActive.HasValue)
        {
            items = items.Where(x => x.IsActive == isActive.Value);
        }

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
