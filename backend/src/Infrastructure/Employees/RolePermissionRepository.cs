using Application.Roles;
using Business_Logic.Employees;
using Infrastructure.Database;

namespace Infrastructure.Employees;

/// <summary>
/// EF Core-backed implementation of IRolePermissionRepository.
/// </summary>
public class RolePermissionRepository : IRolePermissionRepository
{
    private readonly AppDbContext _context;

    public RolePermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<RolePermission> GetByRoleId(int roleId)
    {
        return _context.RolePermissions.Where(rp => rp.RoleId == roleId).ToList();
    }

    public RolePermission? Get(int roleId, int permissionId)
    {
        return _context.RolePermissions
            .FirstOrDefault(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
    }

    public void Add(RolePermission rolePermission)
    {
        _context.RolePermissions.Add(rolePermission);
        _context.SaveChanges();
    }

    public void Delete(RolePermission rolePermission)
    {
        _context.RolePermissions.Remove(rolePermission);
        _context.SaveChanges();
    }
}
