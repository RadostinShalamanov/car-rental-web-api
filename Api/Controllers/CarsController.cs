using System;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Car;
using Api.Infrastructure.RequestDTOs.Categories;
using Api.Infrastructure.RequestDTOs.Features;
using Api.Infrastructure.RequestDTOs.Locations;
using Api.Infrastructure.ResponseDTOs.Cars;
using Api.Infrastructure.ResponseDTOs.Shared;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;
        public CarsController(CarService carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public IActionResult Get()
        {
           
            // return Ok(carService.GetAll());

            var cars=_carService.GetAll();
            return Ok(cars.Select(car=>new CarResponseDto
            {
                Id=car.Id,
                Brand=car.Brand,
                Model=car.Model,
                Year=car.Year,
                PricePerDay=car.PricePerDay,
                Categories=car.CarCategories.Select(cc=>new IdNameDTO
                {
                    Id=cc.Category.Id,
                    Name=cc.Category.Name
                }).ToList(),
                Features=car.CarFeatures.Select(cf=>new IdNameDTO
                {
                    Id=cf.Feature.Id,
                    Name=cf.Feature.Name
                }).ToList(),
                Locations=car.CarLocations.Select(cl=>new IdNameDTO
                {
                    Id=cl.Location.Id,
                    Name=cl.Location.Name
                }).ToList()
            }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var car=_carService.GetById(id);
            if(car==null)
                return NotFound();

            var dto=new CarResponseDto
            {
                Id=car.Id,
                Brand=car.Brand,
                Model=car.Model,
                Year=car.Year,
                PricePerDay=car.PricePerDay,
                Categories=car.CarCategories.Select(cc=>new IdNameDTO
                {
                    Id=cc.Category.Id,
                    Name=cc.Category.Name
                }).ToList(),
                Features=car.CarFeatures.Select(cf=>new IdNameDTO
                {
                    Id=cf.Feature.Id,
                    Name=cf.Feature.Name
                }).ToList(),
                Locations=car.CarLocations.Select(cl=>new IdNameDTO
                {
                    Id=cl.Location.Id,
                    Name=cl.Location.Name
                }).ToList()
                
            };

            return Ok(dto);
        }

        [HttpGet("available")]

        public IActionResult AvailableCars([FromQuery]DateOnly pickupDate,[FromQuery]DateOnly returnDate)
        {
            var cars=_carService.SearchAvailableCars(pickupDate,returnDate);
            if (cars == null)
            {
                return NotFound();
            }

            var dto=cars.Select(c=>new SearchAvailableCarsDto
            {
                Id=c.Id,
                Brand=c.Brand,
                Model=c.Model,
                Year=c.Year,
                PricePerDay=c.PricePerDay
            }).ToList();


            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CarRequestDTO dto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var car = new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                PricePerDay = dto.PricePerDay,
            
            };

            _carService.Create(car);

            return Ok(car);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CarRequestDTO dto)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Car forUpdate = _carService.GetById(id);
            if (forUpdate == null)
                return NotFound();

            forUpdate.Brand = dto.Brand;
            forUpdate.Model = dto.Model;
            forUpdate.Year = dto.Year;
            forUpdate.PricePerDay = dto.PricePerDay;
            

            _carService.Update(forUpdate);
            return Ok(forUpdate);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Car forDelete = _carService.GetById(id);
            if (forDelete == null)
                throw new Exception("Car not found");

            _carService.Delete(forDelete);

            return Ok(forDelete);
        }

        


        [HttpPut("{id}/categories")]
        public IActionResult SetCategories(int id, [FromBody] CarCategoriesUpdateDto dto)
        {
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _carService.SetCategories(id, dto.CategoryIds);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(); 
        }


        [HttpPut("{id}/features")]
        public IActionResult SetFeatures(int id, [FromBody] CarFeaturesUpdateDto dto)
        {
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _carService.SetFeatures(id, dto.FeatureIds);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(); 
        }


        [HttpPut("{id}/locations")]
        public IActionResult SetLocations(int id, [FromBody] CarLocationsUpdateDto dto)
        {
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _carService.SetLocations(id, dto.LocationIds);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok(); 
        }


        
    }
}
