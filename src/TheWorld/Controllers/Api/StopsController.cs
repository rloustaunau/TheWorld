using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TheWorld.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        // GET: /<controller>/
        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Get(String tripName)
        {
            try  { 
             var trip = _repository.GetTripByName(tripName);

              return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s=> s.Order)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get stops: {0} ", ex);
            }
            return BadRequest("Failed to get stops");
        }
        
    }
}
