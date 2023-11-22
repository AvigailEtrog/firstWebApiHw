﻿using Service;
using Entities;
using Microsoft.AspNetCore.Mvc;

using System.Reflection.Metadata;
using System.Text.Json;
using DTO;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace firstWebApiHw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;
        public UsersController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // POST: api/<user>
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<UserIdNameDto>> Post([FromBody] UserNameAndPassword User)
        {
            try
            {
                User user = await _userService.getUserByUserNameAndPassword(User.UserName, User.Password);
                UserIdNameDto UserIdDto = _mapper.Map<User, UserIdNameDto>(user);
                return user != null ? Ok(UserIdDto) : Unauthorized();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }


        // POST api/<user>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto UserDto)
        {
            try {
            User user = _mapper.Map<UserDto,User>(UserDto);
            User newUser = await _userService.createNewUser(user);
                return newUser != null ? CreatedAtAction(nameof(Get), new { id = UserDto.UserId }, UserDto) : BadRequest();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }



        }
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {
                
                User user = await _userService.getUserById(id);
                UserDto UserDto = _mapper.Map<User, UserDto>(user);
                return user != null ? Ok(UserDto) : BadRequest("User didn't found");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT api/<user>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserIdNameDto>> Put(int id, [FromBody] UserDto userDto)
        {
            try {
                User userToUpdate = _mapper.Map<UserDto, User>(userDto);
                User updatedUser = await _userService.update(id, userToUpdate);
                UserIdNameDto userIdNameDto = _mapper.Map<User, UserIdNameDto>(updatedUser);
                return updatedUser != null ? Ok(userIdNameDto) : BadRequest();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

      

        [Route("password")]
        [HttpPost]
        public ActionResult<int> Post([FromBody] string password)
        {
            try { 
            var result = _userService.checkPassword(password);
            return result < 2?BadRequest( "Password is too weak") :Ok(result);
              }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}



