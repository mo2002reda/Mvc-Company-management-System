using AutoMapper;
using CompanyMVC.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var User = _user.Users.Select(
                    U => new UserViewModel()
                    {
                        Email = U.Email,
                        FName = U.FName,
                        LName = U.LName,
                        Id = U.Id,
                        PhoneNumber = U.PhoneNumber,
                        Roles = _user.GetRolesAsync(U).Result//used Result to aVoid async 
                    });
                return View(User);
            }
            else
            {
                var User = await _user.FindByEmailAsync(SearchValue);
                var MappedUser = new UserViewModel()//Manual mapping
                {
                    Email = User.Email,
                    FName = User.FName,
                    LName = User.LName,
                    Id = User.Id,
                    PhoneNumber = User.PhoneNumber,
                    Roles = _user.GetRolesAsync(User).Result//used Result to aVoid async 

                };
                return View(new List<UserViewModel> { MappedUser });//To Put mapped output in the List To return as IEnumrable For Result Of if and Result of Else
            }

        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var User = await _user.FindByIdAsync(id);
            if (User is null)
                return NotFound();

            var MappedUser = _mapper.Map<ApplicationUser, UserViewModel>(User);
            return View(ViewName, MappedUser);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var User = await _user.FindByIdAsync(model.Id);
                    User.PhoneNumber = model.PhoneNumber;
                    User.FName = model.FName;
                    User.LName = model.LName;//cause Update not deal with mapped data case it deal with spacific database(identity database)
                    await _user.UpdateAsync(User);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string Id)
        {
            return await Details(Id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _user.FindByIdAsync(model.Id);
                if (user != null)
                {
                    try
                    {
                        await _user.DeleteAsync(user);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                    return BadRequest();

            }
            return View(model);
        }
    }
}
