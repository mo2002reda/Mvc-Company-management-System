using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUniteOfWork _uniteOfWork;

        public DepartmentController(IUniteOfWork uniteOfWork)
        {

            _uniteOfWork = uniteOfWork;
        }

        public async Task<IActionResult> GetAllDepartments()
        {
            var depts = await _uniteOfWork.DepartmentRepository.GetAllAsync();
            return View(depts);
        }
        public async Task<IActionResult> AddNewDepartment()
        {
            Department dept = new Department();
            return View(dept);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewDepartment(Department department)
        {//Client Side Validation : Error appear to user when he complete data at input fields (put at Views)
         //server side validation :Error appear to user after he submit the form (Put at the actions or methods)
            if (ModelState.IsValid)//server Side Validation :check all form then appear the errors
            {
                await _uniteOfWork.DepartmentRepository.AddAsync(department);
                int result = await _uniteOfWork.CompleteAsync();
                if (result > 0)
                {
                    TempData["Added"] = $"{department.Name} Department Created";
                }
                return RedirectToAction(nameof(GetAllDepartments));
            }
            return View(department);//will return the same data which entered in the form
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "DepartmentDetails")
        {
            if (id is null)
                return BadRequest();//Status Code 400
            var department = await _uniteOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(ViewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)//will send specific department to edit
        {

            #region Edit Code
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetDepartmentById(id.Value);
            //if (department is null)
            //    return NotFound(); 
            #endregion
            return await Details(id, nameof(Edit));//to display Details DepartmentDetails
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//to avoid any apps except the browser to run this method 
        public async Task<IActionResult> Edit(Department department, int id)
        {
            if (id != department.ID)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try//use this cause if ther is problems in saving at database
                {
                    _uniteOfWork.DepartmentRepository.Update(department);
                    await _uniteOfWork.CompleteAsync();
                    return RedirectToAction(nameof(GetAllDepartments));
                }
                catch (Exception ex)
                {//1)Log the Execption : save and Send the error to develop Team
                 //2)Form :Send error at form to user
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _uniteOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            _uniteOfWork.DepartmentRepository.Remove(department);
            await _uniteOfWork.CompleteAsync();
            return RedirectToAction(nameof(GetAllDepartments));
        }

    }
}
