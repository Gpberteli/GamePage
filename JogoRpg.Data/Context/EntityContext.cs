using JogoRpg.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JogoRpg.Data.Context
{
    public class EntityContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options){ }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<ClassReference> ClassReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>()
                .Property(p => p.CharSex)
                .HasConversion<string>();

            modelBuilder.Entity<ClassReference>(entity =>
            {
                entity.ToTable("ClassReference");
                entity.HasKey(e => e.ClassId);
                entity.Property(e => e.ClassName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Charact");
                entity.HasKey(e => e.CharId);
                entity.Property(e => e.CharName).HasMaxLength(100);

                entity.HasOne(e => e.ClassReference)
                      .WithMany(c => c.Characters)
                      .HasForeignKey(e => e.ClassId)
                      .HasConstraintName("FK_Charact_ClassReference");
            });
        }
    }
}