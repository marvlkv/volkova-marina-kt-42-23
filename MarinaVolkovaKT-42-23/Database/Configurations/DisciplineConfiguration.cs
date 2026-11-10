using MarinaVolkovaKT_42_23.Models;
using MarinaVolkovaKT_42_23.Database.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarinaVolkovaKT_42_23.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "cd_discipline";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasKey(p => p.DisciplineId).HasName($"pk_{TableName}_discipline_id");
            builder.Property(p => p.DisciplineId).HasColumnName("discipline_id")
                .HasComment("Идентификатор записи дисциплины").ValueGeneratedOnAdd();

            builder.Property(p => p.Name).IsRequired().HasColumnName("c_name")
                .HasComment("Название дисциплины");

            builder.Property(p => p.IsDeleted).HasColumnName("f_is_deleted")
                .HasColumnType(ColumnType.Bool).HasDefaultValue(false).HasComment("Флаг мягкого удаления");

            builder.ToTable(TableName);
        }
    }
}
