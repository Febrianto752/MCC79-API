using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
{
    public UniversityRepository(BookingDBContext context) : base(context)
    {
    }

    //public ICollection<University> GetAll()
    //{
    //    return _context.Set<University>().ToList();
    //    //return new List<University>() { new University() { Name = "test" } };
    //}

    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>().Where(u => u.Name.Contains(name));
    }


}