namespace API.DTOs.Auth
{
    public class UserSignedDto
    {
        public Guid Guid { get; set; }
        public string? Nik { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
    }
}
