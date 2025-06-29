using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<MaintenanceSchedule> MaintenanceSchedule { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set table names (optional if you want exact naming)
        modelBuilder.Entity<Equipment>().ToTable("equipment");
        modelBuilder.Entity<MaintenanceSchedule>().ToTable("maintenance_schedule");

        // Optional: configure relationships if not using data annotations
        modelBuilder.Entity<MaintenanceSchedule>()
            .HasOne(m => m.Equipment)
            .WithMany() // or .WithMany(e => e.MaintenanceSchedules) if collection exists
            .HasForeignKey(m => m.EquipmentId);
    }
}
