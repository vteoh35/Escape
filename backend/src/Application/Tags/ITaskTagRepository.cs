using Business_Logic.Tags;

namespace Application.Tags;

public interface ITaskTagRepository
{
    List<TaskTag> GetByTaskId(string taskId);
    TaskTag? Get(string taskId, int tagId);
    void Add(TaskTag taskTag);
    void Delete(TaskTag taskTag);
}
