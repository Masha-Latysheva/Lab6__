using System;
using System.Linq;
using System.Threading.Tasks;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Logistic.WebUI.Controllers
{
    [Route("[controller]")]
    public class WorkAnalysisController : Controller
    {
        private readonly ITransportationRepository _transportationRepository;

        public WorkAnalysisController(ITransportationRepository transportationRepository)
        {
            _transportationRepository = transportationRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("WorkAnalysis/Filling")]
        public async Task<IActionResult> Filling()
        {
            var filling = _transportationRepository.QueryEntities(includeAllChildren: false)
                .Include(x => x.Rate)
                .Include(x => x.Cargo)
                .ToList()
                .GroupBy(x => x.Rate.Id)
                .Select(x => new
                {
                    Rate = x.First().Rate.Name,
                    RateCarrying = x.First().Rate.CarryingRate,
                    CargoWeight = x
                        .Select(z => z.Cargo.Weight)
                        .Sum()
                })
                .Select(x => (x.Rate, x.RateCarrying == 0 ? 0 : (int)((double)x.CargoWeight / x.RateCarrying * 100)))
                .ToList();

            return View(filling);
        }

        [HttpGet("GetDriverStats")]
        public async Task<IActionResult> GetDriverStats(DateTime firstDate, DateTime secondDate)
        {
            secondDate = secondDate.AddHours(23);
            var queryResult = await _transportationRepository.QueryEntities(includeAllChildren: false)
                .Where(x => x.Date >= firstDate && x.Date <= secondDate)
                .Include(x => x.Driver)
                .Include(x => x.Rate)
                .ToListAsync();

            var grouped = queryResult
                .GroupBy(x => x.Driver.FirstName + " " + x.Driver.LastName)
                .Select(x =>
                    (Driver: x.Key,
                    TransportationCount: x.Count(),
                    Rates: x.GroupBy(t => t.Rate.Name).Select(x => (Rate: x.Key, Count: x.Count())).ToList()
                ))
                .ToList();

            return PartialView(grouped);
        }

        [HttpGet("Stats")]
        public async Task<IActionResult> GetStats(DateTime firstDate, DateTime secondDate)
        {
            secondDate = secondDate.AddHours(23);
            var queryResult = await _transportationRepository.QueryEntities(includeAllChildren: false)
                .Where(x => x.Date >= firstDate && x.Date <= secondDate)
                .Include(t => t.Rate)
                .Include(t => t.Cargo)
                .Select(x => new
                {
                    TotalVolume = x.Cargo.Volume * x.CargoCount,
                    TotalWeight = (double)x.Cargo.Weight * x.CargoCount / 1000,
                    TotalSum = x.Rate.CarryingRate * x.Cargo.Weight * x.CargoCount + x.Rate.VolumeRate * x.Cargo.Volume * x.CargoCount
                })
                .ToListAsync();

            if (queryResult.Count == 0)
            {
                return Json("");
            }

            var totalSum = queryResult.Sum(x => x.TotalSum);
            var totalVolume = queryResult.Sum(x => x.TotalVolume);
            var totalWeight = queryResult.Sum(x => x.TotalWeight);

            var totalResult = new
            {
                TotalSum = totalSum,
                TotalVolume = totalVolume,
                TotalWeight = totalWeight
            };

            return Json(totalResult);
        }
    }
}