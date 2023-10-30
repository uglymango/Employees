using System.ComponentModel.DataAnnotations;

namespace Employees.ViewModels;

    public abstract class BaseEmployeeViewModel
    {
        [Required(ErrorMessage = "You are required to enter Name!")]
        public string firstName { get; set; }
        
        [Required(ErrorMessage = "You are required to enter Surname!")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "You are required to enter Father Name!")]
        public string fatherName { get; set; }
        
        [Required(ErrorMessage = "You are required to enter Email!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "You are required to enter Personal Identification Number!")]
        public string personalId { get; set; }
        
        [Required(ErrorMessage = "You are required to enter Employee Identification Number!")]
        public string employeeId { get; set; }
        
        [Required(ErrorMessage = "You are required to enter Photo!")]
        public string Photo { get; set; }
        public int? departamentId { get; set; }
    }

