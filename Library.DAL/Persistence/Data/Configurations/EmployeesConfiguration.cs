using Library.DAL.Common.Enums;
using Library.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Persistence.Data.Configurations
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name)
                   .HasColumnType("varchar(50)")
                   .IsRequired();
            builder.Property(E => E.Address)
                 .HasColumnType("varchar(100)");

            builder.Property(E => E.Salary)
                   .HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender)
                   .HasConversion((gender) => gender.ToString(),
                                  (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(E=>E.CreateOn).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
