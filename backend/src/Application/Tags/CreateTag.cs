using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Creates a new tag. TagId is database-generated, so callers only supply the name.
/// </summary>
public class CreateTag
{
    private readonly ITagRepository _tagRepository;

    public CreateTag(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public Tag Execute(string tagName)
    {
        var tag = new Tag
        {
            TagName = tagName
        };

        _tagRepository.Add(tag);

        return tag;
    }
}
