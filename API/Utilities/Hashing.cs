namespace API.Utilities;

public class Hashing
{
    private static string GenerateSalt()
    {
        return BCrypt.Net.BCrypt.GenerateSalt(12);
    }

    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
    }

    public bool ValidatePassword(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}

