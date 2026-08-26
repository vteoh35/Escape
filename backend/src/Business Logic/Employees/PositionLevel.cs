namespace Business_Logic.Employees;

/// <summary>
/// A job level (e.g. "Senior Developer") referenced by Employee.EmployeeLevel. Small, admin-configurable lookup table.
/// </summary>
public class PositionLevel
{
    public string Position { get; set; }
    public int Level { get; set; }
}