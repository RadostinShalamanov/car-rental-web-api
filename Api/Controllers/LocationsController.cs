using System;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Locations;
using Api.Infrastructure.ResponseDTOs.Locations;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;
        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var locations=_locationService.GetAll();

            return Ok(locations.Select(l=>new LocationListResponseDto
            {
                Id=l.Id,
                Name=l.Name,
                Address=l.Address
            }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var location = _locationService.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(new LocationResponseDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                Cars=location.CarLocations.Select(cl=>cl.Car)
                .Select(c=>new LocationCarDto
                {
                    Id=c.Id,
                    Name=$"{c.Brand} {c.Model}",
                    PricePerDay=c.PricePerDay
                })
                .ToList()
            });
        }


        [HttpPost]
        public IActionResult Post([FromBody] LocationRequestDto dto)
        {
             if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var location = new Location
            {
                Name=dto.Name,
                Address=dto.Address
            };

            _locationService.Create(location);

            return Ok(location);

        }

        [HttpPut("{id}")]

        public IActionResult Put(int id,[FromBody] LocationRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Location forUpdate = _locationService.GetById(id);
            if (forUpdate == null)
                return NotFound();

            forUpdate.Name=dto.Name;
            forUpdate.Address=dto.Address;

            _locationService.Update(forUpdate);
            return Ok(forUpdate);
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            Location forDelete = _locationService.GetById(id);
            if (forDelete == null)
                throw new Exception("Car not found");

            _locationService.Delete(forDelete);

            return Ok(forDelete);
        }


    }
}
