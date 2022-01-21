using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAPI.Data.Migrations
{
    public partial class Projects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImagePath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    ProjectDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    SourceLocation = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    DemoLocation = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.ProjectDetailId);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectImages",
                columns: table => new
                {
                    ProjectImageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectDetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImages", x => x.ProjectImageId);
                    table.ForeignKey(
                        name: "FK_ProjectImages_ProjectDetails_ProjectDetailId",
                        column: x => x.ProjectDetailId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Description", "ImagePath", "Name" },
                values: new object[] { 1, "This site! This one you are viewing right now. Bit of recursion for you. This demonstrates experiments in blazor with a Web API backend with EF core.", "assets/img/project_blazor.png", "Blazor Portfolio" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Description", "ImagePath", "Name" },
                values: new object[] { 2, "This is my main static landing site. Built to be simple and lightweight whilst also being responsive.", "assets/img/project_main_site.png", "Main sneddon.dev Site" });

            migrationBuilder.InsertData(
                table: "ProjectDetails",
                columns: new[] { "ProjectDetailId", "Content", "DemoLocation", "ProjectId", "SourceLocation" },
                values: new object[] { 1, "", "https://blazor.sneddon.dev/", 1, "https://github.com/snedex/blazor-portfolio" });

            migrationBuilder.InsertData(
                table: "ProjectDetails",
                columns: new[] { "ProjectDetailId", "Content", "DemoLocation", "ProjectId", "SourceLocation" },
                values: new object[] { 2, "", "https://www.sneddon.dev/", 2, "https://github.com/snedex/snedex.github.io" });

            migrationBuilder.InsertData(
                table: "ProjectImages",
                columns: new[] { "ProjectImageId", "ImagePath", "ProjectDetailId" },
                values: new object[] { 1, "assets/img/project/blazor_1.png", 1 });

            migrationBuilder.InsertData(
                table: "ProjectImages",
                columns: new[] { "ProjectImageId", "ImagePath", "ProjectDetailId" },
                values: new object[] { 2, "assets/img/project/blazor_2.png", 1 });

            migrationBuilder.InsertData(
                table: "ProjectImages",
                columns: new[] { "ProjectImageId", "ImagePath", "ProjectDetailId" },
                values: new object[] { 3, "assets/img/project/static_1.png", 2 });

            migrationBuilder.InsertData(
                table: "ProjectImages",
                columns: new[] { "ProjectImageId", "ImagePath", "ProjectDetailId" },
                values: new object[] { 4, "assets/img/project/static_2.png", 2 });

            migrationBuilder.InsertData(
                table: "ProjectImages",
                columns: new[] { "ProjectImageId", "ImagePath", "ProjectDetailId" },
                values: new object[] { 5, "assets/img/project/static_3.png", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectId",
                table: "ProjectDetails",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectImages_ProjectDetailId",
                table: "ProjectImages",
                column: "ProjectDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectImages");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
