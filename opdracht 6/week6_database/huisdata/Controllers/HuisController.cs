using Microsoft.AspNetCore.Mvc;
using huisdata.Models;
using huisdata.DBData;

namespace huisdata.Controllers;

[ApiController]
[Route("[controller]")]
public class HuisController : ControllerBase
{

    private readonly ILogger<HuisController> _logger;
    private readonly HuisDataContext huisContext;

    public HuisController(ILogger<HuisController> logger, HuisDataContext huisContext)
    {
        _logger = logger;
        this.huisContext = huisContext;
    }

    [HttpGet()]
    public IEnumerable<EnergyVerbruik> Get()
    {
        return huisContext.GetEnergyVebruik();
    }
    
}
