using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Applies a tag to a task.
/// </summary>
public class TagTask
{
    private readonly ITaskTagRepository _taskTagRepository;

    public TagTask(ITaskTagRepository taskTagRepository)
    {
        _taskTagRepository = taskTagRepository;
    }

    public TaskTag Execute(string taskId, int tagId)
    {
        var taskTag = new TaskTag
        {
            TaskId = taskId,
            TagId = tagId
        };

        _taskTagRepository.Add(taskTag);

        return taskTag;
    }
}
