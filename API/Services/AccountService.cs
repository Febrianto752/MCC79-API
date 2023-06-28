using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Utilities;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly EmployeeService _employeeService;
    private readonly UniversityService _universityService;
    private readonly EducationService _educationService;


    public AccountService(IAccountRepository accountRepository, EmployeeService employeeService,
            UniversityService universityService,
            EducationService educationService)
    {
        _accountRepository = accountRepository;
        _employeeService = employeeService;
        _universityService = universityService;
        _educationService = educationService;
    }

    public RegisterDto? Register(RegisterDto registerDto)
    {

        var employee = new NewEmployeeDto
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName ?? "",
            Birthdate = registerDto.Birthdate,
            Gender = registerDto.Gender,
            HiringDate = registerDto.HiringDate,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber
        };
        Console.WriteLine("1");
        var createdEmployee = _employeeService.CreateEmployee(employee);
        if (createdEmployee is null)
        {
            return null;
        }
        Console.WriteLine("2");

        var university = new NewUniversityDto
        {
            Code = registerDto.UniversityCode,
            Name = registerDto.UniversityName
        };

        var createdUniversity = _universityService.CreateUniversity(university);

        if (createdUniversity is null)
        {
            return null;
        }

        Console.WriteLine("3");

        var education = new EducationDto
        {
            GUID = createdEmployee.GUID,
            Major = registerDto.Major,
            Degree = registerDto.Degree,
            GPA = registerDto.GPA,
            UniversityGUID = createdUniversity.GUID
        };

        var createdEducation = _educationService.CreateEducation(education);
        if (createdEducation is null)
        {
            return null;
        }
        Console.WriteLine("4");
        var account = new AccountDto
        {
            GUID = createdEmployee.GUID,
            Password = Hashing.HashPassword(registerDto.Password),
        };

        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return null;
        }
        Console.WriteLine("5");
        var createdAccount = CreateAccount(account);
        if (createdAccount is null)
        {
            return null;
        }

        Console.WriteLine("6");
        var toDto = new RegisterDto
        {
            FirstName = createdEmployee.FirstName,
            LastName = createdEmployee.LastName,
            Birthdate = createdEmployee.Birthdate,
            Gender = createdEmployee.Gender,
            HiringDate = createdEmployee.HiringDate,
            Email = createdEmployee.Email,
            PhoneNumber = createdEmployee.PhoneNumber,
            Password = createdAccount.Password,
            Major = createdEducation.Major,
            Degree = createdEducation.Degree,
            GPA = createdEducation.GPA,
            UniversityCode = createdUniversity.Code,
            UniversityName = createdUniversity.Name
        };

        return toDto;
    }

    public IEnumerable<AccountDto>? GetAccount()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return null; // No accounts found
        }

        var toDto = accounts.Select(account =>
                                    new AccountDto
                                    {
                                        GUID = account.GUID,
                                        Password = account.Password,
                                        IsDeleted = account.IsDeleted,
                                        OTP = account.OTP,
                                        IsUsed = account.IsUsed,
                                        ExpiredTime = account.ExpiredTime,
                                    }).ToList();

        return toDto; // Universities found
    }

    public AccountDto? GetAccount(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return null; // Account not found
        }

        var toDto = new AccountDto
        {
            GUID = account.GUID,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            OTP = account.OTP,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime,
        };

        return toDto; // Universities found
    }

    public AccountDto? CreateAccount(AccountDto newAccountDto)
    {
        var expiredDate = DateTime.Now.Add(new TimeSpan(25, 20, 55));
        var account = new Account
        {
            GUID = newAccountDto.GUID,
            Password = Hashing.HashPassword(newAccountDto.Password),
            IsDeleted = newAccountDto.IsDeleted,
            //OTP = newAccountDto.OTP,
            OTP = GenerateOTP(),
            IsUsed = newAccountDto.IsUsed,
            //ExpiredTime = newAccountDto.ExpiredTime,
            ExpiredTime = expiredDate,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdAccount = _accountRepository.Create(account);
        if (createdAccount is null)
        {
            return null; // Account not created
        }

        var toDto = new AccountDto
        {
            GUID = account.GUID,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            OTP = account.OTP,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime,
        };

        return toDto; // Account created
    }

    public int UpdateAccount(AccountDto updateAccountDto)
    {
        var isExist = _accountRepository.IsExist(updateAccountDto.GUID);
        if (!isExist)
        {
            return -1; // Account not found
        }

        var getAccount = _accountRepository.GetByGuid(updateAccountDto.GUID);

        var account = new Account
        {
            GUID = updateAccountDto.GUID,
            Password = Hashing.HashPassword(updateAccountDto.Password),
            IsDeleted = updateAccountDto.IsDeleted,
            OTP = updateAccountDto.OTP,
            IsUsed = updateAccountDto.IsUsed,
            ExpiredTime = updateAccountDto.ExpiredTime,
            ModifiedDate = DateTime.Now,
            CreatedDate = getAccount!.CreatedDate
        };

        var isUpdate = _accountRepository.Update(account);
        if (!isUpdate)
        {
            return 0; // Account not updated
        }

        return 1;

    }

    public int DeleteAccount(Guid guid)
    {
        var isExist = _accountRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // Account not found
        }

        var account = _accountRepository.GetByGuid(guid);
        var isDelete = _accountRepository.Delete(account!.GUID);
        if (!isDelete)
        {
            return 0; // Account not deleted
        }

        return 1;
    }

    public string GenerateOTP()
    {
        // Creating object of random class
        Random rand = new Random();

        // Choosing the size of string
        // Using Next() string
        int stringlen = 6;
        int randValue;
        string OTP = "";
        char letter;
        for (int i = 0; i < stringlen; i++)
        {

            // Generating a random number.
            randValue = rand.Next(0, 26);

            // Generating random character by converting
            // the random number into character.
            letter = Convert.ToChar(randValue + 65);

            // Appending the letter to string.
            OTP = OTP + letter;
        }

        return OTP;
    }
}

