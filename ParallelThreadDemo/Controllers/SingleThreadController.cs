using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParallelThreadDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SingleThreadController : ControllerBase
    {        

        private readonly ILogger<SingleThreadController> _logger;

        public SingleThreadController(ILogger<SingleThreadController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            Stopwatch sw = Stopwatch.StartNew();
            var response1 = await RetriveDataFromMockAPI1();
            var response2 = await RetriveDataFromMockAPI2();
            sw.Stop();

            var response = new
            {
                mockAPI1Response = response1,
                mockAPI2Response = response2,
                totalTime = sw.ElapsedMilliseconds
            };
            return Ok(response);
        }

        private static async Task<string> RetriveDataFromMockAPI1()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(new Uri(Constants.MockApi1));
                return await result.Content.ReadAsStringAsync();
            }
        }

        private static async Task<string> RetriveDataFromMockAPI2()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(new Uri(Constants.MockApi2));
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}
