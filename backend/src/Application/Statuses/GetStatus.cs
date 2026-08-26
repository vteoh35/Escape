using Business_Logic.Statuses;

namespace Application.Statuses;

/// <summary>
/// Reads statuses, either all of them or by id.
/// </summary>
public class GetStatus
{
    private readonly IStatusRepository _statusRepository;

    public GetStatus(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public List<Status> GetAll()
    {
        return _statusRepository.GetAll();
    }

    public Status? GetById(int statusId)
    {
        return _statusRepository.GetById(statusId);
    }
}
