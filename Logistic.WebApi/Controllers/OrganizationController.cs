using Logistic.DAL.Repositories.Abstractions;
using Logistic.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationController(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var routes = await _organizationRepository.QueryEntities(includeAllChildren: false)
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
        await _organizationRepository.Delete(id);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> EditAsync([FromBody] UpdateOrganizationBody body)
    {
        var entityToUpdate = new DAL.Entities.Organization
        {
            Id = body.Id,
            Name = body.Name
        };
        await _organizationRepository.Update(entityToUpdate);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateOrganizationBody body)
    {
        var entityToCreate = new DAL.Entities.Organization
        {
            Name = body.Name
        };
        await _organizationRepository.Add(entityToCreate);

        return NoContent();
    }
}