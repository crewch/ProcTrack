using AuthService.Exceptions;
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
            try
            {
                await _testDataService.CreateTestData();
                return Ok();
            } catch (ConflictException ex)
            {
                return StatusCode(409, ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
