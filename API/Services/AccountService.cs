using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Utilities;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
            var account = new Account
            {
                GUID = newAccountDto.GUID,
                Password = Hashing.HashPassword(newAccountDto.Password),
                IsDeleted = newAccountDto.IsDeleted,
                OTP = newAccountDto.OTP,
                IsUsed = newAccountDto.IsUsed,
                ExpiredTime = newAccountDto.ExpiredTime,
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

    }
}
