using Employees.Database.DomainModels;

namespace Employees.ViewModels
{
    public class EmployeeAddResponseViewModel : BaseEmployeeViewModel
    {
        public List<Department> Departments { get; set; }

    }
}
