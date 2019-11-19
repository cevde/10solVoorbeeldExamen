using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThePlaceToMeet.Models.Domain;

namespace ThePlaceToMeet.Data.Mappers
{
    public class ReservatieConfiguration : IEntityTypeConfiguration<Reservatie>
    {
        public void Configure(EntityTypeBuilder<Reservatie> builder)
        {
            //implemsenteer
            builder.ToTable("Reservatie");
            builder
                .HasOne(t => t.Catering)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(t => t.Korting)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

