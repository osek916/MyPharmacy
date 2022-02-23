using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Models;
using MyPharmacy.Models.Queries;
using MyPharmacy.Models.UserDtos;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Controllers
{

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        //ADMIN POBIERA KAŻDEGO
        //MANAGER POBIERA TYLKO ZE SWOJEJ APTEKI FARMACEUTÓW
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult<PagedResult<UserDto>> GetAll([FromQuery] UserGetAllQuery query)
        {
            var users = _userService.GetAll(query);
            return users;
        }


        //UPDATE BY ADMIN WITH ALL PRIVILEGES
        

        //UPDATE BY MANAGER ALL FROM HIS FIRM
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult UpdatePharmacist(UpdateUserDtoWithRole dto)
        {
            _userService.UpdateWithRole();
            return Ok();
        }

        //UPDATE SELF ACCOUNT
        [HttpPut()]
        [Authorize(Roles = "Admin,Manager,Pharmacist,User")]
        public ActionResult UpdateSelfAccount(UpdateUserDto dto)
        {
            _userService.UpdateSelfAccount();
            return Ok();
        }

        

        //[HttpPost]
        //[Authorize(Roles = "Admin,Manager")]
        //public ActionResult<CreateUserDtoWithRole>
        //[HttpGet("administratorpanel")]
        //[Authorize(Roles = "Admin")]
        //public ActionResult<PagedResult<UserDto>> GetAllWithPrivilege([FromQuery] UserGetAllQuery query)
        //{
        //    var users = _userService.GetAllWithPrivilege(query);
        //    return users;
        //}

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<UserDto> GetById([FromRoute] int userId)
        {
            var users = _userService.GetById(userId);
            return users;
        }

        [HttpGet("self")]
        [Authorize(Roles = "Admin,Manager,Pharmacist,User")]
        public ActionResult<UserDto> GetSelfAccount()
        {
            var users = _userService.GetSelfAccount();
            return users;
        }

        

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteById(int id)
        {
            _userService.DeleteById(id);
            return NoContent();
        }

    }

}
