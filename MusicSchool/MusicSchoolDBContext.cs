using Microsoft.EntityFrameworkCore;
using MusicSchool.Models;

namespace MusicSchool;

public class MusicSchoolDBContext: DbContext
{
    public MusicSchoolDBContext(DbContextOptions<MusicSchoolDBContext> options) : base(options)
    {
    }

    public DbSet<Instrument> Instruments { get; set; } = null!;
}
