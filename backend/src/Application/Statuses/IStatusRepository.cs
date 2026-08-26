using Business_Logic.Statuses;

namespace Application.Statuses;

/// <summary>
/// Data access contract for Statuses, implemented in Infrastructure against Postgres.
/// </summary>
public interface IStatusRepository
{
    List<Status> GetAll();
    Status? GetById(int statusId);
    void Add(Status status);
    void Update(Status status);
    void Delete(Status status);
}
