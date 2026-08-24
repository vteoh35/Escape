using Microsoft.EntityFrameworkCore;
using Business_Logic.Employees;
using AuthenticationEntity = Business_Logic.Employees.Authentication;
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
    public DbSet<AuthenticationEntity> Authentications { get; set; }
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string employeeIdType = "character(10)";

        modelBuilder.Entity<Employee>(e =>
        {
            e.ToTable("employee");
            e.HasKey(x => x.EmployeeId);
            e.Property(x => x.EmployeeId).HasColumnName("employee_id").HasColumnType(employeeIdType);
            e.Property(x => x.Name).HasColumnName("name_").HasMaxLength(150);
            e.Property(x => x.Email).HasColumnName("email").HasMaxLength(150);
            e.Property(x => x.EmployeeLevel).HasColumnName("employee_level");
            e.Property(x => x.Department).HasColumnName("department").HasMaxLength(100);
            e.HasIndex(x => x.Email).IsUnique();
            e.HasOne<PositionLevel>().WithMany()
                .HasForeignKey(x => x.EmployeeLevel).HasPrincipalKey(p => p.Level)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<AuthenticationEntity>(e =>
        {
            e.ToTable("authentication");
            e.HasKey(x => x.EmployeeId);
            e.Property(x => x.EmployeeId).HasColumnName("employee_id").HasColumnType(employeeIdType);
            e.Property(x => x.PasswordHash).HasColumnName("pass_word").HasMaxLength(255);
            e.HasOne<Employee>().WithMany()
                .HasForeignKey(x => x.EmployeeId).HasPrincipalKey(emp => emp.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<PositionLevel>(e =>
        {
            e.ToTable("position_level");
            e.HasKey(x => x.Level);
            e.Property(x => x.Level).HasColumnName("level").ValueGeneratedNever();
            e.Property(x => x.Position).HasColumnName("position").HasMaxLength(50).IsRequired(false);
        });

        modelBuilder.Entity<Priority>(e =>
        {
            e.ToTable("priority");
            e.HasKey(x => x.PriorityId);
            e.Property(x => x.PriorityId).HasColumnName("priority_id").ValueGeneratedNever();
            e.Property(x => x.PriorityName).HasColumnName("priority_name").HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(e =>
        {
            e.ToTable("status");
            e.HasKey(x => x.StatusId);
            e.Property(x => x.StatusId).HasColumnName("status_id").ValueGeneratedNever();
            e.Property(x => x.StatusName).HasColumnName("status_name").HasMaxLength(50);
        });

        modelBuilder.Entity<Project>(e =>
        {
            e.ToTable("projects");
            e.HasKey(x => x.ProjectID);
            e.Property(x => x.ProjectID).HasColumnName("project_id").HasMaxLength(10);
            e.Property(x => x.Name).HasColumnName("name_").HasMaxLength(150);
            e.Property(x => x.Description).HasColumnName("description");
            e.Property(x => x.PriorityId).HasColumnName("priority_id");
            e.Property(x => x.StatusId).HasColumnName("status_id");
            e.Property(x => x.StartDate).HasColumnName("start_date");
            e.Property(x => x.EndDate).HasColumnName("end_date");
            e.HasOne<Priority>().WithMany()
                .HasForeignKey(x => x.PriorityId).HasPrincipalKey(p => p.PriorityId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Status>().WithMany()
                .HasForeignKey(x => x.StatusId).HasPrincipalKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<TaskItem>(e =>
        {
            e.ToTable("tasks");
            e.HasKey(x => x.TaskId);
            e.Property(x => x.TaskId).HasColumnName("task_id").HasMaxLength(10);
            e.Property(x => x.Name).HasColumnName("name_").HasMaxLength(150);
            e.Property(x => x.Description).HasColumnName("description");
            e.Property(x => x.PriorityId).HasColumnName("priority_id");
            e.Property(x => x.StartDate).HasColumnName("start_date");
            e.Property(x => x.EndDate).HasColumnName("end_date");
            e.Property(x => x.StatusId).HasColumnName("status_id");
            e.Property(x => x.ProjectId).HasColumnName("project_id").HasMaxLength(10);
            e.Property(x => x.ParentTaskId).HasColumnName("parent_task").HasMaxLength(10);
            e.HasOne<Priority>().WithMany()
                .HasForeignKey(x => x.PriorityId).HasPrincipalKey(p => p.PriorityId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Status>().WithMany()
                .HasForeignKey(x => x.StatusId).HasPrincipalKey(s => s.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Project>().WithMany()
                .HasForeignKey(x => x.ProjectId).HasPrincipalKey(p => p.ProjectID)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<TaskItem>().WithMany()
                .HasForeignKey(x => x.ParentTaskId).HasPrincipalKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Comment>(e =>
        {
            e.ToTable("comments_");
            e.HasKey(x => x.CommentId);
            e.Property(x => x.CommentId).HasColumnName("comment_id").HasMaxLength(10);
            e.Property(x => x.Description).HasColumnName("description");
            e.Property(x => x.TaskId).HasColumnName("task_id").HasMaxLength(10);
            e.Property(x => x.EmployeeId).HasColumnName("employee_id").HasColumnType(employeeIdType);
            e.Property(x => x.CommentTime).HasColumnName("comment_time");
            e.Property(x => x.ParentCommentId).HasColumnName("parent_comment_id").HasColumnType("character(10)");
            e.HasOne<TaskItem>().WithMany()
                .HasForeignKey(x => x.TaskId).HasPrincipalKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Employee>().WithMany()
                .HasForeignKey(x => x.EmployeeId).HasPrincipalKey(emp => emp.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Comment>().WithMany()
                .HasForeignKey(x => x.ParentCommentId).HasPrincipalKey(c => c.CommentId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Attachment>(e =>
        {
            e.ToTable("attachments");
            e.HasKey(x => x.AttachmentId);
            e.Property(x => x.AttachmentId).HasColumnName("attachment_id").HasMaxLength(10);
            e.Property(x => x.AttachmentLocation).HasColumnName("attachment_location").HasMaxLength(255);
            e.Property(x => x.ProjectId).HasColumnName("project_id").HasMaxLength(10);
            e.Property(x => x.TaskId).HasColumnName("task_id").HasMaxLength(10);
            e.HasOne<Project>().WithMany()
                .HasForeignKey(x => x.ProjectId).HasPrincipalKey(p => p.ProjectID)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<TaskItem>().WithMany()
                .HasForeignKey(x => x.TaskId).HasPrincipalKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ActivityLog>(e =>
        {
            e.ToTable("activity_log");
            e.HasKey(x => x.LogId);
            e.Property(x => x.LogId).HasColumnName("log_id").HasMaxLength(255);
            e.Property(x => x.Description).HasColumnName("description");
            e.Property(x => x.ProjectId).HasColumnName("project_id").HasMaxLength(10);
            e.Property(x => x.TaskId).HasColumnName("task_id").HasMaxLength(10);
            e.Property(x => x.EmployeeId).HasColumnName("employee_id").HasColumnType(employeeIdType);
            e.Property(x => x.LogTime).HasColumnName("log_time");
            e.HasOne<Project>().WithMany()
                .HasForeignKey(x => x.ProjectId).HasPrincipalKey(p => p.ProjectID)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<TaskItem>().WithMany()
                .HasForeignKey(x => x.TaskId).HasPrincipalKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Employee>().WithMany()
                .HasForeignKey(x => x.EmployeeId).HasPrincipalKey(emp => emp.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ProjectMember>(e =>
        {
            e.ToTable("project_members");
            e.HasKey(x => new { x.EmployeeId, x.ProjectId });
            e.Property(x => x.EmployeeId).HasColumnName("employee_id").HasColumnType(employeeIdType);
            e.Property(x => x.ProjectId).HasColumnName("project_id").HasMaxLength(10);
            e.Property(x => x.Role).HasColumnName("role_").HasMaxLength(50);
            e.HasOne<Employee>().WithMany()
                .HasForeignKey(x => x.EmployeeId).HasPrincipalKey(emp => emp.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<Project>().WithMany()
                .HasForeignKey(x => x.ProjectId).HasPrincipalKey(p => p.ProjectID)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<TaskAssignee>(e =>
        {
            e.ToTable("task_assignees");
            e.HasKey(x => new { x.EmployeeId, x.TaskId });
            e.Property(x => x.EmployeeId).HasColumnName("employee_id").HasColumnType(employeeIdType);
            e.Property(x => x.TaskId).HasColumnName("task_id").HasMaxLength(10);
            e.Property(x => x.Role).HasColumnName("role_").HasMaxLength(50);
            e.HasOne<Employee>().WithMany()
                .HasForeignKey(x => x.EmployeeId).HasPrincipalKey(emp => emp.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);
            e.HasOne<TaskItem>().WithMany()
                .HasForeignKey(x => x.TaskId).HasPrincipalKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }

}

