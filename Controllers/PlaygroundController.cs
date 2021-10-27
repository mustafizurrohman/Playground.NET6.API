namespace Playground.NET6.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaygroundController : ControllerBase
    {
        private readonly IDataService DataService;

        public PlaygroundController(IDataService dataService)
        {
            DataService =  Guard.Against.Null(dataService, nameof(dataService));
        }

        [HttpGet("names")]
        public IActionResult GetNames(int num)
        {
            var people = DataService.GetPeople(num);
            return Ok(people);
        }

        [HttpGet("maxage")]
        public IActionResult GetNamesZipped(int num)
        {
            var people = DataService.GetPeople(num);
            var maxAge = people.MaxBy(p => p.Age)?.Age;
            
            return Ok(maxAge);
        }

        [HttpGet("chunk")]
        public IActionResult LinqChunkTest(int num)
        {
            var people = DataService.GetPeople(num);
            
            var chunked = people
                .Select(p => p.FirstName + " " + p.LastName)
                .Chunk((int)Math.Sqrt(num));

            return Ok(chunked);
        }

        [HttpGet("overload")]
        public IActionResult OverLoadTest(int num)
        {
            num = Guard.Against.NegativeOrZero(num, nameof(num));

            var randomNumbers = Enumerable.Range(1, num)
                .Select(x => new Faker().Random.Number(10, 100))
                .ToList();
            
            var numberslessThan10 = randomNumbers.FirstOrDefault(x => x < 10, -1);

            return Ok(numberslessThan10);
        }

        [HttpGet("cis")]
        public IActionResult ConstantInterpolatedStrings()
        {
            const string languageReleasePrefix = "C# 10";
            const string languageRelease = $"{languageReleasePrefix } to be released in November 2021.";
            
            return Ok(languageRelease);
        }

        //[HttpGet("lambda")]
        //public IActionResult LambdaImprovements()
        //{
        //    var helloWorld = () => "Hello World";
        //}
    }
}
