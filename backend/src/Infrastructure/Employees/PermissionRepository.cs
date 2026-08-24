using Application.Permissions;
using Business_Logic.Employees;
using Infrastructure.Database;

namespace Infrastructure.Employees;

public class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Permission> GetAll()
    {
        return _context.Permissions.ToList();
    }

    public Permission? GetById(int permissionId)
    {
        return _context.Permissions.FirstOrDefault(permission => permission.PermissionId == permissionId);
    }

    public void Add(Permission permission)
    {
        _context.Permissions.Add(permission);
        _context.SaveChanges();
    }

    public void Update(Permission permission)
    {
        _context.SaveChanges();
    }

    public void Delete(Permission permission)
    {
        _context.Permissions.Remove(permission);
        _context.SaveChanges();
    }
}
