using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ServerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectDetail> ProjectDetails { get; set; }

        public DbSet<ProjectImage> ProjectImages { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
#if DEBUG
            optionsBuilder.LogTo(l => System.Diagnostics.Debug.WriteLine(l));
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedCategories(modelBuilder);

            SeedSkills(modelBuilder);

            SeedProjects(modelBuilder);

            SeedProjectDetails(modelBuilder);

            SeedProjectImages(modelBuilder);

            SeedPosts(modelBuilder);
        }

        private void SeedPosts(ModelBuilder modelBuilder)
        {
            Post[] postsToSeed = new Post[6];

            for (int i = 1; i < 7; i++)
            {
                string postTitle = string.Empty;
                int categoryId = (i % 3) + 1;

                switch (i)
                {
                    case 1:
                        postTitle = "First post";
                        break;
                    case 2:
                        postTitle = "Second post";
                        break;
                    case 3:
                        postTitle = "Third post";
                        break;
                    case 4:
                        postTitle = "Fourth post";
                        break;
                    case 5:
                        postTitle = "Fifth post";
                        break;
                    case 6:
                        postTitle = "Sixth post";
                        break;
                    default:
                        break;
                }

                postsToSeed[i - 1] = new Post
                {
                    PostId = i,
                    ThumbnailImagePath = "uploads/placeholder.jpg",
                    Title = postTitle,
                    Excerpt = $"This is the excerpt for post {i}. An excerpt is a little extraction from a larger piece of text. Sort of like a preview.",
                    Content = string.Empty,
                    PublishDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm"),
                    Published = true,
                    Author = "Will",
                    CategoryId = categoryId
                };
            }

            modelBuilder.Entity<Post>().HasData(postsToSeed);
        }

        private static void SeedProjectImages(ModelBuilder modelBuilder)
        {
            var images = new List<ProjectImage>()
            {
                new ProjectImage() { ProjectDetailId = 1, ProjectImageId = 1 , ImagePath = "assets/img/project/blazor_1.png" },
                new ProjectImage() { ProjectDetailId = 1, ProjectImageId = 2 , ImagePath = "assets/img/project/blazor_2.png" },
                new ProjectImage() { ProjectDetailId = 2, ProjectImageId = 3 , ImagePath = "assets/img/project/static_1.png" },
                new ProjectImage() { ProjectDetailId = 2, ProjectImageId = 4 , ImagePath = "assets/img/project/static_2.png" },
                new ProjectImage() { ProjectDetailId = 2, ProjectImageId = 5 , ImagePath = "assets/img/project/static_3.png" },
            };

            modelBuilder.Entity<ProjectImage>().HasData(images);
        }

        private static void SeedProjectDetails(ModelBuilder modelBuilder)
        {
            var projectDetails = new ProjectDetail[2];

            projectDetails[0] = new ProjectDetail()
            {
                ProjectDetailId = 1,
                ProjectId = 1,
                DemoLocation = "https://blazor.sneddon.dev/",
                SourceLocation = "https://github.com/snedex/blazor-portfolio",
                Content = ""
            };

            projectDetails[1] = new ProjectDetail()
            {
                ProjectDetailId = 2,
                ProjectId = 2,
                DemoLocation = "https://www.sneddon.dev/",
                SourceLocation = "https://github.com/snedex/snedex.github.io",
                Content = ""
            };

            modelBuilder.Entity<ProjectDetail>().HasData(projectDetails);
        }
  
        private static void SeedProjects(ModelBuilder modelBuilder)
        {
            var projectSeed = new Project[2];

            projectSeed[0] = new Project()
            {
                ProjectId = 1,
                Name = "Blazor Portfolio",
                Description = "This site! This one you are viewing right now. Bit of recursion for you. This demonstrates experiments in blazor with a Web API backend with EF core.",
                ImagePath = "assets/img/project_blazor.png"
            };

            projectSeed[1] = new Project()
            {
                ProjectId = 2,
                Name = "Main sneddon.dev Site",
                Description = "This is my main static landing site. Built to be simple and lightweight whilst also being responsive.",
                ImagePath = "assets/img/project_main_site.png"
            };

            modelBuilder.Entity<Project>().HasData(projectSeed);
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
