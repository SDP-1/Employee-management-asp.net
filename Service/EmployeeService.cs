public class EmployeeService
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly PublicHolidayRepository _holidayRepository;

    public EmployeeService(EmployeeRepository employeeRepository, PublicHolidayRepository holidayRepository)
    {
        _employeeRepository = employeeRepository;
        _holidayRepository = holidayRepository;
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

    // Calculate working days
    public int CalculateWorkingDays(DateTime startDate, DateTime endDate)
    {
        if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
            throw new ArgumentException("Start date must be a weekday.");

        var publicHolidays = _holidayRepository.GetPublicHolidays().Select(h => h.Date).ToHashSet();
        int workingDays = 0;

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday &&
                date.DayOfWeek != DayOfWeek.Sunday &&
                !publicHolidays.Contains(date))
            {
                workingDays++;
            }
        }
        return workingDays;
    }
}
