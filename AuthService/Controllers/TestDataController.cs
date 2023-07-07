using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly ITestDataService _testDataService;

        public TestDataController(ITestDataService testDataService)
        {
            _testDataService = testDataService;
        }

        [Route("AddTestdata")]
        [HttpGet]
        public async Task<ActionResult> CreateTestData()
        {
            await _testDataService.CreateTestData();
            return Ok();
        }
    }
}
