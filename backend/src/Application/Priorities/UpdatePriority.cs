using Business_Logic.Priorities;

namespace Application.Priorities;

public class UpdatePriority
{
    private readonly IPriorityRepository _priorityRepository;

    public UpdatePriority(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public Priority? Execute(int priorityId, string priorityName)
    {
        var priority = _priorityRepository.GetById(priorityId);

        if (priority == null)
        {
            return null;
        }

        priority.PriorityName = priorityName;

        _priorityRepository.Update(priority);

        return priority;
    }
}
