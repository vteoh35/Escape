using Business_Logic.Tags;

namespace Application.Tags;

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
