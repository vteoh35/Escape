namespace Application.Tags;

public class DeleteTag
{
    private readonly ITagRepository _tagRepository;

    public DeleteTag(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public bool Execute(int tagId)
    {
        var tag = _tagRepository.GetById(tagId);

        if (tag == null)
        {
            return false;
        }

        _tagRepository.Delete(tag);

        return true;
    }
}
