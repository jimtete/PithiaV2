using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PithiaV2.Migrations
{
    public partial class database_v_2_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassingGrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GradingBookletId = table.Column<int>(type: "integer", nullable: false),
                    StudentXCourseId = table.Column<int>(type: "integer", nullable: false),
                    ProfessorId = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassingGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassingGrades_GradingBooklets_GradingBookletId",
                        column: x => x.GradingBookletId,
                        principalTable: "GradingBooklets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassingGrades_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassingGrades_StudentXCourses_StudentXCourseId",
                        column: x => x.StudentXCourseId,
                        principalTable: "StudentXCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassingGrades_GradingBookletId",
                table: "PassingGrades",
                column: "GradingBookletId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingGrades_ProfessorId",
                table: "PassingGrades",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_PassingGrades_StudentXCourseId",
                table: "PassingGrades",
                column: "StudentXCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassingGrades");
        }
    }
}
