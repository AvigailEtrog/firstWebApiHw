using Service;
using Entities;
using Microsoft.AspNetCore.Mvc;
using DTO;
using AutoMapper;
using webApiShopSite.Controllers;
using Microsoft.Extensions.Logging;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace firstWebApiHw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService,IMapper mapper, ILogger<UsersController> logger)
        {
            _logger = logger;
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
                UserIdNameDto UserIdNameDto = _mapper.Map<User, UserIdNameDto>(user);
                if (user != null)
                {
                    _logger.LogInformation($"Login attemped with userName  {user.UserName} and password {user.Password}");
                    return Ok(UserIdNameDto);
                }
                else
                    return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }  
        }


        // POST api/<user>
        [HttpPost]
        public async Task<ActionResult<UserIdNameDto>> Post([FromBody] UserDto UserDto)
        {
            try {
            User user = _mapper.Map<UserDto,User>(UserDto);
            User newUser = await _userService.createNewUser(user);
            UserIdNameDto userIdNameDto = _mapper.Map<User, UserIdNameDto>(newUser);
            return newUser != null ? CreatedAtAction(nameof(Get), new { id = userIdNameDto.UserId }, userIdNameDto) : BadRequest();
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



