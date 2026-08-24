using Business_Logic.Comments;

namespace Application.Comments;

public interface ICommentRepository
{
    List<Comment> GetAll();
    Comment? GetById(string commentId);
    void Add(Comment comment);
    void Update(Comment comment);
    void Delete(Comment comment);
}
