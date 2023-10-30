using Employees.Database.DomainModels;

namespace Employees.ViewModels
{
    public class EmployeeUpdateResponseViewModel : BaseEmployeeViewModel
    {
        public int Id { get; set; }

        public List<Department> Departments { get; set; }
    }
}
