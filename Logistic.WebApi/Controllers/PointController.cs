using Logistic.DAL.Repositories.Abstractions;
using Logistic.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PointController : ControllerBase
{
    private readonly IPointRepository _pointRepository;

    public PointController(IPointRepository pointRepository)
    {
        _pointRepository = pointRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var routes = await _pointRepository.QueryEntities(includeAllChildren: false)
            .Select(x => new
            {
                x.Id,
                x.Name
            })
            .ToListAsync();

        return Ok(routes);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _pointRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateOrganizationBody body)
    {
        var entityToUpdate = new DAL.Entities.Point
        {
            Id = body.Id,
            Name = body.Name
        };
        await _pointRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateOrganizationBody body)
    {
        var entityToCreate = new DAL.Entities.Point
        {
            Name = body.Name
        };
        await _pointRepository.Add(entityToCreate);

        return NoContent();
    }
}