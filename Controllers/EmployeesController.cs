using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeesController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: api/Employees
    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetAllEmployees()
    {
        var employees = _employeeService.GetAllEmployees();
        return Ok(employees);
    }

    // GET: api/Employees/{id}
    [HttpGet("{id}")]
    public ActionResult<Employee> GetEmployeeById(int id)
    {
        var employee = _employeeService.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // POST: api/Employees
    [HttpPost]
    public ActionResult<Employee> AddEmployee(Employee employee)
    {
        if (employee == null)
        {
            return BadRequest("Employee data is required.");
        }

        _employeeService.AddEmployee(employee);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }

    // PUT: api/Employees/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.Id)
        {
            return BadRequest("Employee ID mismatch.");
        }

        var existingEmployee = _employeeService.GetEmployeeById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        _employeeService.UpdateEmployee(employee);
        return Ok(employee);
    }

    // DELETE: api/Employees/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteEmployee(int id)
    {
        var existingEmployee = _employeeService.GetEmployeeById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }
        var employeeToDelete = existingEmployee;

        _employeeService.DeleteEmployee(id);
        return Ok(employeeToDelete);
    }

}
