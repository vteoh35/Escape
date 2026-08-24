using Business_Logic.Employees;

namespace Application.PositionLevels;

public class GetPositionLevel
{
    private readonly IPositionLevelRepository _positionLevelRepository;

    public GetPositionLevel(IPositionLevelRepository positionLevelRepository)
    {
        _positionLevelRepository = positionLevelRepository;
    }

    public List<PositionLevel> GetAll()
    {
        return _positionLevelRepository.GetAll();
    }

    public PositionLevel? GetByLevel(int level)
    {
        return _positionLevelRepository.GetByLevel(level);
    }
}
