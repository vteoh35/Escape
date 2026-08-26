using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Data access contract for Tags, implemented in Infrastructure against Postgres.
/// </summary>
public interface ITagRepository
{
    List<Tag> GetAll();
    Tag? GetById(int tagId);
    void Add(Tag tag);
    void Update(Tag tag);
    void Delete(Tag tag);
}
