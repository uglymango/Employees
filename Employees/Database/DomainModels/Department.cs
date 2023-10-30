namespace Employees.Database.DomainModels
{
    public class Department
    {
        public Department(int id, string name) { 
        
            Id = id;
            departmentName = name;
        
        }

        public Department() { }

        public int Id { get; set; }
        public string departmentName { get; set; }
    } 

}
