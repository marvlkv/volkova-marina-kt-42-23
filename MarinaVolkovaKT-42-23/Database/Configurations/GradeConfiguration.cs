using MarinaVolkovaKT_42_23.Models;
using MarinaVolkovaKT_42_23.Database.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarinaVolkovaKT_42_23.Database.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        private const string TableName = "cd_grade";

        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(p => p.GradeId).HasName($"pk_{TableName}_grade_id");
            builder.Property(p => p.GradeId).HasColumnName("grade_id")
                .HasComment("Идентификатор записи оценки").ValueGeneratedOnAdd();

            builder.Property(p => p.Value).IsRequired().HasColumnName("c_value").HasComment("оценка");

            // Связь со студентом
            builder.Property(p => p.StudentId).IsRequired().HasColumnName("f_student_id")
                .HasColumnType(ColumnType.Int).HasComment("Идентификатор студента");

            builder.HasOne(p => p.Student).WithMany(g => g.Grades)
                .HasForeignKey(p => p.StudentId).HasConstraintName("fk_f_student_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.StudentId, $"idx_{TableName}_fk_f_student_id");
            builder.Navigation(p => p.Student).AutoInclude();

            // Связь с дисциплиной
            builder.Property(p => p.DisciplineId).IsRequired().HasColumnName("f_discipline_id")
                .HasColumnType(ColumnType.Int).HasComment("Идентификатор дисциплины");

            builder.HasOne(p => p.Discipline).WithMany(g => g.Grades)
                .HasForeignKey(p => p.DisciplineId).HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");
            builder.Navigation(p => p.Discipline).AutoInclude();

            builder.ToTable(TableName);
        }
    }
}
