using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain;

namespace Store.Data.Context.Configurations
{
    public class ItemProductConfiguration : IEntityTypeConfiguration<ItemProduct>
    {
        public void Configure(EntityTypeBuilder<ItemProduct> builder)
        {
            builder.HasKey(c => c.Id);
            builder.ToTable("ItemProduct");
        }
    }
}
