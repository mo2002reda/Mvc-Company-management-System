using AutoMapper;
using BLL.Interfaces;
using CompanyMVC.Helper;
using CompanyMVC.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyMVC.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUniteOfWork _uniteOfWork;
        private readonly IMapper _mapper;



        public EmployeeController(IUniteOfWork uniteOfWork
                                 , IMapper mapper)//Ask CLR for creating Object from EmployeeRepository 
        {
            _uniteOfWork = uniteOfWork;
            _mapper = mapper;

        }

        public async Task<IActionResult> GetAll(string SearchEmployee)
        {
            IEnumerable<Employee> Employee;

            if (string.IsNullOrEmpty(SearchEmployee))
                Employee = await _uniteOfWork.EmployeeRepository.GetAllAsync();
            else
                Employee = _uniteOfWork.EmployeeRepository.SearchByName(SearchEmployee);


            var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employee);

            return View(MappedEmployee);


            //var mappedemployees = _mapper.Map<Employee, EmployeeViewModel>(emps);


        }
        [HttpPost]//this will carry form of data
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                    //take the image from user and return imagename which store in ImageName Attribute
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    await _uniteOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                    int result = await _uniteOfWork.CompleteAsync();
                    if (result > 0)
                    {
                        TempData["Added"] = "New Employee Added";
                    }
                    return RedirectToAction(nameof(GetAll));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employeeVM);

        }
        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            ViewBag.Departments = await _uniteOfWork.DepartmentRepository.GetAllAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return File("~/Images/bad.jpg", "jpg");
            var employee = await _uniteOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);
            //ViewData :Dictionary[Key]=Value (carry any data in action that model doesn't carry
            ViewData["HelloMessage"] = $"Details Of {MappedEmployee.Name} ";

            return View(MappedEmployee);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = await _uniteOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            try
            {
                _uniteOfWork.EmployeeRepository.Remove(employee);
                var result = await _uniteOfWork.CompleteAsync();
                if (result > 0 && employee.ImageName is not null)
                {
                    DocumentSettings.DeleteFile(employee.ImageName, "Images");
                }
                return RedirectToAction(nameof(GetAll));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)//will send specific department to edit
        {
            return await Details(id, nameof(Edit));//to display Details DepartmentDetails
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//to avoid any apps except the browser to run this method 
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try//use this cause if ther is problems in saving at database
                {
                    employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _uniteOfWork.EmployeeRepository.Update(mappedEmployee);
                    int result = await _uniteOfWork.CompleteAsync();
                    return RedirectToAction(nameof(GetAll));
                }
                catch (Exception ex)
                {//1)Log the Execption : save and Send the error to develop Team
                 //2)Form :Send error at form to user
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return View(employeeVM);
        }

    }
}
