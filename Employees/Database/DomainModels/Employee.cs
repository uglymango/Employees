namespace Employees.Database.DomainModels
{
    public class Employee
    {

        public Employee()
            : this(default, default, default, default, default, default, default, default, default)
        {

        }
        public Employee(int id, string name, string surname, string fathername, string email, string idNumber, string employeeID, string photo, int? departamentID)
        {
            Id = id;
            firstName = name;
            Surname = surname;
            fatherName = fathername;
            Email = email;
            personalId = idNumber;
            Photo = photo;
            employeeId = employeeID;
            departamentId = departamentID;
        }

        public int Id { get; set; }
        public string firstName { get; set; }
        public string Surname { get; set; }
        public string fatherName { get; set; }
        public string Email { get; set; }
        public string personalId { get; set; }
        public string employeeId { get; set; }
        public string Photo { get; set; }
        public int? departamentId { get; set; }
        public Department Department { get; internal set; }
    }
}
