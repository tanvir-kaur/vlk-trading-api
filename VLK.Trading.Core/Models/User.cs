using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace VLK.Trading.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int? PasswordIterations { get; set; }
        public bool IsActive { get; set; }
        public DateTime LoginCreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }

    public static class UserModelBuilderExtension
    {
        public static void BuildUserModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("User");
                //entity.HasMany(x => x.TaskDependencyRules)
                //    .WithOne(x => x.TaskTag)
                //    .HasForeignKey(x => x.TaskTagId);
            });
        }
    }
}
