using Business_Logic.Tags;

namespace Application.Tags;

public class GetTaskTags
{
    private readonly ITaskTagRepository _taskTagRepository;

    public GetTaskTags(ITaskTagRepository taskTagRepository)
    {
        _taskTagRepository = taskTagRepository;
    }

    public List<TaskTag> Execute(string taskId)
    {
        return _taskTagRepository.GetByTaskId(taskId);
    }
}
