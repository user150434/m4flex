using Microsoft.AspNetCore.Mvc;
using huisapi.models;

namespace huisapi.Controllers;

[ApiController]
[Route("[controller]")]
public class LampController : ControllerBase
{
    private readonly ILogger<LampController> _logger;

    public LampController(ILogger<LampController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetLampen")]
    public Lampen Get()
    {
        Lampen lampen = new Lampen();
        return lampen   ;
    }

}
