using Application.PositionLevels;
using Business_Logic.Employees;
using Infrastructure.Database;

namespace Infrastructure.Employees;

/// <summary>
/// EF Core-backed implementation of IPositionLevelRepository.
/// </summary>
public class PositionLevelRepository : IPositionLevelRepository
{
    private readonly AppDbContext _context;

    public PositionLevelRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<PositionLevel> GetAll()
    {
        return _context.PositionLevels.ToList();
    }

    public PositionLevel? GetByLevel(int level)
    {
        return _context.PositionLevels.FirstOrDefault(positionLevel => positionLevel.Level == level);
    }

    public void Add(PositionLevel positionLevel)
    {
        _context.PositionLevels.Add(positionLevel);
        _context.SaveChanges();
    }

    public void Update(PositionLevel positionLevel)
    {
        _context.SaveChanges();
    }

    public void Delete(PositionLevel positionLevel)
    {
        _context.PositionLevels.Remove(positionLevel);
        _context.SaveChanges();
    }
}
