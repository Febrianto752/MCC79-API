using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IGeneralRepository<University>
{
    public IEnumerable<University> GetName(string name);

}