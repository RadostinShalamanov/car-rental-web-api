using System;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Role;
using Api.Infrastructure.RequestDTOs.User;
using Api.Infrastructure.ResponseDTOs.Shared;
using Api.Infrastructure.ResponseDTOs.Users;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
    
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAll();
            var model =users.Select(u=>new UserResponseDto
            {
                Id=u.Id,
                FirstName=u.FirstName,
                LastName=u.LastName,
                Username=u.Username,
                Roles=u.Roles.Select(ur=>new IdNameDTO
                {
                    Id=ur.RoleId,
                    Name=ur.Role.RoleName
                }).ToList()
            }).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute]int id)
        {
            var user = _userService.GetById(id);
            if(user==null)
                return NotFound();
            
            var userModel = new UserResponseDto{
                Id=user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Username=user.Username,
                Roles=user.Roles.Select(r=>new IdNameDTO
                {
                    Id=r.Role.Id,
                    Name=r.Role.RoleName
                }).ToList()
                };


                return Ok(userModel);
        }

        [HttpGet("{id}/rentals")]

        public IActionResult GetRentals(int id)
        {
            var rentals =_userService.GetRentals(id);
            if (rentals == null)
            {
                return NotFound();
            }

            var dto=rentals.Select(r=>new UserRentalsDto
            {
                Id=r.Id,
                CarId=r.CarId,
                PickupLocationId=r.PickupLocationId,
                ReturnLocationId=r.ReturnLocationId,
                PickupDate=r.StartDate,
                ReturnDate=r.EndDate,
                TotalPaid=r.Payments.Sum(p=>p.Amount)
            }).ToList();


            return Ok(dto);
        }

        [HttpGet("count")]
        public IActionResult GetUsersCount()
        {
           
            var count = _userService.GetUsersCount();
            return Ok(new{count});
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserRequest dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Password = dto.Password,
                
            };

            _userService.Create(user);

            
            return Ok(user);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UserRequest dto)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User forUpdate = _userService.GetById(id);
            if (forUpdate == null)
                return NotFound();

            forUpdate.FirstName = dto.FirstName;
            forUpdate.LastName = dto.LastName;
            forUpdate.Username = dto.Username;
            forUpdate.Password=dto.Password;

            _userService.Update(forUpdate);
            return Ok(forUpdate);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
           
            User forDelete = _userService.GetById(id);
            if (forDelete == null)
                throw new Exception("User not found");

            _userService.Delete(forDelete);

            return Ok(forDelete);
        }


        [HttpPut("{id}/roles")]

        public IActionResult SetRoles(int id, [FromBody] UserRolesUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _userService.SetRoles(id,dto.RoleIds);
            }
            catch(Exception ex)
            {
                return BadRequest(new{message=ex.Message});
            }

            return Ok();
        }

    }
}
