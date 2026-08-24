using Microsoft.EntityFrameworkCore;
using Business_Logic.Employees;
using Business_Logic.Projects;
using Business_Logic.Tasks;
using Business_Logic.Statuses;
using Business_Logic.Priorities;
using Business_Logic.Comments;
using Business_Logic.Attachments;
using Business_Logic.ActivityLogs;

namespace Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Authentication> Authentications { get; set; }
    public DbSet<PositionLevel> PositionLevels { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<TaskAssignee> TaskAssignees { get; set; }

    protected override void onModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectMember>()
            .HasKey(pm => {pm.EmployeeId, pm.ProjectId});

        modelBuilder.Entity<TaskAssignee>()
            .HasKey(ta => new { ta.EmployeeId, ta.TaskId });
    }

}

