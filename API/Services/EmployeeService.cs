using API.Contracts;
using API.DTOs.Employees;
using API.Models;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<EmployeeDto>? GetEmployee()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return null; // No employees found
        }

        var toDto = employees.Select(employee =>
                                    new EmployeeDto
                                    {
                                        GUID = employee.GUID,
                                        NIK = employee.NIK,
                                        FirstName = employee.FirstName,
                                        LastName = employee.LastName,
                                        Birthdate = employee.Birthdate,
                                        Gender = employee.Gender,
                                        HiringDate = employee.HiringDate,
                                        Email = employee.Email,
                                        PhoneNumber = employee.PhoneNumber
                                    }).ToList();

        return toDto; // Universities found
    }


    public EmployeeDto? GetEmployee(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return null; // Employee not found
        }

        var toDto = new EmployeeDto
        {
            GUID = employee.GUID,
            NIK = employee.NIK,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Birthdate = employee.Birthdate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        };

        return toDto; // Universities found
    }

    public EmployeeDto? CreateEmployee(NewEmployeeDto newEmployeeDto)
    {
        var employee = new Employee
        {
            GUID = new Guid(),
            NIK = GenerateNIK(),
            FirstName = newEmployeeDto.FirstName,
            LastName = newEmployeeDto.LastName ?? "",
            Birthdate = newEmployeeDto.Birthdate,
            Gender = newEmployeeDto.Gender,
            HiringDate = newEmployeeDto.HiringDate,
            Email = newEmployeeDto.Email,
            PhoneNumber = newEmployeeDto.PhoneNumber
        };

        var createdEmployee = _employeeRepository.Create(employee);
        if (createdEmployee is null)
        {
            return null; // Employee not created
        }

        var toDto = new EmployeeDto
        {
            GUID = createdEmployee.GUID,
            NIK = createdEmployee.NIK,
            FirstName = createdEmployee.FirstName,
            LastName = createdEmployee.LastName,
            Birthdate = createdEmployee.Birthdate,
            Gender = createdEmployee.Gender,
            HiringDate = createdEmployee.HiringDate,
            Email = createdEmployee.Email,
            PhoneNumber = createdEmployee.PhoneNumber
        };

        return toDto; // Employee created
    }

    public int UpdateEmployee(EmployeeDto updateEmployeeDto)
    {
        var isExist = _employeeRepository.IsExist(updateEmployeeDto.GUID);
        if (!isExist)
        {
            return -1; // Employee not found
        }

        var getEmployee = _employeeRepository.GetByGuid(updateEmployeeDto.GUID);

        var employee = new Employee
        {
            GUID = updateEmployeeDto.GUID,
            NIK = updateEmployeeDto.NIK,
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            Birthdate = updateEmployeeDto.Birthdate,
            Gender = updateEmployeeDto.Gender,
            HiringDate = updateEmployeeDto.HiringDate,
            Email = updateEmployeeDto.Email,
            PhoneNumber = updateEmployeeDto.PhoneNumber,
            ModifiedDate = DateTime.Now,
            CreatedDate = getEmployee!.CreatedDate
        };


        var isUpdate = _employeeRepository.Update(employee);
        if (!isUpdate)
        {
            return 0; // Employee not updated
        }

        return 1;

    }

    public int DeleteEmployee(Guid guid)
    {
        var isExist = _employeeRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Employee not found
        }

        var employee = _employeeRepository.GetByGuid(guid);
        var isDelete = _employeeRepository.Delete(employee!.GUID);
        if (!isDelete)
        {
            return 0; // Employee not deleted
        }

        return 1;
    }

    public string GenerateNIK()
    {
        var lastEmployee = _employeeRepository.GetAll().OrderBy(e => e.NIK).Last();
        Console.WriteLine(lastEmployee.FirstName);

        if (lastEmployee is null)
        {
            return "1111";
        }
        else
        {
            int tmp = int.Parse(lastEmployee.NIK) + 1;
            Console.WriteLine(tmp);
            return (int.Parse(lastEmployee.NIK) + 1).ToString();
        }
    }


}

