using System;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Payments;
using Api.Infrastructure.ResponseDTOs.Payments;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _service;

        public PaymentsController(PaymentService service)
        {
            _service=service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var payments=_service.GetAll();

            return Ok(payments.Select(p=>new PaymentResponseDto
            {
                Id=p.Id,
                RentalId=p.RentalId,
                Amount=p.Amount,
                PaymentDate=p.PaymentDate
            }).ToList()) ;      
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var payment=_service.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            var dto=new PaymentResponseDto
            {
                Id=payment.Id,
                RentalId=payment.RentalId,
                Amount=payment.Amount,
                PaymentDate=payment.PaymentDate
            }; 


            return Ok(dto);
        }


        [HttpGet("by-rental/{rentalId}")]
        public IActionResult GetByRentalId(int rentalId)
        {
            var payments=_service.GetByRentalId(rentalId);
            return Ok(payments.Select(p=>new PaymentResponseDto
            {
                Id=p.Id,
                RentalId=p.RentalId,
                Amount=p.Amount,
                PaymentDate=p.PaymentDate
            }).ToList());                  

        }


        [HttpPost]
        public IActionResult Post([FromBody]PaymentRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var payment = new Payment
            {
              RentalId=dto.RentalId,
              Amount=dto.Amount
            };

            _service.Create(payment);

            return Ok(payment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Payment forDelete = _service.GetById(id);
            if (forDelete == null)
                throw new Exception("Car not found");

            _service.Delete(forDelete);

            return Ok(forDelete);
        }

    }
}
