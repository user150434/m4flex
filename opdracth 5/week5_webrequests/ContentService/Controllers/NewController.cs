using Microsoft.AspNetCore.Mvc;


namespace ContentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewController : Controller
    {
        [HttpGet]

        public string[] Get()
        {
            return new string[] { "breaking news", "slow news" };
        }
    }
}