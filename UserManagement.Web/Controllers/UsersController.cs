using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ActionResult Index(bool? isActive = null)
    {
        try
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

            var model = new UserIndexViewModel
            {
                Items = items.ToList()
            };

            return View(model);
        }
        catch
        {
            TempData["ErrorMessage"] = "Sorry, something went wrong when loading users. Please try again.";
            return RedirectToAction("Index", "Home");
        }
        
    }

    [HttpGet]
    public ActionResult Edit(long? id)
    {
        var model = new UserInformationViewModel();
        try
        {
            if (id.HasValue)
            {
                var userDetails = _userService.GetById(id.Value);
                if (userDetails == null)
                {
                    TempData["ErrorMessage"] = "User not found.";
                    return RedirectToAction("Index", "Users");
                }

                model.Id = userDetails.Id;
                model.Forename = userDetails.Forename;
                model.Surname = userDetails.Surname;
                model.DateOfBirth = userDetails.DateOfBirth;
                model.Email = userDetails.Email;
                model.IsActive = userDetails.IsActive;
            }
        }
        catch
        {
            TempData["ErrorMessage"] = "Sorry, something went wrong. Please try again.";
            return RedirectToAction("Index", "Users");
        }
        
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(UserInformationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var user = model.Id.HasValue ? _userService.GetById(model.Id.Value) : new User();

            user.Forename = model.Forename;
            user.Surname = model.Surname;
            user.DateOfBirth = model.DateOfBirth;
            user.Email = model.Email;
            user.IsActive = model.IsActive;

            _userService.Update(user);
        }
        catch
        {
            TempData["ErrorMessage"] = "Sorry, something went wrong. Please try again.";
        }

        return RedirectToAction("Index", "Users");
    }


    [HttpGet]
    public ActionResult Details(long id)
    {
        var model = new UserInformationViewModel();
        try
        {
            var userDetails = _userService.GetById(id);

            model.Id = userDetails.Id;
            model.Forename = userDetails.Forename;
            model.Surname = userDetails.Surname;
            model.DateOfBirth = userDetails.DateOfBirth;
            model.Email = userDetails.Email;
            model.IsActive = userDetails.IsActive;
        }
        catch
        {
            TempData["ErrorMessage"] = "Sorry, something went wrong. Please try again.";
            return RedirectToAction("Index", "Users");
        }

        return View(model);
    }

}
