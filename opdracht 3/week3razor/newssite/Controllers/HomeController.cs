using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using newssite.Models;
using System.Text.Json;

namespace newssite.Controllers;





public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    private List<NewsItem> NewsItems = new List <NewsItem>();

    public HomeController(ILogger<HomeController> logger)
    {    _logger = logger;

    string json = System.IO.File.ReadAllText("data/news.json");
    NewsItems = JsonSerializer.Deserialize<List<NewsItem>>(json);  


 
    }

    public IActionResult Index()
    {
        return View(NewsItems);
    }

    public IActionResult Privacy()
    {
        Privacy Models = new Privacy();
        Models.PolicyText = "We do not track anything";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
