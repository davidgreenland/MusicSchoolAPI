using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;

namespace MusicSchool;

public class MusicSchoolDBContext: DbContext
{
    public MusicSchoolDBContext(DbContextOptions<MusicSchoolDBContext> options) : base(options)
    {
    }

    public DbSet<Instrument> Instrument { get; set; } = null!;
    public DbSet<Student> Student { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasMany(e => e.Instruments)
            .WithMany(e => e.Students)
            .UsingEntity<StudentInstrument>();

        modelBuilder.Entity<Instrument>()
            .HasOne(e => e.Category)
            .WithMany(e => e.Instruments)
            .HasForeignKey(e => e.CategoryId);
    }

public DbSet<MusicSchool.Models.StudentInstrument> StudentInstrument { get; set; } = default!;

public DbSet<MusicSchool.Models.Category> Category { get; set; } = default!;
}
