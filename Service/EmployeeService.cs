public class EmployeeService
{
    private readonly EmployeeRepository _employeeRepository;

    public EmployeeService(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    // GET All Employees
    public IEnumerable<Employee> GetAllEmployees() => _employeeRepository.GetAllEmployees();

    // GET Employee by Id
    public Employee GetEmployeeById(int id) => _employeeRepository.GetEmployeeById(id);

    // Add Employee
    public void AddEmployee(Employee employee) => _employeeRepository.AddEmployee(employee);

    // Update Employee
    public void UpdateEmployee(Employee employee) => _employeeRepository.UpdateEmployee(employee);

    // Delete Employee
    public void DeleteEmployee(int id) => _employeeRepository.DeleteEmployee(id);
}
