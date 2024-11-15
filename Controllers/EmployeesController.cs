using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Employee_management_asp.net.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employee_management_asp.net.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly IMemoryCache _cache;

        public EmployeesController(EmployeeService employeeService, IMemoryCache cache)
        {
            _employeeService = employeeService;
            _cache = cache;
        }

        // GET: Employees/All
        public IActionResult All()
        {
            const string cacheKey = "EmployeeList";

            // Attempt to retrieve data from cache
            if (!_cache.TryGetValue(cacheKey, out List<Employee> employees))
            {
                // Data not in cache, retrieve from service
                employees = (List<Employee>?)_employeeService.GetAllEmployees();

                // Set cache options and cache the data
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5)); // Cache for 5 minutes

                _cache.Set(cacheKey, employees, cacheEntryOptions);
            }

            return View(employees);
        }

        // GET: Employees/Details/5
        public IActionResult Details(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.AddEmployee(employee);

                // Clear the employee list cache after adding a new employee
                _cache.Remove("EmployeeList");

                return RedirectToAction(nameof(All));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(employee);

                // Clear the employee list cache after editing an employee
                _cache.Remove("EmployeeList");

                return RedirectToAction(nameof(All));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeService.DeleteEmployee(id);

            // Clear the employee list cache after deleting an employee
            _cache.Remove("EmployeeList");

            return RedirectToAction(nameof(All));
        }
    }
}
