using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;

namespace API.Services;
public class AccountRoleService
{
    private readonly IAccountRoleRepository _accountRepository;

    public AccountRoleService(IAccountRoleRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public IEnumerable<AccountRoleDto>? GetAccountRole()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return null; // No accounts found
        }

        var toDto = accounts.Select(account =>
                                            new AccountRoleDto
                                            {
                                                GUID = account.GUID,
                                                AccountGUID = account.AccountGUID,
                                                RoleGUID = account.RoleGUID,

                                            }).ToList();

        return toDto; // Universities found
    }

    public AccountRoleDto? GetAccountRole(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return null; // AccountRole not found
        }

        var toDto = new AccountRoleDto
        {
            GUID = account.GUID,
            AccountGUID = account.AccountGUID,
            RoleGUID = account.RoleGUID,
        };

        return toDto; // Universities found
    }

    public AccountRoleDto? CreateAccountRole(NewAccountRoleDto newAccountRoleDto)
    {
        var account = new AccountRole
        {
            GUID = new Guid(),
            AccountGUID = newAccountRoleDto.AccountGUID,
            RoleGUID = newAccountRoleDto.RoleGUID,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var createdAccountRole = _accountRepository.Create(account);
        if (createdAccountRole is null)
        {
            return null; // AccountRole not created
        }

        var toDto = new AccountRoleDto
        {
            GUID = createdAccountRole.GUID,
            AccountGUID = createdAccountRole.AccountGUID,
            RoleGUID = createdAccountRole.RoleGUID,
        };

        return toDto; // AccountRole created
    }

    public int UpdateAccountRole(AccountRoleDto updateAccountRoleDto)
    {
        var isExist = _accountRepository.IsExist(updateAccountRoleDto.GUID);
        if (!isExist)
        {
            return -1; // AccountRole not found
        }

        var getAccountRole = _accountRepository.GetByGuid(updateAccountRoleDto.GUID);

        var account = new AccountRole
        {
            GUID = updateAccountRoleDto.GUID,
            AccountGUID = updateAccountRoleDto.AccountGUID,
            RoleGUID = updateAccountRoleDto.RoleGUID,
            ModifiedDate = DateTime.Now,
            CreatedDate = getAccountRole!.CreatedDate
        };


        var isUpdate = _accountRepository.Update(account);
        if (!isUpdate)
        {
            return 0; // AccountRole not updated
        }

        return 1;

    }

    public int DeleteAccountRole(Guid guid)
    {
        var isExist = _accountRepository.IsExist(guid);
        if (!isExist)
        {
            return -1; // AccountRole not found
        }

        var account = _accountRepository.GetByGuid(guid);
        var isDelete = _accountRepository.Delete(account!.GUID);
        if (!isDelete)
        {
            return 0; // AccountRole not deleted
        }

        return 1;
    }
}

