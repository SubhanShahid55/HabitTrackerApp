using HabitTrackerApp.Data;
using HabitTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerApp.Controllers
{
    public class HabitsController : Controller
    {
        private readonly AppDbContext _context;

        public HabitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Habits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Habits == null)
            {
                return NotFound();
            }

            var habit = await _context.Habits.FindAsync(id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // POST: Habits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Days,IsCompleted")] Habit habit)
        {
            if (id != habit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingHabit = await _context.Habits.FindAsync(habit.Id);

                    if (existingHabit == null)
                    {
                        return NotFound();
                    }

                    // Update properties
                    existingHabit.Name = habit.Name;
                    existingHabit.Description = habit.Description;
                    existingHabit.Days = habit.Days;

                    // Set completion status
                    if (habit.IsCompleted)
                    {
                        existingHabit.LastCompletedDate = DateTime.Today;
                        existingHabit.StreakCount++;
                    }
                    else
                    {
                        existingHabit.LastCompletedDate = null;
                        existingHabit.StreakCount = 0;
                    }

                    _context.Update(existingHabit);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = habit.IsCompleted ? "Habit marked as completed!" : "Completion removed.";

                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Habits.Any(e => e.Id == habit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(habit);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Habits == null)
            {
                return NotFound();
            }

            var habit = await _context.Habits
                .FirstOrDefaultAsync(m => m.Id == id);
            if (habit == null)
            {
                return NotFound();
            }

            return View(habit);
        }

        // POST: Habits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Habits == null)
            {
                return Problem("Entity set 'AppDbContext.Habits' is null.");
            }

            var habit = await _context.Habits.FindAsync(id);
            if (habit != null)
            {
                _context.Habits.Remove(habit);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Habit deleted successfully!";
            }

            return RedirectToAction("Index", "Home");
        }

        // POST: Habits/ToggleComplete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var habit = await _context.Habits.FindAsync(id);

            if (habit == null)
            {
                return NotFound();
            }

            // Toggle completion status
            if (habit.LastCompletedDate.HasValue && habit.LastCompletedDate.Value.Date == DateTime.Today)
            {
                habit.LastCompletedDate = null; // Mark incomplete
                habit.StreakCount = 0; // Reset streak
            }
            else
            {
                habit.LastCompletedDate = DateTime.Today; // Mark complete
                habit.StreakCount++; // Increment streak
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Habit status updated successfully!";
            return RedirectToAction("Index", "Home");
        }
        // GET: Habits/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Habit habit)
        {
            // Manually parse Days from FormCollection
            var rawDays = Request.Form["Days"].ToString();

            if (string.IsNullOrEmpty(rawDays))
            {
                ModelState.AddModelError("Days", "Please select at least one day.");
                return View(habit);
            }

            try
            {
                // Parse JSON-like string "[0,1,2]" into list of DayOfWeek
                var dayStrings = rawDays.Trim('[', ']')
                                       .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Select(int.Parse)
                                       .ToList();

                var selectedDays = dayStrings.Select(d => (DayOfWeek)d).ToList();

                habit.Days = selectedDays;
            }
            catch
            {
                ModelState.AddModelError("Days", "Invalid days selected.");
                return View(habit);
            }

            if (ModelState.IsValid)
            {
                _context.Add(habit);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Habit created successfully!";
                return RedirectToAction("Index", "Home");
            }

            return View(habit);
        }
    }
}