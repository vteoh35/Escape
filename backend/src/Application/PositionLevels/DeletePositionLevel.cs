namespace Application.PositionLevels;

public class DeletePositionLevel
{
    private readonly IPositionLevelRepository _positionLevelRepository;

    public DeletePositionLevel(IPositionLevelRepository positionLevelRepository)
    {
        _positionLevelRepository = positionLevelRepository;
    }

    public bool Execute(int level)
    {
        var positionLevel = _positionLevelRepository.GetByLevel(level);

        if (positionLevel == null)
        {
            return false;
        }

        _positionLevelRepository.Delete(positionLevel);

        return true;
    }
}
