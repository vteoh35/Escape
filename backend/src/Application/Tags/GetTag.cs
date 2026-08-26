using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Reads tags, either all of them or by id.
/// </summary>
public class GetTag
{
    private readonly ITagRepository _tagRepository;

    public GetTag(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public List<Tag> GetAll()
    {
        return _tagRepository.GetAll();
    }

    public Tag? GetById(int tagId)
    {
        return _tagRepository.GetById(tagId);
    }
}
