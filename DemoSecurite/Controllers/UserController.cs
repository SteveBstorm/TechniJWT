using DemoSecurite.Models;
using DemoSecurite.Tools;
using JWTDal.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSecurite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private ITokenManager _token;

        public UserController(IUserService userService, ITokenManager token)
        {
            _userService = userService;
            _token = token;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UserForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            _userService.Register(form.Email, form.Password);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            User connectedUser;
            try
            {
                connectedUser = _userService.Login(form.Email, form.Password).ToAPI();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            connectedUser = _token.GenerateJWT(connectedUser);

            return Ok(connectedUser);

        }

        [HttpGet]
        [Authorize("userPolicy")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll().Select(x => x.ToAPI()));
        }

        [HttpDelete("{id}")]
        [Authorize("adminPolicy")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
