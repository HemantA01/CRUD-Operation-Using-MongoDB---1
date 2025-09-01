using CrudOpsMongo.API.Models;
using CrudOpsMongo.API.ServiceModule.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CrudOpsMongo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetails _userdetails;

        public UserDetailsController(IUserDetails userdetails)
        {
                _userdetails = userdetails;
        }
        [HttpPost, Route("insert-user-details")]
        public async Task<IActionResult> InsertUserDetails(UserDetailModel model)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.InsertUserDetailsAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }

            
        }

        [HttpGet, Route("get-user-details")]
        public async Task<IActionResult> FetchUserDetails()
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.GetUserDetailsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet, Route("get-user-details-byid")]
        public async Task<IActionResult> FetchUserDetailsById(string? id)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.GetUserDetailsByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet, Route("get-user-details-byname")]
        public async Task<IActionResult> FetchUserDetailsByName(string? name)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.GetUserDetailsByNameAsync(name);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPut, Route("update-user-details")]
        public async Task<IActionResult> UpdateUserDetails(UserDetailModel model)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.UpdateUserDetailsByIdAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpPatch, Route("update-salary")]
        public async Task<IActionResult> UpdateUserSalaryById(UserDetailModel model)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.UpdateUserSalaryByIdAsync(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpDelete, Route("delete-record-byid")]
        public async Task<IActionResult> DeleteUserById(string? id)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.DeleteUserByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpDelete, Route("delete-record-bygender")]
        public async Task<IActionResult> DeleteUserByGender(string? gender)
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.DeleteUserByGenderAsync(gender);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
        [HttpDelete, Route("delete-all-record")]
        public async Task<IActionResult> DeleteUserByGender()
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                response = await _userdetails.DeleteAllUsersAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception occured: " + ex.Message;
                return BadRequest(response);
            }
        }
    }
}
