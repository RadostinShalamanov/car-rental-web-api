using System.Collections.Generic;
using System.Linq;
using Api.Infrastructure.RequestDTOs.Auth;
using Api.Services;
using Common;
using Common.Entities;
using Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult CreateToken([FromForm] AuthTokenRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                    ServiceResultExtension<System.Collections.Generic.List<Common.Error>>.Failure(null, ModelState)
                    );

                
             User loggedUser=_userService.GetAll()
                .FirstOrDefault(u=>u.Username==model.Username&&
                                u.Password==model.Password);

            if (loggedUser == null)
            {
                ModelState.AddModelError("Global","Invalid username or password");

                return Unauthorized(
                    ServiceResultExtension<System.Collections.Generic.List<Common.Error>>.Failure(null, ModelState)
                );
            }

            string token = _tokenService.CreateToken(loggedUser);
            return Ok(new
            {
                token=token
            });
        }
    }


}
