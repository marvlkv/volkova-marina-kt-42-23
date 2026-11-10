using MarinaVolkovaKT_42_23.Models;
using MarinaVolkovaKT_42_23.Database.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarinaVolkovaKT_42_23.Database.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        private const string TableName = "cd_student";

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Первичный ключ
            builder.HasKey(p => p.StudentId).HasName($"pk_{TableName}_student_id");
            builder.Property(p => p.StudentId).ValueGeneratedOnAdd();
            builder.Property(p => p.StudentId).HasColumnName("student_id").HasComment("Идентификатор записи студента");

            // Колонки
            builder.Property(p => p.FirstName)
                .IsRequired().HasColumnName("c_student_firstname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Имя студента");

            builder.Property(p => p.LastName)
                .IsRequired().HasColumnName("c_student_lastname")
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Фамилия студента");

            builder.Property(p => p.IsDeleted)
                .HasColumnName("f_is_deleted").HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false).HasComment("Флаг мягкого удаления");

            // Внешний ключ на группу
            builder.Property(p => p.GroupId)
                .IsRequired().HasColumnName("f_group_id").HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор группы");

            // Связь: один студент - одна группа, у группы много студентов
            builder.HasOne(p => p.Group).WithMany(g => g.Students)
                .HasForeignKey(p => p.GroupId).HasConstraintName("fk_f_group_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.GroupId, $"idx_{TableName}_fk_f_group_id");
            builder.Navigation(p => p.Group).AutoInclude();
            builder.ToTable(TableName);
        }
    }
}
