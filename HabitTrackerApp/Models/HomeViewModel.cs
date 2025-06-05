namespace HabitTrackerApp.Models
{
    public class HomeViewModel
    {
        public List<Habit> Habits { get; set; } = new List<Habit>();
        public List<MotivationalVideo> Videos { get; set; } = new List<MotivationalVideo>();
    }
}