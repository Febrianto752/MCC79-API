using API.Contracts;
using API.DTOs.Auth;
using API.Models;
using API.Utilities.Handlers;
using System.Data;
using System.Security.Claims;

namespace API.Services;

public class AuthService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly ITokenHandler _tokenHandler;


    public AuthService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, IRoleRepository roleRepository, IAccountRoleRepository accountRoleRepository, ITokenHandler tokenHandler)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _universityRepository = universityRepository;
        _educationRepository = educationRepository;
        _accountRoleRepository = accountRoleRepository;
        _roleRepository = roleRepository;

        _tokenHandler = tokenHandler;
    }

    public RegisterDto Register(RegisterDto registerDto)
    {
        try
        {
            var employee = new Employee
            {
                Guid = new Guid(),
                PhoneNumber = registerDto.PhoneNumber,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName ?? "",
                Gender = registerDto.Gender,
                HiringDate = registerDto.HiringDate,
                Email = registerDto.Email,
                BirthDate = registerDto.BirthDate,
                Nik = GenericNik(),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            var createdEmployee = _employeeRepository.Create(employee);

            if (createdEmployee == null)
            {
                return new RegisterDto();
            }

            var account = new Account
            {
                Guid = createdEmployee.Guid,
                IsDeleted = false,
                IsUsed = true,
                Otp = 0,
                ExpiredTime = DateTime.Now,
                Password = HashingHandler.HashPassword(registerDto.Password),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            var createdAccount = _accountRepository.Create(account);

            if (createdAccount == null)
            {
                return new RegisterDto();
            }

            var roleUser = _roleRepository.GetAll().FirstOrDefault(role => role.Name == "Admin");

            if (roleUser == null)
            {
                return new RegisterDto();
            }

            var accountRole = new AccountRole
            {
                AccountGuid = createdAccount.Guid,
                RoleGuid = roleUser.Guid
            };
            var createdAccountRole = _accountRoleRepository.Create(accountRole);


            var university = new University
            {
                Guid = new Guid(),
                Code = registerDto.UniversityCode,
                Name = registerDto.UniversityName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            var createdUniversity = _universityRepository.Create(university);

            if (createdUniversity == null)
            {
                return new RegisterDto();
            }

            var education = new Education
            {
                Guid = createdEmployee.Guid,
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                Gpa = registerDto.Gpa,
                UniversityGuid = university.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            var createdEducation = _educationRepository.Create(education);

            if (createdEducation == null)
            {
                return new RegisterDto();
            }

            var toDto = new RegisterDto
            {
                FirstName = createdEmployee.FirstName,
                LastName = createdEmployee.LastName,
                BirthDate = createdEmployee.BirthDate,
                Gender = createdEmployee.Gender,
                HiringDate = createdEmployee.HiringDate,
                Email = createdEmployee.Email,
                PhoneNumber = createdEmployee.PhoneNumber,
                Major = createdEducation.Major,
                Degree = createdEducation.Degree,
                Gpa = createdEducation.Gpa,
                UniversityCode = createdUniversity.Code,
                UniversityName = createdUniversity.Name,
                Password = createdAccount.Password,
                ConfirmPassword = createdAccount.Password
            };
            return toDto;
        }
        catch
        {
            return null;
        }

    }

    public string GenericNik()
    {
        var employees = _employeeRepository.GetAll().OrderBy(e => e.Nik).LastOrDefault();
        if (employees is null)
        {
            return "111111";
        }

        var nik = int.Parse(employees.Nik) + 1;

        return nik.ToString();
    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var isExist = _employeeRepository.GetByEmail(changePasswordDto.Email);
        if (isExist is null)
        {
            return -1; // Account not found
        }

        var getAccount = _accountRepository.GetByGuid(isExist.Guid);
        if (getAccount.Otp != changePasswordDto.Otp)
        {
            return 0;
        }

        if (getAccount.IsUsed == true)
        {
            return 1;
        }

        if (getAccount.ExpiredTime < DateTime.Now)
        {
            return 2;
        }

        var account = new Account
        {
            Guid = getAccount.Guid,
            IsUsed = getAccount.IsUsed,
            IsDeleted = getAccount.IsDeleted,
            ModifiedDate = DateTime.Now,
            CreatedDate = getAccount!.CreatedDate,
            Otp = getAccount.Otp,
            ExpiredTime = getAccount.ExpiredTime,
            Password = HashingHandler.HashPassword(changePasswordDto.NewPassword),
        };

        var isUpdate = _accountRepository.Update(account);
        if (!isUpdate)
        {
            return 0; // Account not updated
        }

        return 3;
    }


    public string SigninAccount(SigninDto signinDto)
    {
        var employee = _employeeRepository.GetByEmail(signinDto.Email);
        if (employee is null)
            return "0";

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return "0";

        if (!HashingHandler.ValidatePassword(signinDto.Password, account!.Password))
            return "-1";

        try
        {
            var claims = new List<Claim>() {
                new Claim("NIK", employee.Nik),
                new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                new Claim("EmailAddress", signinDto.Email)
            };

            var getAccountRole = _accountRoleRepository.GetAccountRolesByAccountGuid(employee.Guid);
            var getRoleNameByAccountRole = from ar in getAccountRole
                                           join r in _roleRepository.GetAll() on ar.RoleGuid equals r.Guid
                                           select r.Name;

            //foreach (var roleName in getRoleNameByAccountRole)
            //{
            //    claims.Add(new Claim("Role", roleName));
            //}

            claims.AddRange(getRoleNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var getToken = _tokenHandler.GenerateToken(claims);
            return getToken;
        }
        catch
        {
            return "-2";
        }

    }





}



