using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TrustGuard_API.Models;
using TrustGuard_API.Services;

namespace TrustGuard_API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class MessagesController : ControllerBase
{
    private readonly MessagesService _messagesService;

    public MessagesController(MessagesService messagesService)
    {
        _messagesService = messagesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var existingMessage = await _messagesService.GetAsync();

        if (existingMessage is null)
        {
            return NotFound();
        }
        return Ok(existingMessage);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var existingMessage = await _messagesService.GetAsync(id);

        if (existingMessage is null)
        {
            return NotFound();
        }
        return Ok(existingMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Messages messageInput)
    {
        await _messagesService.CreateAsync(messageInput);
        return CreatedAtAction(nameof(Get), new { id = messageInput.Id }, messageInput);
    }
}