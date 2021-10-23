using Ardalis.GuardClauses;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Playground.NET6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaygroundController : ControllerBase
    {
        public PlaygroundController()
        {

        }

        [HttpGet("names")]
        public IActionResult GetNames(int num)
        {
            num = Guard.Against.NegativeOrZero(num, nameof(num));
                        
            var faker = new Faker();

            var names = Enumerable.Range(0, num)
                .Select(_ => faker.Name.FirstName() + " " + faker.Name.LastName())
                .ToList();

            return Ok(names);
        }
    }
}
