using SiliconWebApi.Contexts;
using WebApi.Entities;

namespace WebApi.Repositories;

public class CourseRepository(DataContext_WebApi context) : Repo<CourseEntity>(context)
{
   
}
// fick felmeddelande vid uppstart av WebApi i min progam.cs fil. Hade satt referencen till DataContext fel här
// gick iväg till min infrastructure istället genom DataContext