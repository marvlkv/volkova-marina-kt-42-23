using MarinaVolkovaKT_42_23.Models;
using MarinaVolkovaKT_42_23.Database.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarinaVolkovaKT_42_23.Database.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        private const string TableName = "cd_group";

        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(p => p.GroupId).HasName($"pk_{TableName}_group_id");
            builder.Property(p => p.GroupId).ValueGeneratedOnAdd();
            builder.Property(p => p.GroupId).HasColumnName("group_id").HasComment("Идентификатор записи группы");

            builder.Property(p => p.Name).IsRequired().HasColumnName("c_group_name")
                .HasColumnType(ColumnType.String).HasMaxLength(100).HasComment("Название группы");

            builder.Property(p => p.Course).HasColumnName("c_group_course").HasColumnType(ColumnType.Int);

            // Внешний ключ на специальность
            builder.Property(p => p.SpecialityId).IsRequired().HasColumnName("f_specality_id")
                .HasColumnType(ColumnType.Int).HasComment("Идентификатор специальности");

            // Связь: одна группа - одна специальность, у специальности много групп
            builder.HasOne(p => p.Speciality).WithMany(g => g.Groups)
                .HasForeignKey(p => p.SpecialityId).HasConstraintName("fk_f_speciality_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.SpecialityId, $"idx_{TableName}_fk_f_specality_id");
            builder.Navigation(p => p.Speciality).AutoInclude();

            builder.Property(p => p.IsDeleted).HasColumnName("f_is_deleted")
                .HasColumnType(ColumnType.Bool).HasDefaultValue(false).HasComment("Флаг мягкого удаления");

            builder.ToTable(TableName);
        }
    }
}
