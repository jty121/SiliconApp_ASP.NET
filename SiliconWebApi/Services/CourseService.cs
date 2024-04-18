using System.Diagnostics;
using WebApi.Entities;
using WebApi.Repositories;

namespace WebApi.Services;

public class CourseService(CourseRepository courseRepository)
{
    private readonly CourseRepository _courseRepository = courseRepository;


    public async Task<CourseEntity> GetOneByIdAsync(int id)
    {
        try
        {
            var courseEntities = await _courseRepository.GetOneAsync(x => x.Id == id);
            return courseEntities;

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }

    public async Task<IEnumerable<CourseEntity>> GetAllAsync()
    {
        try
        {
            var courseEntities = await _courseRepository.GetAllAsync();
            if (courseEntities != null)
            {
                return courseEntities;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
}

