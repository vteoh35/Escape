namespace Application.Authentication;

/// <summary>
/// Hashes and verifies passwords. Implemented in Infrastructure using PBKDF2.
/// </summary>
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}
