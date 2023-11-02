using Service;
using Entities;
using Microsoft.AspNetCore.Mvc;

using System.Reflection.Metadata;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace firstWebApiHw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<user>
        [HttpGet]
        public async Task<ActionResult<User>> Get([FromQuery] string UserName, [FromQuery] string Password)
        {
            try
            {
                User user = await _userService.getUserByUserNameAndPassword(UserName, Password);
                if(user!=null)
                    return Ok(user);
                else 
                    return NoContent();//Unauthorized() , 
                //The 401 (Unauthorized) status code indicates that the request has not been applied because it lacks valid authentication credentials for the target resource.


            }
            catch (Exception ex)
            {
                //error code 400 (BadRequest) is not suitable for server exceptions; use the 500 error code for internal server Error. 
                //return Status code 500 or throw an exception. 
                return BadRequest(ex.Message);
            }
            
        }

        
        // POST api/<user>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            try { 
            User newUser = _userService.createNewUser(user);
                //newUser
                if(user!=null)
               return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
                else // The 'else' here is not necessary; you can simply return a BadRequest.
                    return BadRequest();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }



        }
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            try
            {
                //string? it is more appropriate to get a User object (change it also in the service & repository) 
                string user = await _userService.getUserById(id);
                return user;
                // if user!= null return-  Ok(user) : else return  BadRequest("User didn't found") 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //suggestion for shorter and nicer code- return user != null ? Ok(user) : BadRequest("User didn't found");


        // PUT api/<user>/5
        [HttpPut("{id}")]
        public ActionResult<int> Put(int id, [FromBody] User userToUpdate)
        {
            try { 
            var result = _userService.checkPassword(userToUpdate.Password);
             if (result < 2)
                    // It is better to return a more specific error message for a weak password,( e.g. "Password is too weak.")
                    return BadRequest(result);
            else { 
            _userService.update(id,userToUpdate);
                    //Update should return the updated user. (Returning the password result is not appropriate here..)       
                    return Ok(result);
            }}
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //Clean code -Remove unnecessary lines of code.
        // DELETE api/<user>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("password")]
        [HttpPost]
        public ActionResult<int> Post([FromBody] string password)
        {
            try { 
            var result = _userService.checkPassword(password);
            if (result < 2)
                return BadRequest(result);
            else
                return Ok(result);}
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}



