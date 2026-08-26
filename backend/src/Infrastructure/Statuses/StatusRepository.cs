using Application.Statuses;
using Business_Logic.Statuses;
using Infrastructure.Database;

namespace Infrastructure.Statuses;

/// <summary>
/// EF Core-backed implementation of IStatusRepository.
/// </summary>
public class StatusRepository : IStatusRepository
{
    private readonly AppDbContext _context;

    public StatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Status> GetAll()
    {
        return _context.Statuses.ToList();
    }

    public Status? GetById(int statusId)
    {
        return _context.Statuses.FirstOrDefault(status => status.StatusId == statusId);
    }

    public void Add(Status status)
    {
        _context.Statuses.Add(status);
        _context.SaveChanges();
    }

    public void Update(Status status)
    {
        _context.SaveChanges();
    }

    public void Delete(Status status)
    {
        _context.Statuses.Remove(status);
        _context.SaveChanges();
    }
}
