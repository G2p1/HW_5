using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.SqlServer;

namespace HW_5
{
     public class OrdderRntityConfigutarion : IEntityTypeConfiguration<Order>   {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(s => s.ord_id);

            builder.Property(s => s.ord_datetime)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(s => s.ord_an);

            builder.HasIndex(s => new { s.ord_id });

        }

    }
}
