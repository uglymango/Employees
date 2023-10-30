using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers.Admin
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;
        private readonly ILogger<EmployeetController> _logger;


    }
}
