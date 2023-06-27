namespace API.DTOs.Accounts
{
    public class AccountDto
    {
        public Guid GUID { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public string OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
