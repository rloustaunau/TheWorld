using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TheWorld.ViewModels;
using TheWorld.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{

    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        // GET: /<controller>/
        public StopsController(IWorldRepository repository, ILogger<StopsController> logger, GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("/api/trips/{tripName}/stops")]
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

        [HttpPost("/api/trips/{tripName}/stops")]
        public  async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {

                //If VM is valid
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    //Lookup the GeoCodes
                    var result = await _coordsService.GetCoordsAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        //Save to the Database

                        _repository.AddStop(tripName, newStop);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed", ex);
          
            }
            return BadRequest("Failed to save new stop");
        }
        
    }
}
