using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;

namespace SiliconApp.Controllers;

public class CoursesController : Controller
{
    [Route("/courses")]
    [HttpGet]
    public async Task<IActionResult> Courses()
    {
        //hämtar in information till vår vy med hjälp av httpclient som går till url:en på wep apiet.
        using var http = new HttpClient();
        var response = await http.GetAsync("https://localhost:7296/api/courses");
        var json = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(json);

        return View(data);
    }

    [Route("/courses/details")]
    [HttpGet]
    public IActionResult CourseDetail()
    {
        return View();
    }

    [Route("/courses/saved")]
    [HttpGet]
    public IActionResult SavedCourses()
    {
        return View();
    }
}
