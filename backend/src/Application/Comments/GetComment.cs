using Business_Logic.Comments;

namespace Application.Comments;

public class GetComment
{
    private readonly ICommentRepository _commentRepository;

    public GetComment(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public List<Comment> GetAll()
    {
        return _commentRepository.GetAll();
    }

    public Comment? GetById(string commentId)
    {
        return _commentRepository.GetById(commentId);
    }
}
