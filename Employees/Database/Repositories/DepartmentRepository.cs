using Employees.Database.DomainModels;
using Npgsql;

namespace Employees.Database.Repositories
{
    public class DepartmentRepository : IDisposable
    {

        private readonly NpgsqlConnection _npgsqlConnection;

        public DepartmentRepository()
        {
            _npgsqlConnection = new NpgsqlConnection(DatabaseConstants.connectionString);
            _npgsqlConnection.Open();
        }

        public List<Department> GetAll()
        {
            var selectQuery = "SELECT * FROM departments ORDER BY name";

            using NpgsqlCommand command = new NpgsqlCommand(selectQuery, _npgsqlConnection);
            using NpgsqlDataReader dataReader = command.ExecuteReader();

            List<Department> departments = new List<Department>();

            while (dataReader.Read())
            {
                Department department = new Department
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    departmentName = Convert.ToString(dataReader["name"]),
                };

                departments.Add(department);
            }

            return departments;
        }

        public Department GetById(int id)
        {
            using NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM departments WHERE id={id}", _npgsqlConnection);
            using NpgsqlDataReader dataReader = command.ExecuteReader();

            Department department = null;

            while (dataReader.Read())
            {
                department = new Department
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    departmentName = Convert.ToString(dataReader["name"]),
                };
            }

            return department;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
