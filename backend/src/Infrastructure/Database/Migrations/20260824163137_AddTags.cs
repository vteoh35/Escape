using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "project_tags",
                columns: table => new
                {
                    project_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_tags", x => new { x.project_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_project_tags_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "project_id");
                    table.ForeignKey(
                        name: "FK_project_tags_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "tag_id");
                });

            migrationBuilder.CreateTable(
                name: "task_tags",
                columns: table => new
                {
                    task_id = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_tags", x => new { x.task_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_task_tags_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "tag_id");
                    table.ForeignKey(
                        name: "FK_task_tags_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_tags_tag_id",
                table: "project_tags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_tag_name",
                table: "tag",
                column: "tag_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_task_tags_tag_id",
                table: "task_tags",
                column: "tag_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_tags");

            migrationBuilder.DropTable(
                name: "task_tags");

            migrationBuilder.DropTable(
                name: "tag");
        }
    }
}
