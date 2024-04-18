
using Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;
using SiliconWebApi.Attributes;
using WebApi.Services;

namespace SiliconWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
//[UseApiKey]
public class SubscribersController(SubscribeService subscribeService) : ControllerBase
{
    private readonly SubscribeService _subscribeService = subscribeService;


    [HttpPost] // create i CRUD
    public async Task<IActionResult> Create(SubscriberDto dto) 
    {
        if (ModelState.IsValid)
        {
            try
            {
                var subscribeEntity = await _subscribeService.CreateAsync(dto);
                if (subscribeEntity != null)
                {
                    return Created("", null); //skickar tillbaka statuskod 201, behöver inte skicka tillbaka någon data här
                }
            }
            catch { return Problem("unable to create subscription"); }  //500 meddelande

            return Conflict("Your email address is already subscribed"); // om det redan finns en email registerad skicka tillbaka 409
        }
        return BadRequest();
    }

    [HttpGet("{id}")] // Read i CRUD hämtar ut en specifik baserat på id
    public async Task<IActionResult> GetOne(int id)
    {
        var subscriber = await _subscribeService.GetOneByIdAsync(id); //hämtar en baserat på id från metoden i min service
        if (subscriber != null)
        {
            return Ok(subscriber);
        }

        return NotFound();
    }

    [HttpGet] // Read i CRUD
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await _subscribeService.GetAllAsync();
        if (subscribers.Any())
        {
            return Ok(subscribers); //om det finns några i databasen så skicka tillbaka dessa, ok 200
        }

        return NotFound();
    }

    [HttpPut("{id}")] // Update i CRUD
    public async Task<IActionResult> Update(int id, string email)
    {
        if (ModelState.IsValid)
        {
            var updatedSubscriber = await _subscribeService.UpdateAsync(id, email);

            if (updatedSubscriber != null)
            {
                return Ok(updatedSubscriber); //returnera om den har uppdaterats korrekt
            }
        }
        return NotFound();
    }

    [HttpDelete("{id}")] // Delete i CRUD
    public async Task<IActionResult> Delete(int id)
    {
        var subscriber = await _subscribeService.GetOneByIdAsync(id);
        if (subscriber != null)
        {
            await _subscribeService.DeleteAsync(id);
            return Ok();
        }
        return NotFound();
    }

}
