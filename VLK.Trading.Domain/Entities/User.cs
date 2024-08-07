using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace VLK.Trading.Domain.Models
{
    public class User
    {
        [Key]
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
    }

    public static class UserModelBuilderExtension
    {
        public static void BuildUserModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("User");
            });
        }
    }
}
