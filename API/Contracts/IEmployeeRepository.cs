using API.Models;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGeneralRepository<Employee>
    {
        public Employee? GetByEmailAndPhoneNumber(string data);
        public Employee? GetByEmail(string email);
        public string? GetLastEmpoyeeNik();
    }
}
