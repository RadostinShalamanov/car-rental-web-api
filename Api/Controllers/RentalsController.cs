using System;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Rental;
using Api.Infrastructure.ResponseDTOs.Rental;
using Api.Infrastructure.ResponseDTOs.Shared;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly RentalService _service;

        public RentalsController(RentalService service)
        {
            _service=service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rentals=_service.GetAll();

            return Ok(rentals.Select(r=>new RentalResponseDto
            {
                Id=r.Id,
                UserId=r.UserId,
                CarId=r.CarId,
                PickupLocationId=r.PickupLocationId,
                ReturnLocationId=r.ReturnLocationId,
                PickupDate=r.StartDate,
                ReturnDate=r.EndDate,
                Payments=r.Payments.Select(p=>new IdNameDTO
                {
                    Id=p.Id,
                    Name=p.Amount.ToString()
                }).ToList()
            }).ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var rental=_service.GetById(id);

            if (rental == null)
            {
                throw new InvalidOperationException("Not Found");
            }

            var dto=new RentalResponseDto
            {
                Id=rental.Id,
                UserId=rental.UserId,
                CarId=rental.CarId,
                PickupLocationId=rental.PickupLocationId,
                ReturnLocationId=rental.ReturnLocationId,
                PickupDate=rental.StartDate,
                ReturnDate=rental.EndDate,
                 Payments=rental.Payments.Select(p=>new IdNameDTO
                {
                    Id=p.Id,
                    Name=p.Amount.ToString()
                }).ToList()
            };

             return Ok(dto);
        }


        [HttpGet("by-car/{carId}")]
        public IActionResult GetByCarId(int carId)
        {
            var rentals=_service.GetByCarId(carId);

             return Ok(rentals.Select(r=>new RentalResponseDto
            {
                Id=r.Id,
                UserId=r.UserId,
                CarId=r.CarId,
                PickupLocationId=r.PickupLocationId,
                ReturnLocationId=r.ReturnLocationId,
                PickupDate=r.StartDate,
                ReturnDate=r.EndDate,
                 Payments=r.Payments.Select(p=>new IdNameDTO
                {
                    Id=p.Id,
                    Name=p.Amount.ToString()
                }).ToList()
            }).ToList());
        }

        
        [HttpGet("by-user/{userId}")]
        public IActionResult GetByUserId(int userId)
        {
            var rentals=_service.GetByUserId(userId);

             return Ok(rentals.Select(r=>new RentalResponseDto
            {
                Id=r.Id,
                UserId=r.UserId,
                CarId=r.CarId,
                PickupLocationId=r.PickupLocationId,
                ReturnLocationId=r.ReturnLocationId,
                PickupDate=r.StartDate,
                ReturnDate=r.EndDate,
                Payments=r.Payments.Select(p=>new IdNameDTO
                {
                    Id=p.Id,
                    Name=p.Amount.ToString()
                }).ToList()
            }).ToList());
        }


        [HttpPost]
        public IActionResult Post([FromBody] RentalCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var rental = new Rental
            {
              UserId= dto.UserId,
              CarId=dto.CarId,
              PickupLocationId=dto.PickupLocationId,
              ReturnLocationId=dto.ReturnLocationId,
              StartDate=dto.PickupDate,
              EndDate=dto.ReturnDate
            };

            _service.Create(rental);

            return Ok(rental);
            
        }


        [HttpPut("{id}")]

        public IActionResult Put(int id,[FromBody]RentalUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Rental forUpdate=_service.GetById(id);
            if (forUpdate == null)
            {
                return NotFound();
            }

            forUpdate.PickupLocationId=dto.PickupLocationId;
            forUpdate.ReturnLocationId=dto.ReturnLocationId;
            forUpdate.StartDate=dto.PickupDate;
            forUpdate.EndDate=dto.ReturnDate;

            _service.Update(forUpdate);
            return Ok(forUpdate);
            
        }


        [HttpDelete("{id}")]

        public IActionResult Delete(int id)
        {
            Rental forDelete=_service.GetById(id);
            if (forDelete == null)
            {
                return NotFound();
            }

            _service.Delete(forDelete);
            return Ok(forDelete);
        }
    }
}
