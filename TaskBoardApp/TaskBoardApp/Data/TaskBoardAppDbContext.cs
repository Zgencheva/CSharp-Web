using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Data.Entities;

namespace TaskBoardApp.Data
{
    public class TaskBoardAppDbContext : IdentityDbContext<User>
    {
        public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        private User GuestUser { get; set; }
        private Board OpenBoard { get; set; }
        private Board InProgressBoard { get; set; }
        private Board DoneBoard { get; set; }

        public DbSet<TaskBoardApp.Data.Entities.Task> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedUsers();
            builder
                .Entity<User>()
                .HasData(this.GuestUser);
            SeedBoards();
            builder
               .Entity<Board>()
               .HasData(this.OpenBoard, this.InProgressBoard, this.DoneBoard);

            builder.Entity<Entities.Task>()
                .HasData(
                    new Entities.Task()
                    {
                        Id = 1,
                        Title = "Prepare for ASP.NET Fundamentals exam",
                        Description = "Learn using ASP.NET Core identity",
                        CreatedOn = DateTime.UtcNow.AddMonths(-1),
                        OwnerId = this.GuestUser.Id,
                        BoardId = this.OpenBoard.Id,
                    },
                    new Entities.Task()
                    {
                        Id = 2,
                        Title = "Improve EF Core skills",
                        Description = "Learn using EF Core and MS SQL",
                        CreatedOn = DateTime.UtcNow.AddMonths(-5),
                        OwnerId = this.GuestUser.Id,
                        BoardId = this.DoneBoard.Id,
                    },
                    new Entities.Task()
                    {
                        Id = 3,
                        Title = "Improve ASP.NER Core skills",
                        Description = "Learn using ASP.NET Core identity",
                        CreatedOn = DateTime.UtcNow.AddMonths(-10),
                        OwnerId = this.GuestUser.Id,
                        BoardId = this.InProgressBoard.Id,
                    },
                     new Entities.Task()
                     {
                         Id = 4,
                         Title = "Prepare fr C# Fundamentals exam",
                         Description = "Prepare by NOT solving old Mid and Final exams",
                         CreatedOn = DateTime.UtcNow.AddMonths(-1),
                         OwnerId = this.GuestUser.Id,
                         BoardId = this.DoneBoard.Id,
                     }
                    );

            builder
                .Entity<TaskBoardApp.Data.Entities.Task>()
                .HasOne(t => t.Board)
                .WithMany(b => b.Tasks)
                .HasForeignKey(k => k.BoardId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        private void SeedBoards()
        {
            this.OpenBoard = new Board()
            {
                Id = 1,
                Name = "Open"
            };
            this.InProgressBoard = new Board()
            {
                Id = 2,
                Name = "In Progess"
            };
            this.DoneBoard = new Board()
            {
                Id = 3,
                Name = "Done"
            };
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            this.GuestUser = new User
            {
                UserName = "guest",
                NormalizedUserName = "GUEST",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM",
                FirstName = "Guest",
                LastName = "User"
            };

            this.GuestUser.PasswordHash = hasher.HashPassword(this.GuestUser, "guest");
        }
    }
}