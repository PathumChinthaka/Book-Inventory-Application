using Book_Inventory_System.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_Inventory_System.Data
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(a => a.Id);
            builder.Property(b => b.Id).UseIdentityColumn();

            builder.Property(s => s.Title).HasMaxLength(20);
            builder.Property(s => s.ISBN).HasMaxLength(10);
            builder.Property(s => s.Author).HasMaxLength(20);
            builder.Property(s => s.PublicationDate).HasMaxLength(15);
        }
    }
}
