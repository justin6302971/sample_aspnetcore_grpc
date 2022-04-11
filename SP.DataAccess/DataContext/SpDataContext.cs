using GrpcGreeter.Models;
using Microsoft.EntityFrameworkCore;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GrpcGreeter.DataContexts
{
    public partial class SpDataContext : DbContext
    {
        public SpDataContext()
        {
        }

        public SpDataContext(DbContextOptions<SpDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=ConnectionStrings:SpConnectionString");
            }
            optionsBuilder.EnableSensitiveDataLogging();//for debugging only
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("car", "sp");

                entity.HasIndex(e => e.PersonId)
                    .HasName("car_person_id_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(19,2)");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Car)
                    .HasForeignKey<Car>(d => d.PersonId)
                    .HasConstraintName("car_person_id_fkey");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person", "sp");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
