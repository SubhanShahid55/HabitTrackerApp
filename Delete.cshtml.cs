using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HabitTrackerApp.Models;
using HabitTrackerApp.Data;
using System.Threading.Tasks;

namespace HabitTrackerApp.Pages.Habits
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Habit Habit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Habit = await _context.Habits.FindAsync(id);

            if (Habit == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Habit == null || Habit.Id == 0)
            {
                return NotFound();
            }

            var habit = await _context.Habits.FindAsync(Habit.Id);

            if (habit != null)
            {
                _context.Habits.Remove(habit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
