using Api_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_App.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.FullName).HasMaxLength(200).IsRequired();
            builder.Property(s => s.Email).HasMaxLength(100).HasDefaultValue("test@gmail.com").IsRequired(false);
            builder.Property(s => s.Age).HasDefaultValue(18).IsRequired();
        }
    }
}
