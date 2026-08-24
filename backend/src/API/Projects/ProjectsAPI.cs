// TODO: implement Projects API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs (MapGet/MapPost/MapPut/MapDelete on a WebApplication
// extension method, called from program.cs). Wire each route to the matching Application.Projects
// use case (constructor-injected via minimal API DI).
//
// Core CRUD (Application.Projects):
//   GET    /projects            -> GetProject.GetAll()
//   GET    /projects/{id}       -> GetProject.GetById(projectId)
//   POST   /projects            -> CreateProject.Execute(projectId, name)
//   PUT    /projects/{id}       -> UpdateProject.Execute(projectId, name)
//   DELETE /projects/{id}       -> DeleteProject.Execute(projectId)
//
// Project membership (Application.Projects):
//   GET    /projects/{id}/members               -> GetProjectMembers.Execute(projectId)
//   POST   /projects/{id}/members                -> AddProjectMember.Execute(employeeId, projectId, role)
//   PUT    /projects/{id}/members/{employeeId}   -> UpdateProjectMemberRole.Execute(employeeId, projectId, role)
//   DELETE /projects/{id}/members/{employeeId}   -> RemoveProjectMember.Execute(employeeId, projectId)
//
// Project tags (Application.Tags):
//   GET    /projects/{id}/tags           -> GetProjectTags.Execute(projectId)
//   POST   /projects/{id}/tags/{tagId}   -> TagProject.Execute(projectId, tagId)
//   DELETE /projects/{id}/tags/{tagId}   -> UntagProject.Execute(projectId, tagId)
//
// DI (program.cs): register IProjectRepository -> ProjectRepository,
// IProjectMemberRepository -> ProjectMemberRepository (both AddScoped, both need AppDbContext),
// plus AddScoped for each use case class above.
