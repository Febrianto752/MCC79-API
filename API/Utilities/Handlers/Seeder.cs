using API.Models;
using API.Utilities.Enums;
using Bogus;
using Bogus.Extensions.Denmark;

namespace API.Utilities.Handlers;

public class Seeder
{
    public Faker<Employee> GenerateEmployees()
    {
        //var employees = new List<Employee>();

        var employeesFaker = new Faker<Employee>()
                            //.RuleFor(u => u.Guid, _ => new Guid())
                            .RuleFor(u => u.FirstName, faker => faker.Name.FirstName())
                            .RuleFor(u => u.LastName, faker => faker.Name.LastName())
                            .RuleFor(u => u.BirthDate, faker => faker.Person.DateOfBirth)
                            .RuleFor(u => u.HiringDate, (faker, u) => faker.Date.Between(u.BirthDate.AddYears(10), DateTime.Now))
                            .RuleFor(u => u.Nik, faker => faker.Person.Cpr())
                            .RuleFor(u => u.Email, faker => faker.Person.Email)
                            .RuleFor(u => u.Gender, faker => faker.PickRandom<GenderEnum>())
                            .RuleFor(u => u.PhoneNumber, faker => faker.Person.Phone);
        //.RuleFor(u => u.CreatedDate, _ => DateTime.Now)
        //.RuleFor(u => u.ModifiedDate, _ => DateTime.Now);

        //var employeeSeeds = employeesFaker.Generate(10);

        return employeesFaker;
    }
}

