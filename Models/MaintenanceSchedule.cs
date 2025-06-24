using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MaintenanceSchedule
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int EquipmentId { get; set; }

    [ForeignKey("EquipmentId")]
    public Equipment? Equipment { get; set; }

    [Required]
    public DateTime NextMaintenanceDate { get; set; }

    public string? Notes { get; set; }
}
