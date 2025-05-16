using Microsoft.AspNetCore.Mvc;
using testKol.Exceptions;
using testKol.Models.DTOs;
using testKol.Services;

namespace testKol.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VisitsController(IVisitService visitService) : ControllerBase
{
    private readonly IVisitService _visitService = visitService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        
        try
        {
            var t = await _visitService.GetClientVisit(id);
            return Ok(t);
        }
        catch (NotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound(e);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Post(VisitAddDTO visitAddDTO)
    {

        try
        {
            await _visitService.AddClientVisit(visitAddDTO);
            return Ok();
        }
        catch (NotFoundException e)
        {
            Console.WriteLine(e);
            return NotFound(e);
        }
        catch (ConflictException e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }
}