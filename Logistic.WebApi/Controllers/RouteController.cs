using Logistic.DAL.Repositories.Abstractions;
using Logistic.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly IPointRepository _pointRepository;
    private readonly IRouteRepository _routeRepository;


    public RouteController(IPointRepository pointRepository, IRouteRepository routeRepository)
    {
        _pointRepository = pointRepository;
        _routeRepository = routeRepository;
    }

    [HttpGet("Points")]
    public async Task<IActionResult> GetPoints()
    {
        var points = await _pointRepository.QueryEntities(includeAllChildren: false)
            .Select(x => new
            {
                x.Id,
                x.Name
            })
            .ToListAsync();

        return Ok(points);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var routes = await _routeRepository.QueryEntities(includeAllChildren: false)
            .Include(x => x.StartPoint)
            .Include(x => x.EndPoint)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.RouteLength,
                StartPoint = x.StartPoint.Name,
                EndPoint = x.EndPoint.Name,
                x.StartPointId,
                x.EndPointId
            })
            .ToListAsync();

        return Ok(routes);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _routeRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateRouteBody body)
    {
        var entityToUpdate = new Logistic.DAL.Entities.Route
        {
            Id = body.Id,
            RouteLength = body.RouteLength,
            StartPointId = body.StartPointId,
            Name = body.Name,
            EndPointId = body.EndPointId
        };
        await _routeRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateRouteBody body)
    {
        var entityToCreate = new Logistic.DAL.Entities.Route
        {
            RouteLength = body.RouteLength,
            StartPointId = body.StartPointId,
            Name = body.Name,
            EndPointId = body.EndPointId
        };
        await _routeRepository.Add(entityToCreate);

        return NoContent();
    }
}