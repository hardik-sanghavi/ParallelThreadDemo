using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ParallelThreadDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParrallelThreadController : ControllerBase
    {

        private readonly ILogger<ParrallelThreadController> _logger;

        public ParrallelThreadController(ILogger<ParrallelThreadController> logger)
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
            Task<string> task1 = Task.Run(() => RetriveDataFromMockAPI1());
            Task<string> task2 = Task.Run(() => RetriveDataFromMockAPI2());

            Task.WaitAll(task1, task2);
            sw.Stop();

            var response = new
            {
                mockAPI1Response = task1.Result,
                mockAPI2Response = task2.Result,
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
