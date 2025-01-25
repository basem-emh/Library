using Library.DAL.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Persistence.Data.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(B => B.Title)
                  .HasColumnType("varchar(50)")
                  .IsRequired();
            builder.Property(B => B.Author)
                 .HasColumnType("varchar(50)");

            builder.Property(E => E.CreateOn).HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
