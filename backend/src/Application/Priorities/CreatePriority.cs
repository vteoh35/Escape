using Business_Logic.Priorities;

namespace Application.Priorities;

public class CreatePriority
{
    private readonly IPriorityRepository _priorityRepository;

    public CreatePriority(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public Priority Execute(int priorityId, string priorityName)
    {
        var priority = new Priority
        {
            PriorityId = priorityId,
            PriorityName = priorityName
        };

        _priorityRepository.Add(priority);

        return priority;
    }
}
