using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Task = ApiBackend.Models.Task;
using TaskStatus = ApiBackend.Models.TaskStatus;

namespace ApiBackend.Dal
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Task> Tasks => Set<Task>();
        public DbSet<TaskHistory> TaskHistories => Set<TaskHistory>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>().HasData(
                new User { Id = 1, UserName = "Alice" },
                new User { Id = 2, UserName = "Bob" }
            );
            mb.Entity<Task>().HasData(
                new Task { 
                    Id = 1,
                    AssignedToUserId = 1,
                    CreatedAt = new DateTime(2025, 07, 13, 10, 00, 00),
                    Description = "Desc: Dev test .net react",
                    Status = TaskStatus.ToDo,
                    Title = "Dev test .net react"                    
                },
                new Task
                {
                    Id = 2,
                    AssignedToUserId = 1,
                    CreatedAt = new DateTime(2025, 07, 13, 10, 00, 00),
                    Description = "Desc: Dev test .net angular",
                    Status = TaskStatus.ToDo,
                    Title = "Dev test .net angular"
                },
                new Task
                {
                    Id = 3,
                    AssignedToUserId = 1,
                    CreatedAt = new DateTime(2025, 07, 13, 10, 00, 00),
                    Description = "Desc: Dev test .net aspnet.api",
                    Status = TaskStatus.InProgress,
                    Title = "Dev test .net aspnet.api"
                },
                new Task
                {
                    Id = 4,
                    AssignedToUserId = 2,
                    CreatedAt = new DateTime(2025, 07, 13, 10, 00, 00),
                    Description = "Desc: Dev test TS",
                    Status = TaskStatus.InProgress,
                    Title = "Dev test TS"
                },
                new Task
                {
                    Id = 5,
                    AssignedToUserId = 2,
                    CreatedAt = new DateTime(2025, 07, 13, 10, 00, 00),
                    Description = "Desc: Dev Devops",
                    Status = TaskStatus.Done,
                    Title = "Dev Devops"
                }
                );
        }
    }
}
