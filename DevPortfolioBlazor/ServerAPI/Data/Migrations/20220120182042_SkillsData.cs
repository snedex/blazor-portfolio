using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerAPI.Data.Migrations
{
    public partial class SkillsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DisplayOrder", "ImagePath", "Title" },
                values: new object[] { -6, "I've been teaching myself the Python language using the Django framework as part of a hobby project. Learning new skills and languages is both a fun challenge and important to my continual impovement for my career; plus it allows me to pivot when needed.", 6, "assets/img/python.svg", "Python + Django" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DisplayOrder", "ImagePath", "Title" },
                values: new object[] { -5, "I have some experience with maintaining server infrastructure both on premises and in the cloud. I've also started to learn Azure deployment with CI/CD with this project utilising Github actions to automate the deployment process. ", 5, "assets/img/Azure.png", "Devops" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DisplayOrder", "ImagePath", "Title" },
                values: new object[] { -4, "Over my career I have spend a significant time writing SQL. This varied from stored procedures for application logic, maintenance, diagnostics, and even CLR extended functions to enhance functionality. I'm no stranger to EF code or database first either! ", 4, "assets/img/SQLDatabase.png", "Database Development" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DisplayOrder", "ImagePath", "Title" },
                values: new object[] { -3, "As part of honing my skills, this site is an experiment and learning experience building web applications with Blazor and WebASM technologies. This gives me a chance to try out new things and build more progressive web applications with better UX.", 3, "assets/img/ASPNetBlazor.png", "Blazor" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DisplayOrder", "ImagePath", "Title" },
                values: new object[] { -2, "I am familiar with some of the more popular web technologies, HTML 5, Bootstrap, Javascript/jQuery, along with some less well known items like pico.css. I hope to expand into Angular, React, and Vue as time allows.", 2, "assets/img/CoreWebTech.png", "Core Web Technologies" });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "Description", "DisplayOrder", "ImagePath", "Title" },
                values: new object[] { -1, "C# is my primary programming language. I've been using this language since 2009 to build Windows Services, WPF Applications, Web applications (WebForms, MVC, WebAPI, SOAP Services), and Console utilities for the companies I have worked for. I find C# to be extremely flexible with excellent tooling and support built around it.", 1, "assets/img/CSharp.png", "C# Development" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
