using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Skill> Skills { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedCategories(modelBuilder);

            SeedSkills(modelBuilder);

        }

        private static void SeedSkills(ModelBuilder modelBuilder)
        {
            var skillSeed = new Skill[6];

            skillSeed[0] = new Skill() {
                SkillId = -1,
                DisplayOrder = 1,
                Title = "C# Development",
                Description = "C# is my primary programming language. I've been using this language since 2009 to build Windows Services, WPF Applications, Web applications (WebForms, MVC, WebAPI, SOAP Services), and Console utilities for the companies I have worked for. I find C# to be extremely flexible with excellent tooling and support built around it.",
                ImagePath = "assets/img/CSharp.png"
            };

            skillSeed[1] = new Skill() {
                SkillId = -2,
                DisplayOrder = 2,
                Title = "Core Web Technologies",
                Description = "I am familiar with some of the more popular web technologies, HTML 5, Bootstrap, Javascript/jQuery, along with some less well known items like pico.css. I hope to expand into Angular, React, and Vue as time allows.",
                ImagePath = "assets/img/CoreWebTech.png"
            };

            skillSeed[2] = new Skill() {
                SkillId = -3,
                DisplayOrder = 3,
                Title = "Blazor",
                Description = "As part of honing my skills, this site is an experiment and learning experience building web applications with Blazor and WebASM technologies. This gives me a chance to try out new things and build more progressive web applications with better UX.",
                ImagePath = "assets/img/ASPNetBlazor.png"
            };

            skillSeed[3] = new Skill() {
                SkillId = -4,
                DisplayOrder = 4,
                Title = "Database Development",
                Description = "Over my career I have spend a significant time writing SQL. This varied from stored procedures for application logic, maintenance, diagnostics, and even CLR extended functions to enhance functionality. I'm no stranger to EF code or database first either! ",
                ImagePath = "assets/img/SQLDatabase.png"
            };

            skillSeed[4] = new Skill() {
                SkillId = -5,
                DisplayOrder = 5,
                Title = "Devops",
                Description = "I have some experience with maintaining server infrastructure both on premises and in the cloud. I've also started to learn Azure deployment with CI/CD with this project utilising Github actions to automate the deployment process. ",
                ImagePath = "assets/img/Azure.png"
            };

            skillSeed[5] = new Skill() {
                SkillId = -6,
                DisplayOrder = 6,
                Title = "Python + Django",
                Description = "I've been teaching myself the Python language using the Django framework as part of a hobby project. Learning new skills and languages is both a fun challenge and important to my continual impovement for my career; plus it allows me to pivot when needed.",
                ImagePath = "assets/img/python.svg"
            };

            modelBuilder.Entity<Skill>().HasData(skillSeed);
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            var categorySeed = new Category[3];

            for (int i = 1; i < categorySeed.Length + 1; i++)
            {
                categorySeed[i - 1] = new Category
                {
                    CategoryId = i,
                    ThumbnailPath = "uploads/placeholder.jpg",
                    Name = $"Category {i}",
                    Description = $"This is a description of category {i}"
                };
            }

            modelBuilder.Entity<Category>().HasData(categorySeed);
        }
    }
}
