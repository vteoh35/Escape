// TODO: implement Tags API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Tags):
//   GET    /tags            -> GetTag.GetAll()
//   GET    /tags/{id}       -> GetTag.GetById(tagId)
//   POST   /tags            -> CreateTag.Execute(tagName)   (tagId is DB-generated, don't take it as input)
//   PUT    /tags/{id}       -> UpdateTag.Execute(tagId, tagName)
//   DELETE /tags/{id}       -> DeleteTag.Execute(tagId)
//
// Task tagging (Application.Tags):
//   GET    /tasks/{id}/tags           -> GetTaskTags.Execute(taskId)
//   POST   /tasks/{id}/tags/{tagId}   -> TagTask.Execute(taskId, tagId)
//   DELETE /tasks/{id}/tags/{tagId}   -> UntagTask.Execute(taskId, tagId)
//   (these could live here or in TasksAPI.cs -- pick whichever keeps routes easier to find)
//
// DI (program.cs): register ITagRepository -> TagRepository, ITaskTagRepository -> TaskTagRepository,
// IProjectTagRepository -> ProjectTagRepository (all AddScoped, need AppDbContext), plus AddScoped
// for each use case class above.
