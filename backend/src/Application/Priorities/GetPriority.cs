using Business_Logic.Priorities;

namespace Application.Priorities;

public class GetPriority
{
    private readonly IPriorityRepository _priorityRepository;

    public GetPriority(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public List<Priority> GetAll()
    {
        return _priorityRepository.GetAll();
    }

    public Priority? GetById(int priorityId)
    {
        return _priorityRepository.GetById(priorityId);
    }
}
