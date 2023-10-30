using Employees.Database.DomainModels;
using Employees.Database.Repositories;
using Employees.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Pustok.Controllers.Admin;

[Route("admin/employees")]
public class EmployeeController : Controller
{
    private readonly EmployeeRepository _employeeRepository;
    private readonly DepartmentRepository _departmentRepository;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController()
    {
        _employeeRepository = new EmployeeRepository();
        _departmentRepository = new DepartmentRepository();

        var factory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        _logger = factory.CreateLogger<EmployeeController>();
    }

    #region Products

    [HttpGet] //admin/products
    public IActionResult Products()
    {
        return View("Views/Admin/Employees.cshtml", _employeeRepository.GetAllWithDepartments());
    }

    #endregion

    #region Add

    [HttpGet("add")]
    public IActionResult Add()
    {
        var departments = _departmentRepository.GetAll();
        var model = new EmployeeAddResponseViewModel
        {
            Departments = departments
        };

        return View("Views/Admin/EmployeeAdd.cshtml", model);
    }

    [HttpPost("add")]
    public IActionResult Add(EmployeeAddRequestViewModel model)
    {
        if (!ModelState.IsValid)
            return PrepareValidationView("Views/Admin/EmployeeAdd.cshtml");

        if (model.departamentId != null)
        {
            var category = _departmentRepository.GetById(model.departamentId.Value);
            if (category == null)
            {
                ModelState.AddModelError("departmentId", "Department doesn't exist");

                return PrepareValidationView("Views/Admin/EmployeeAdd.cshtml");
            }
        }

        var employee = new Employee
        {
            firstName = model.firstName,
            Surname = model.Surname,
            fatherName = model.fatherName,
            Email = model.Email,

        };

        try
        {
            _employeeRepository.Insert(employee);
        }
        catch (PostgresException e)
        {
            _logger.LogError(e, "Postgresql Exception");

            throw e;
        }

        return RedirectToAction("Employees");
    }

    #endregion

    #region Edit

    [HttpGet("edit")]
    public IActionResult Edit(int id)
    {
        Employee employee = _employeeRepository.GetById(id);
        if (employee == null)
            return NotFound();


        var model = new EmployeeUpdateResponseViewModel
        {
            firstName = employee.firstName,
            Surname = employee.Surname,
            fatherName = employee.fatherName,
            Email = employee.Email,
            personalId = employee.personalId,
            employeeId = employee.employeeId,
            Photo = employee.Photo,
            departamentId = employee.departamentId
        };

        return View("Views/Admin/EmployeeEdit.cshtml", model);
    }

    [HttpPost("edit")]
    public IActionResult Edit(EmployeeUpdateRequestViewModel model)
    {
        if (!ModelState.IsValid)
            return PrepareValidationView("Views/Admin/EmployeeEdit.cshtml");

        if (model.departamentId != null)
        {
            var category = _departmentRepository.GetById(model.departamentId.Value);
            if (category == null)
            {
                ModelState.AddModelError("departmentId", "Department doesn't exist");

                return PrepareValidationView("Views/Admin/EmployeeAdd.cshtml");
            }
        }

        Employee employee = _employeeRepository.GetById(model.Id);
        if (employee == null)
            return NotFound();


        employee.firstName = model.firstName;
        employee.Surname = model.Surname;
        employee.fatherName = model.fatherName;
        employee.Email = model.Email;
        employee.personalId = model.personalId;
        employee.employeeId = model.employeeId;
        employee.Photo = model.Photo;
        employee.departamentId = model.departamentId;


        try
        {
            _employeeRepository.Update(employee);
        }
        catch (PostgresException e)
        {
            _logger.LogError(e, "Postgresql Exception");

            throw e;
        }


        return RedirectToAction("Employees");
    }

    #endregion

    #region Delete

    [HttpGet("delete")]
    public IActionResult Delete(int id)
    {
        Employee employee = _employeeRepository.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }

        _employeeRepository.RemoveById(id);

        return RedirectToAction("Employees");
    }

    #endregion

    private IActionResult PrepareValidationView(string viewName)
    {
        var departments = _departmentRepository.GetAll();

        var responseViewModel = new EmployeeAddResponseViewModel
        {
            Departments = departments
        };

        return View(viewName, responseViewModel);
    }

    protected override void Dispose(bool disposing)
    {
        _employeeRepository.Dispose();
        _departmentRepository.Dispose();

        base.Dispose(disposing);
    }
}
