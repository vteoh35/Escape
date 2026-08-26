namespace Application.Tags;

/// <summary>
/// Removes a tag from a task. Returns false if it wasn't applied.
/// </summary>
public class UntagTask
{
    private readonly ITaskTagRepository _taskTagRepository;

    public UntagTask(ITaskTagRepository taskTagRepository)
    {
        _taskTagRepository = taskTagRepository;
    }

    public bool Execute(string taskId, int tagId)
    {
        var taskTag = _taskTagRepository.Get(taskId, tagId);

        if (taskTag == null)
        {
            return false;
        }

        _taskTagRepository.Delete(taskTag);

        return true;
    }
}
