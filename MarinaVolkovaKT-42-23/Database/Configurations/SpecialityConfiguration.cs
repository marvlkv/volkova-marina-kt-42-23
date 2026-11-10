using MarinaVolkovaKT_42_23.Models;
using MarinaVolkovaKT_42_23.Database.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarinaVolkovaKT_42_23.Database.Configurations
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        private const string TableName = "cd_speciality";

        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder.HasKey(p => p.SpecialityId).HasName($"pk_{TableName}_speciality_id");
            builder.Property(p => p.SpecialityId).HasColumnName("speciality_id")
                .HasComment("Идентификатор записи специальности").ValueGeneratedOnAdd();

            builder.Property(p => p.Title).IsRequired().HasColumnName("c_title")
                .HasComment("Название специальности");

            builder.Property(p => p.Code).HasColumnName("c_code").HasComment("Код специальности");

            builder.ToTable(TableName);
        }
    }
}
