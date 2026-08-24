using Application.Roles;
using Business_Logic.Employees;
using Infrastructure.Database;

namespace Infrastructure.Employees;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Role> GetAll()
    {
        return _context.Roles.ToList();
    }

    public Role? GetById(int roleId)
    {
        return _context.Roles.FirstOrDefault(role => role.RoleId == roleId);
    }

    public void Add(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public void Update(Role role)
    {
        _context.SaveChanges();
    }

    public void Delete(Role role)
    {
        _context.Roles.Remove(role);
        _context.SaveChanges();
    }
}
