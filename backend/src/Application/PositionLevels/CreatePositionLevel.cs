using Business_Logic.Employees;

namespace Application.PositionLevels;

public class CreatePositionLevel
{
    private readonly IPositionLevelRepository _positionLevelRepository;

    public CreatePositionLevel(IPositionLevelRepository positionLevelRepository)
    {
        _positionLevelRepository = positionLevelRepository;
    }

    public PositionLevel Execute(int level, string? position)
    {
        var positionLevel = new PositionLevel
        {
            Level = level,
            Position = position
        };

        _positionLevelRepository.Add(positionLevel);

        return positionLevel;
    }
}
