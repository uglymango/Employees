using Employees.Database.DomainModels;
using Npgsql;

namespace Employees.Database.Repositories
{
    public class EmployeeRepository : IDisposable
    {
        private readonly NpgsqlConnection _npgsqlConnection;


        public EmployeeRepository()
        {
            _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.connectionString);
            _npgsqlConnection.Open();
        }

        public void Dispose()
        {
            _npgsqlConnection.Dispose();
        }

        public List<Employee> GetAll()
        {
            var selectQuery = "SELECT * FROM employees ORDER BY name";

            using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
            using NpgsqlDataReader dataReader = command.ExecuteReader();

            List<Employee> employees = new List<Employee>();

            while (dataReader.Read())
            {
                Employee employee = new Employee
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    firstName = Convert.ToString(dataReader["name"]),
                    Surname = Convert.ToString(dataReader["surname"]),
                    fatherName = Convert.ToString(dataReader["fathername"]),
                    Email = Convert.ToString(dataReader["email"]),
                    personalId = Convert.ToString(dataReader["personalid"]),
                    employeeId = Convert.ToString(dataReader["employeeid"]),
                    Photo = Convert.ToString(dataReader["photo"]),
                    departamentId = Convert.ToInt32(dataReader["rating"]),
                };

                employees.Add(employee);
            }

            return employees;
        }

        public List<Employee> GetAllWithDepartments()
        {
            var selectQuery = "SELECT \r\n\tp.\"id\" employeeId,\r\n\tp.\"name\" employeeName,\r\n\tp.\"surname\" employeeSurname,\r\n\tp.\"fatherName\" employeeFatherName,\r\n\tp.\"email\" employeeEmail,\r\n\tp.\"personalId\" personalId,\r\n\tp.\"employeeId\" employeeId,\r\n\tp.\"employeePhoto\" employeePhoto,\r\n\tc.\"id\" departmentId,\r\n\tc.\"departmentName\" departmentName\r\nFROM employees e\r\nLEFT JOIN departments d ON e.\"departentid\"=d.\"id\"\r\nORDER BY e.name";
            using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
            using NpgsqlDataReader dataReader = command.ExecuteReader();

            List<Employee> employees = new List<Employee>();

            while (dataReader.Read())
            {
                Employee employee = new Employee
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    firstName = Convert.ToString(dataReader["name"]),
                    Surname = Convert.ToString(dataReader["surname"]),
                    fatherName = Convert.ToString(dataReader["fathername"]),
                    Email = Convert.ToString(dataReader["email"]),
                    personalId = Convert.ToString(dataReader["personalid"]),
                    employeeId = Convert.ToString(dataReader["employeeid"]),
                    Photo = Convert.ToString(dataReader["photo"]),
                    Department = dataReader["departmentId"] is int
                    ? new Department(Convert.ToInt32(dataReader["departmentId"]), Convert.ToString(dataReader["departmentName"]))
                    : null
                };

                employees.Add(employee);
            }

            return employees;
        }

        public Employee GetById(int id)
        {
            var selectQuery = "$\"SELECT * FROM employees WHERE id={id}\", _npgsqlConnection";

            using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
            using NpgsqlDataReader dataReader = command.ExecuteReader();

            Employee employee = null;

            while (dataReader.Read())
            {
                employee = new Employee
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    firstName = Convert.ToString(dataReader["name"]),
                    Surname = Convert.ToString(dataReader["surname"]),
                    fatherName = Convert.ToString(dataReader["fathername"]),
                    Email = Convert.ToString(dataReader["email"]),
                    personalId = Convert.ToString(dataReader["personalid"]),
                    employeeId = Convert.ToString(dataReader["employeeid"]),
                    Photo = Convert.ToString(dataReader["photo"]),
                    departamentId = Convert.ToInt32(dataReader["rating"]) as int?

                };
            }
            return employee;
        }

        public void Insert(Employee employee)
        {
            string updateQuery =
                "INSERT INTO employees(name, surname, fathername, email, personalid, employeeid, photo, departmentid)" +
                $"VALUES ('{employee.firstName}', {employee.Surname}, {employee.fatherName}, {employee.Email}, {employee.personalId}, {employee.employeeId}, {employee.Photo}, {employee.departamentId})";

            using NpgsqlCommand command = new NpgsqlCommand(updateQuery, _npgsqlConnection);
            command.ExecuteNonQuery();
        }

        public void Update(Employee employee)
        {
            var query =
                    $"UPDATE employees " +
                    $"SET name='{employee.firstName}', surname={employee.Surname}, fatherName={employee.fatherName}, email={employee.Email}, personalId={employee.personalId}, employeeId={employee.employeeId}, photo={employee.Photo}, departmentId={employee.departamentId} " +
                    $"WHERE id={employee.Id}";

            using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
            updateCommand.ExecuteNonQuery();
        }

        public void RemoveById(int id)
        {
            var query = $"DELETE FROM employees WHERE id={id}";

            using NpgsqlCommand updateCommand = new NpgsqlCommand(query, _npgsqlConnection);
            updateCommand.ExecuteNonQuery();
        }


    }
}
