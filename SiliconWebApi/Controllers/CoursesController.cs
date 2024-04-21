
using Microsoft.AspNetCore.Mvc;
using SiliconWebApi.Attributes;
using WebApi.Services;

namespace SiliconWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class CoursesController(CourseService courseService) : ControllerBase
    {
        private readonly CourseService _courseService = courseService;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            if (courses.Any())
            {
                return Ok(courses); //om det finns några i databasen så skicka tillbaka dessa, ok 200
            }
            return NotFound(); //annars skicka tillbaka notfound 404
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var course = await _courseService.GetOneByIdAsync(id); //hämtar en baserat på id från metoden i min service
            if (course != null)
            {
                return Ok(course);
            }

            return NotFound();
        }
    }
}
