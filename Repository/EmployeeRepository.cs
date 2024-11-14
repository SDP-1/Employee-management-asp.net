using Microsoft.EntityFrameworkCore;

public class EmployeeRepository
{
    private readonly MyDbContext _context;

    public EmployeeRepository(MyDbContext context)
    {
        _context = context;
    }

    // GET All Employees
    public IEnumerable<Employee> GetAllEmployees() => _context.Employees.ToList();

    // GET Employee by Id with AsNoTracking to avoid tracking conflicts
    public Employee GetEmployeeById(int id)
    {
        return _context.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);
    }

    // Add Employee
    public void AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    // Update Employee with explicit detaching if necessary
    public void UpdateEmployee(Employee employee)
    {
        // Find the existing employee (this ensures we are working with a single tracked entity)
        var existingEmployee = _context.Employees.Find(employee.Id);

        if (existingEmployee != null)
        {
            // Detach the existing employee if it's being tracked already
            _context.Entry(existingEmployee).State = EntityState.Detached;
        }

        // Now update the employee, we are not causing duplicate tracking
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    // Delete Employee
    public void DeleteEmployee(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}
