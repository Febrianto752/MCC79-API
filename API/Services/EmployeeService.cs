using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
    }

    public IEnumerable<GetEmployeeDto>? GetEmployee()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return null; // No employee  found
        }

        var toDto = employees.Select(employee =>
                                            new GetEmployeeDto
                                            {
                                                Guid = employee.Guid,
                                                Nik = employee.Nik,
                                                BirthDate = employee.BirthDate,
                                                Email = employee.Email,
                                                FirstName = employee.FirstName,
                                                LastName = employee.LastName,
                                                Gender = employee.Gender,
                                                HiringDate = employee.HiringDate,
                                                PhoneNumber = employee.PhoneNumber
                                            }).ToList();

        return toDto; // employee found
    }

    public GetEmployeeDto? GetEmployee(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return null; // employee not found
        }

        var toDto = new GetEmployeeDto
        {
            Guid = employee.Guid,
            Nik = employee.Nik,
            BirthDate = employee.BirthDate,
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            PhoneNumber = employee.PhoneNumber
        };

        return toDto; // employees found
    }

    public GetEmployeeDto? CreateEmployee(NewEmployeeDto newEmployeeDto)
    {
        var employee = new Employee
        {
            Guid = new Guid(),
            PhoneNumber = newEmployeeDto.PhoneNumber,
            FirstName = newEmployeeDto.FirstName,
            LastName = newEmployeeDto.LastName,
            Gender = newEmployeeDto.Gender,
            HiringDate = newEmployeeDto.HiringDate,
            Email = newEmployeeDto.Email,
            BirthDate = newEmployeeDto.BirthDate,
            Nik = GenerateHandler.Nik(),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdEmployee = _employeeRepository.Create(employee);
        if (createdEmployee is null)
        {
            return null; // employee not created
        }

        var toDto = new GetEmployeeDto
        {
            Guid = createdEmployee.Guid,
            Nik = createdEmployee.Nik,
            BirthDate = createdEmployee.BirthDate,
            Email = createdEmployee.Email,
            FirstName = createdEmployee.FirstName,
            LastName = createdEmployee.LastName,
            Gender = createdEmployee.Gender,
            HiringDate = createdEmployee.HiringDate,
            PhoneNumber = createdEmployee.PhoneNumber
        };

        return toDto; // employee created
    }

    public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
    {
        var isExist = _employeeRepository.IsExist(updateEmployeeDto.Guid);
        if (!isExist)
        {
            return -1; // employee not found
        }

        var getEmployee = _employeeRepository.GetByGuid(updateEmployeeDto.Guid);

        var employee = new Employee
        {
            Guid = updateEmployeeDto.Guid,
            PhoneNumber = updateEmployeeDto.PhoneNumber,
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            Gender = updateEmployeeDto.Gender,
            HiringDate = updateEmployeeDto.HiringDate,
            Email = updateEmployeeDto.Email,
            BirthDate = updateEmployeeDto.BirthDate,
            Nik = updateEmployeeDto.Nik,
            ModifiedDate = DateTime.Now,
            CreatedDate = getEmployee!.CreatedDate
        };

        var isUpdate = _employeeRepository.Update(employee);
        if (!isUpdate)
        {
            return 0; // employee not updated
        }

        return 1;
    }

    public int DeleteEmployee(Guid guid)
    {
        var isExist = _employeeRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // employee not found
        }

        var employee = _employeeRepository.GetByGuid(guid);
        var isDelete = _employeeRepository.Delete(employee!);
        if (!isDelete)
        {
            return 0; // employee not deleted
        }

        return 1;
    }

    //public string GenerateNik()
    //{
    //    var lastEmployee = _employeeRepository.GetAll().OrderBy(e => e.Nik).LastOrDefault();

    //    if (lastEmployee is null)
    //    {
    //        return "1111";
    //    }
    //    else
    //    {
    //        int tmp = int.Parse(lastEmployee.Nik) + 1;
    //        Console.WriteLine(tmp);
    //        return (int.Parse(lastEmployee.Nik) + 1).ToString();
    //    }
    //}

    public IEnumerable<EmployeeEducationDto>? GetMaster()
    {
        var master = (from employee in _employeeRepository.GetAll()
                      join education in _educationRepository.GetAll()
                      on employee.Guid equals education.Guid
                      join university in _universityRepository.GetAll()
                      on education.UniversityGuid equals university.Guid
                      select new EmployeeEducationDto
                      {
                          Guid = employee.Guid,
                          FullName = employee.FirstName + " " + employee.LastName,
                          Nik = employee.Nik,
                          BirthDate = employee.BirthDate,
                          Email = employee.Email,
                          Gender = employee.Gender,
                          HiringDate = employee.HiringDate,
                          PhoneNumber = employee.PhoneNumber,
                          Major = education.Major,
                          Degree = education.Degree,
                          Gpa = education.Gpa,
                          UniversityName = university.Name
                      }
                      ).ToList();

        if (master.Count == 0)
        {
            return null;
        }

        return master;
    }

    public EmployeeEducationDto? GetMasterByGuid(Guid guid)
    {
        var master = GetMaster();

        if (master == null) return null;

        var masterByGuid = master.FirstOrDefault(master => master.Guid == guid);

        return masterByGuid;
    }

}


