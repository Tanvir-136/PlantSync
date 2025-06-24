using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Equipment> EquipmentList { get; set; } // Changed from Equipments to EquipmentList

    [BindProperty]
    public Equipment Equipment { get; set; }

    public async Task OnGetAsync()
    {
        EquipmentList = await _context.Equipment.ToListAsync(); // Now matches DbSet name
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (!ModelState.IsValid)
        {
            EquipmentList = await _context.Equipment.ToListAsync();
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

        if (await TryUpdateModelAsync<Equipment>(
            equipmentToUpdate,
            "equipment",
            e => e.Name, e => e.Type, e => e.InstalledDate))
        {
            await _context.SaveChangesAsync();
            return RedirectToPage();
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
}