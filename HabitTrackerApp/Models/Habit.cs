using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitTrackerApp.Models
{
    public class Habit
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        // Collection of days the habit repeats on
        public ICollection<DayOfWeek> Days { get; set; } = new List<DayOfWeek>();

        // Streak tracking
        public DateTime? LastCompletedDate { get; set; }
        public int StreakCount { get; set; } = 0;

        // Optional field for manual completion toggle
        public bool IsCompleted { get; set; }
    }
}