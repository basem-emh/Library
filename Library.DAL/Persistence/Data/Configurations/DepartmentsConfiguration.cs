using Library.DAL.Entities.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.DAL.Persistence.Data.Configurations
{
    public class DepartmentsConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id)
                              .UseIdentityColumn(10, 10);

            builder.Property(D => D.Name).HasColumnType("varchar(50)")
                   .IsRequired();

            builder.Property(D => D.CreateOn)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(D => D.LastModifiedOn)
                   .HasComputedColumnSql("GETDATE()");

            builder.HasMany(D => D.Emplyees)
                   .WithOne(E => E.Department)
                   .HasForeignKey(E => E.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(D => D.Books)
                   .WithOne(B => B.Department)
                   .HasForeignKey(B => B.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
