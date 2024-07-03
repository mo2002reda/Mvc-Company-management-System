using AutoMapper;
using CompanyMVC.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Role = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(Role);
                return View(MappedRole);
            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(SearchValue);
                var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(new List<RoleViewModel>() { mappedRole });//cause the view render above for IEnumrable of object 
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();

            var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
            return View(ViewName, MappedRole);
        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(model.Id);
                    Role.Name = model.RoleName;
                    await _roleManager.UpdateAsync(Role);
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
        public async Task<IActionResult> Delete(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {

                var Role = await _roleManager.FindByIdAsync(model.Id);
                if (Role != null)
                {
                    try
                    {
                        await _roleManager.DeleteAsync(Role);
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

        public async Task<IActionResult> AddOrRemoveUsers(string roleid)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role is null)
                return NotFound();
            ViewBag.RoleId = roleid;
            var UsersInRole = new List<UserInRoleViewModel>();
            var Users = await _userManager.Users.ToListAsync();//to get all users
            foreach (var user in Users)
            {
                var UserInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    UserInRole.IsSelected = true;
                else
                    UserInRole.IsSelected = false;
                UsersInRole.Add(UserInRole);

            }
            return View(UsersInRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleid, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleid);
            if (role is null)
                return BadRequest();

            if (ModelState.IsValid)
            {

                foreach (var user in users)
                {
                    var AppUser = await _userManager.FindByIdAsync(user.UserId);
                    if (AppUser != null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(AppUser, role.Name))
                        {//this mean that this user checked and previous he not have this role 
                            await _userManager.AddToRoleAsync(AppUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(AppUser, role.Name))
                        {//this mean that user not checked and has this role so it must delete from him
                            await _userManager.RemoveFromRoleAsync(AppUser, role.Name);
                        }
                    }

                }
                return RedirectToAction(nameof(Edit), new { id = roleid });


            }
            return View(users);
        }



    }
}
