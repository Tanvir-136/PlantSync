using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Equipment> EquipmentList { get; set; }
    public IList<MaintenanceSchedule> MaintenanceSchedules { get; set; }
    public decimal TotalValue { get; set; }

    [BindProperty]
    public Equipment Equipment { get; set; }

    [BindProperty]
    public MaintenanceSchedule NewMaintenance { get; set; }

    public async Task OnGetAsync()
    {
        EquipmentList = await _context.Equipment.ToListAsync();
        TotalValue = EquipmentList.Sum(e => e.Value);

        MaintenanceSchedules = await _context.MaintenanceSchedule
            .Include(m => m.Equipment)
            .OrderBy(m => m.NextMaintenanceDate)
            .ToListAsync();

        if (EquipmentList == null || !EquipmentList.Any())
        {
            ModelState.AddModelError(string.Empty, "No equipment found.");
        }
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync();
            return Page();
        }

        _context.Equipment.Add(Equipment);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditAsync(int id)
    {
        var equipmentToUpdate = await _context.Equipment.FindAsync(id);

        if (equipmentToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync(
            equipmentToUpdate,
            "equipment",
            e => e.Name, e => e.Type, e => e.InstalledDate))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var equipment = await _context.Equipment.FindAsync(id);
        if (equipment != null)
        {
            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAddMaintenanceAsync()
    {
        if (!ModelState.IsValid)
        {
            await OnGetAsync(); // reload UI state
            return Page();
        }

        _context.MaintenanceSchedule.Add(NewMaintenance);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }
}
