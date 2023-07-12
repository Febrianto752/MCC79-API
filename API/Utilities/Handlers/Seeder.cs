using API.Models;
using API.Utilities.Enums;

namespace API.Utilities.Handlers;

public class Seeder
{

    //public Faker<Employee> GenerateEmployees()
    //{
    //var employees = new List<Employee>();

    //var employeesFaker = new Faker<Employee>()
    //                    .RuleFor(u => u.Guid, _ => Guid.NewGuid())
    //                    .RuleFor(u => u.FirstName, faker => faker.Name.FirstName())
    //                    .RuleFor(u => u.LastName, faker => faker.Name.LastName())
    //                    .RuleFor(u => u.BirthDate, faker => faker.Person.DateOfBirth)
    //                    .RuleFor(u => u.HiringDate, (faker, u) => faker.Date.Between(u.BirthDate.AddYears(10), DateTime.Now))
    //                    .RuleFor(u => u.Nik, faker => faker.Random.Number(1, 10000).ToString())
    //                    .RuleFor(u => u.Email, faker => faker.Person.Email)
    //                    .RuleFor(u => u.Gender, faker => faker.PickRandom<GenderEnum>())
    //                    .RuleFor(u => u.PhoneNumber, faker => faker.Phone.PhoneNumber("+628##-####-####"))
    //                    .RuleFor(u => u.CreatedDate, _ => DateTime.Now)
    //                    .RuleFor(u => u.ModifiedDate, _ => DateTime.Now);

    ////var employeeSeeds = employeesFaker.Generate(10);

    //return employeesFaker;
    //}

    public List<Employee> GenerateEmployees()
    {
        var employees = new List<Employee>()
        {
            new Employee(){
                Guid = Guid.NewGuid(),
                Nik = "111111",
                FirstName = "Febrianto",
                LastName = "",
                BirthDate = new DateTime(2000, 02, 20),
                Gender = GenderEnum.Male,
                HiringDate = new DateTime(2023, 06, 01),
                Email = "febrianto@gmail.com",
                PhoneNumber = "0812366363",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Employee(){
                Guid = Guid.NewGuid(),
                Nik = "111112",
                FirstName = "Leonardo",
                LastName = "Fransisco",
                BirthDate = new DateTime(2000, 03, 15),
                Gender = GenderEnum.Male,
                HiringDate = new DateTime(2023, 06, 01),
                Email = "leonardo@gmail.com",
                PhoneNumber = "08123888363",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Employee(){
                Guid = Guid.NewGuid(),
                Nik = "111113",
                FirstName = "Anisa",
                LastName = "Dwi",
                BirthDate = new DateTime(2001, 07, 20),
                Gender = GenderEnum.Female,
                HiringDate = new DateTime(2023, 05, 01),
                Email = "anisa@gmail.com",
                PhoneNumber = "0812366344",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Employee(){
                Guid = Guid.NewGuid(),
                Nik = "111114",
                FirstName = "Lita Dwi",
                LastName = "Indriani",
                BirthDate = new DateTime(1999, 012, 20),
                Gender = GenderEnum.Female,
                HiringDate = new DateTime(2023, 05, 01),
                Email = "lita@gmail.com",
                PhoneNumber = "081236636323",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Employee(){
                Guid = Guid.NewGuid(),
                Nik = "111115",
                FirstName = "Messi",
                LastName = "Lionel",
                BirthDate = new DateTime(1997, 02, 11),
                Gender = GenderEnum.Male,
                HiringDate = new DateTime(2023, 06, 01),
                Email = "messi@gmail.com",
                PhoneNumber = "081239926363",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
        };

        return new List<Employee>();
    }

    public List<University> GenerateUniversities()
    {
        var universities = new List<University>
        {
            new University()
            {
                Guid = Guid.NewGuid(),
                Code = "UGM",
                Name = "Universitas Gadjah Mada",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            },
            new University()
            {
                Guid = Guid.NewGuid(),
                Code = "UI",
                Name = "Universitas Indonesia",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            },
            new University()
            {
                Guid = Guid.NewGuid(),
                Code = "UBJ",
                Name = "Universitas Bhayangkara Jakarta Raya",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            },
            new University()
            {
                Guid = Guid.NewGuid(),
                Code = "UB",
                Name = "Universitas Brawijaya",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            },
        };

        return universities;
    }

    public List<Education> GenerateEducations()
    {
        var educations = new List<Education>{
            new Education
            {
                Guid = Guid.NewGuid(),
                Major = "Informatika",
                Degree = "Bachelor",

            },
        };

        return new List<Education>();
    }
}

