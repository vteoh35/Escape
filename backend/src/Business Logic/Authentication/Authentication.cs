namespace Business_Logic.Employees;

/// <summary>
/// An employee's login credentials: a one-to-one extension of Employee holding the PBKDF2 password hash.
/// </summary>
public class Authentication
{
    public string EmployeeId { get; set; }
    public string PasswordHash { get; set; }
}