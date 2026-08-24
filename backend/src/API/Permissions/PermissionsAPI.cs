// TODO: implement Permissions API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Permissions):
//   GET    /permissions            -> GetPermission.GetAll()
//   GET    /permissions/{id}       -> GetPermission.GetById(permissionId)
//   POST   /permissions            -> CreatePermission.Execute(permissionName)  (permissionId is DB-generated)
//   PUT    /permissions/{id}       -> UpdatePermission.Execute(permissionId, permissionName)
//   DELETE /permissions/{id}       -> DeletePermission.Execute(permissionId)
//
// See API/Roles/RolesAPI.cs for assigning permissions to roles.
//
// DI (program.cs): register IPermissionRepository -> PermissionRepository (AddScoped, needs
// AppDbContext), plus AddScoped for CreatePermission/GetPermission/UpdatePermission/DeletePermission.
