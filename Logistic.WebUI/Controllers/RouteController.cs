using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Logistic.DAL.Entities;
using Logistic.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logistic.WebUI.Controllers
{
    public class RouteController : BaseController<IRouteRepository, Route>
    {
        private readonly IPointRepository _pointRepository;

        public RouteController(IRouteRepository repository, IPointRepository pointRepository) : base(repository)
        {
            _pointRepository = pointRepository;
        }

        protected override Expression<Func<Route, bool>> SearchExpression(string searchString)
        {
            return route => route.Name.ToLower().Contains(searchString.ToLower().Trim());
        }

        [HttpGet]
        public IActionResult Report()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ReportData(DateTime startDate, DateTime endDate)
        {
            var data = await Repository.QueryEntities(includeAllChildren: false)
                .Include(x => x.Transportations)
                .SelectMany(x => x.Transportations)
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .Include(x => x.Route)
                .Include(x => x.Rate)
                .Select(x => new
                {
                    Route = x.Route.Name,
                    x.Rate,
                    Volume = x.Cargo.Volume * x.CargoCount,
                    Weight = x.Cargo.Weight * x.CargoCount,
                    x.Date
                })
                .ToListAsync();

            var result = data.Select(x =>
                (x.Route, Rate: x.Rate.Name, CarryingPrice: x.Rate.CarryingRate * x.Weight, VolumePrice: x.Rate.VolumeRate * x.Volume, x.Date)
            ).ToList();

            return PartialView(result);
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var items = await GetSearchQuery(searchString)
                .ToListAsync();

            return View(ToPagedList(items, page));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await Repository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await Repository.GetEntityById(id);

            var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            ViewBag.Points = points;

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Route item)
        {
            if (!ModelState.IsValid)
            {
                var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                    .ToListAsync();
                ViewBag.Points = points;
                return View(item);
            }

            await Repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                .ToListAsync();
            ViewBag.Points = points;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Route item)
        {
            if (!ModelState.IsValid)
            {
                var points = await _pointRepository.QueryEntities(includeAllChildren: false)
                    .ToListAsync();
                ViewBag.Points = points;
                return View(item);
            }

            await Repository.Add(item);

            return RedirectToAction(nameof(Index));
        }
    }
}