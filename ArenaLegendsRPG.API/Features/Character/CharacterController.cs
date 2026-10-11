using ArenaLegendsRPG.API.Features.Character.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ArenaLegendsRPG.API.Features.Character;

[ApiController]
[Route("api/characters")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _service;

    public CharacterController(ICharacterService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterResponse>> Get(int id)
    {
        var characterResponse = await _service.GetAsync(id);
        if (characterResponse == null)
        {
            return NotFound();
        }
        return Ok(characterResponse);
    }

    [HttpPost]
    public async Task<ActionResult<CharacterResponse>> Save(SaveCharacterRequest request)
    {
        try
        {
            int savedId = await _service.SaveAsync(request);
            return Created($"/api/characters/{savedId}", new SaveCharacterResponse { SavedId = savedId });
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

    }
}