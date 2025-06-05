using HabitTrackerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HabitTrackerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Habit> Habits { get; set; }
        public DbSet<MotivationalVideo> MotivationalVideos { get; set; }

    }
}