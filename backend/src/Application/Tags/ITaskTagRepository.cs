using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Data access contract for task-tag assignments (TaskTag), implemented in Infrastructure against Postgres.
/// </summary>
public interface ITaskTagRepository
{
    List<TaskTag> GetByTaskId(string taskId);
    TaskTag? Get(string taskId, int tagId);
    void Add(TaskTag taskTag);
    void Delete(TaskTag taskTag);
}
