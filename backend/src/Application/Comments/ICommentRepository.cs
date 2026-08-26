using Business_Logic.Comments;

namespace Application.Comments;

/// <summary>
/// Data access contract for Comments, implemented in Infrastructure against Postgres.
/// </summary>
public interface ICommentRepository
{
    List<Comment> GetAll();
    Comment? GetById(string commentId);
    void Add(Comment comment);
    void Update(Comment comment);
    void Delete(Comment comment);
}
