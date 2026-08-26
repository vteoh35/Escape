using Application.Priorities;
using Business_Logic.Priorities;
using Infrastructure.Database;

namespace Infrastructure.Priorities;

/// <summary>
/// EF Core-backed implementation of IPriorityRepository.
/// </summary>
public class PriorityRepository : IPriorityRepository
{
    private readonly AppDbContext _context;

    public PriorityRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Priority> GetAll()
    {
        return _context.Priorities.ToList();
    }

    public Priority? GetById(int priorityId)
    {
        return _context.Priorities.FirstOrDefault(priority => priority.PriorityId == priorityId);
    }

    public void Add(Priority priority)
    {
        _context.Priorities.Add(priority);
        _context.SaveChanges();
    }

    public void Update(Priority priority)
    {
        _context.SaveChanges();
    }

    public void Delete(Priority priority)
    {
        _context.Priorities.Remove(priority);
        _context.SaveChanges();
    }
}
