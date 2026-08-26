namespace Application.Priorities;

/// <summary>
/// Deletes a priority. Returns false if the id doesn't exist.
/// </summary>
public class DeletePriority
{
    private readonly IPriorityRepository _priorityRepository;

    public DeletePriority(IPriorityRepository priorityRepository)
    {
        _priorityRepository = priorityRepository;
    }

    public bool Execute(int priorityId)
    {
        var priority = _priorityRepository.GetById(priorityId);

        if (priority == null)
        {
            return false;
        }

        _priorityRepository.Delete(priority);

        return true;
    }
}
