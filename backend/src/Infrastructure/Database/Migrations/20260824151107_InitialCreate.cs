using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "position_level",
                columns: table => new
                {
                    level = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_position_level", x => x.level);
                });

            migrationBuilder.CreateTable(
                name: "priority",
                columns: table => new
                {
                    priority_id = table.Column<int>(type: "integer", nullable: false),
                    priority_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority", x => x.priority_id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    status_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "character(10)", nullable: false),
                    name_ = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    employee_level = table.Column<int>(type: "integer", nullable: true),
                    department = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_employee_position_level_employee_level",
                        column: x => x.employee_level,
                        principalTable: "position_level",
                        principalColumn: "level");
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    project_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name_ = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    priority_id = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.project_id);
                    table.ForeignKey(
                        name: "FK_projects_priority_priority_id",
                        column: x => x.priority_id,
                        principalTable: "priority",
                        principalColumn: "priority_id");
                    table.ForeignKey(
                        name: "FK_projects_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "status_id");
                });

            migrationBuilder.CreateTable(
                name: "authentication",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "character(10)", nullable: false),
                    pass_word = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authentication", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK_authentication_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id");
                });

            migrationBuilder.CreateTable(
                name: "project_members",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "character(10)", nullable: false),
                    project_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    role_ = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_members", x => new { x.employee_id, x.project_id });
                    table.ForeignKey(
                        name: "FK_project_members_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_project_members_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    name_ = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    priority_id = table.Column<int>(type: "integer", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    project_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    parent_task = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_tasks_priority_priority_id",
                        column: x => x.priority_id,
                        principalTable: "priority",
                        principalColumn: "priority_id");
                    table.ForeignKey(
                        name: "FK_tasks_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_tasks_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "status_id");
                    table.ForeignKey(
                        name: "FK_tasks_tasks_parent_task",
                        column: x => x.parent_task,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateTable(
                name: "activity_log",
                columns: table => new
                {
                    log_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    project_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    task_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    employee_id = table.Column<string>(type: "character(10)", nullable: true),
                    log_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity_log", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_activity_log_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_activity_log_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_activity_log_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    attachment_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    attachment_location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    project_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    task_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.attachment_id);
                    table.ForeignKey(
                        name: "FK_attachments_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_attachments_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateTable(
                name: "comments_",
                columns: table => new
                {
                    comment_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    task_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    employee_id = table.Column<string>(type: "character(10)", nullable: true),
                    comment_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    parent_comment_id = table.Column<string>(type: "character(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments_", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_comments__comments__parent_comment_id",
                        column: x => x.parent_comment_id,
                        principalTable: "comments_",
                        principalColumn: "comment_id");
                    table.ForeignKey(
                        name: "FK_comments__employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_comments__tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateTable(
                name: "task_assignees",
                columns: table => new
                {
                    employee_id = table.Column<string>(type: "character(10)", nullable: false),
                    task_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    role_ = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_assignees", x => new { x.employee_id, x.task_id });
                    table.ForeignKey(
                        name: "FK_task_assignees_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "FK_task_assignees_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_log_employee_id",
                table: "activity_log",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_activity_log_project_id",
                table: "activity_log",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_activity_log_task_id",
                table: "activity_log",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_project_id",
                table: "attachments",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_task_id",
                table: "attachments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments__employee_id",
                table: "comments_",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments__parent_comment_id",
                table: "comments_",
                column: "parent_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments__task_id",
                table: "comments_",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_email",
                table: "employee",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_employee_level",
                table: "employee",
                column: "employee_level");

            migrationBuilder.CreateIndex(
                name: "IX_project_members_project_id",
                table: "project_members",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_priority_id",
                table: "projects",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "IX_projects_status_id",
                table: "projects",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_task_assignees_task_id",
                table: "task_assignees",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_parent_task",
                table: "tasks",
                column: "parent_task");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_priority_id",
                table: "tasks",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_project_id",
                table: "tasks",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_status_id",
                table: "tasks",
                column: "status_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_log");

            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "authentication");

            migrationBuilder.DropTable(
                name: "comments_");

            migrationBuilder.DropTable(
                name: "project_members");

            migrationBuilder.DropTable(
                name: "task_assignees");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "position_level");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "priority");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
