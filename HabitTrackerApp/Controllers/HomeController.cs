using HabitTrackerApp.Data;
using HabitTrackerApp.Models;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            Habits = _context.Habits.ToList(),
            Videos = _context.MotivationalVideos.ToList()
        };

        return View(viewModel);
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }

}