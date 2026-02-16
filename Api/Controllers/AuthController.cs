using System.Collections.Generic;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Auth;
using Api.Services;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateToken([FromForm] AuthTokenRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ServiceResultExtension<List<Error>>.Failure(null, ModelState)
                    );

                
             User loggedUser=_userService.GetAll()
                .FirstOrDefault(u=>u.Username==model.Username&&
                                u.Password==model.Password);

            if (loggedUser == null)
            {
                ModelState.AddModelError("Global","Invalid username or password");

                return Unauthorized(
                    ServiceResultExtension<List<Error>>.Failure(null, ModelState)
                    
                );
            }

            TokenService tokenService=new TokenService();
            string token=tokenService.CreateToken(loggedUser);
            return Ok(new
            {
                token=token
            });
        }
    }


}
