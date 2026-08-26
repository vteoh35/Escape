using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Updates a tag's name. Returns null if the id doesn't exist.
/// </summary>
public class UpdateTag
{
    private readonly ITagRepository _tagRepository;

    public UpdateTag(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public Tag? Execute(int tagId, string tagName)
    {
        var tag = _tagRepository.GetById(tagId);

        if (tag == null)
        {
            return null;
        }

        tag.TagName = tagName;

        _tagRepository.Update(tag);

        return tag;
    }
}
