// TODO: implement Roles API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Roles):
//   GET    /roles            -> GetRole.GetAll()
//   GET    /roles/{id}       -> GetRole.GetById(roleId)
//   POST   /roles            -> CreateRole.Execute(roleName)   (roleId is DB-generated, don't take it as input)
//   PUT    /roles/{id}       -> UpdateRole.Execute(roleId, roleName)
//   DELETE /roles/{id}       -> DeleteRole.Execute(roleId)
//
// Role permissions (Application.Roles):
//   GET    /roles/{id}/permissions                -> GetRolePermissions.Execute(roleId)
//   POST   /roles/{id}/permissions/{permissionId}   -> AssignPermissionToRole.Execute(roleId, permissionId)
//   DELETE /roles/{id}/permissions/{permissionId}   -> RemovePermissionFromRole.Execute(roleId, permissionId)
//
// See also API/Permissions/PermissionsAPI.cs for managing permissions themselves.
// An employee's role is Employee.RoleId -- see API/Employees/EmployeesAPI.cs's TODO for assigning it.
//
// DI (program.cs): register IRoleRepository -> RoleRepository, IRolePermissionRepository ->
// RolePermissionRepository (both AddScoped, need AppDbContext), plus AddScoped for
// CreateRole/GetRole/UpdateRole/DeleteRole/AssignPermissionToRole/RemovePermissionFromRole/GetRolePermissions.
