using Application.Comments;
using Business_Logic.Comments;
using Infrastructure.Database;

namespace Infrastructure.Comments;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Comment> GetAll()
    {
        return _context.Comments.ToList();
    }

    public Comment? GetById(string commentId)
    {
        return _context.Comments.FirstOrDefault(comment => comment.CommentId == commentId);
    }

    public void Add(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }

    public void Update(Comment comment)
    {
        _context.SaveChanges();
    }

    public void Delete(Comment comment)
    {
        _context.Comments.Remove(comment);
        _context.SaveChanges();
    }
}
