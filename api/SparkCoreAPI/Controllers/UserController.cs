using AutoMapper;
using CoreFramework.Helper;
using DataLayer.DTOs;
using DataLayer.Entities;
using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SparkCoreAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISecurityService securityService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, ISecurityService securityService, IMapper mapper)
        {
            this._userService = userService;
            this.securityService = securityService;
            this.mapper = mapper;
        }


        [HttpGet("active")]
        public async Task<ActionResult<List<EmployeeShortDTO>>> GetActive([FromHeader(Name = "name")] string name, [FromHeader(Name = "workday")] string workday)
        {
            string message;

            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(workday))
                    throw new CustomException("Invalid Search criterias.");

                var users_db = await _userService.GetActiveUsersInfo(name, workday);

                if (users_db.Count() == 0)
                    throw new CustomException($"The Employee {name ?? workday} does not exist in system as a User!");

                return Ok(new { isCorrect = true, data = mapper.Map<List<UserInfoDTO>>(users_db) });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(new { isCorrect = false, message = e.Message });
            }

            return Ok(new { isCorrect = false, Message = message });
        }


        [HttpGet]
        public async Task<ActionResult<List<EmployeeShortDTO>>> Get([FromHeader(Name = "name")] string name, [FromHeader(Name = "workday")] string workday)
        {
            string message;

            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(workday))
                    throw new CustomException("Invalid Search criterias.");

                var users_db = await _userService.GetUsersInfo(name, workday);

                if (users_db.Count() == 0)
                    throw new CustomException($"The Employee {name ?? workday} does not exist in system as a User!");


                return Ok(new { isCorrect = true, data = mapper.Map<List<UserInfoDTO>>(users_db) });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(new { isCorrect = false, message = e.Message });
            }

            return Ok(new { isCorrect = false, message = message });
        }



        [HttpPost]
        public async Task<ActionResult> Post(UserCreationDTO userDTO)
        {
            string message = string.Empty;

            try
            {
                // VALIDATE IF USER EXIST
                var oUser_db = await _userService.GetUserById(userDTO.Workday);
                if (oUser_db != null)
                    throw new CustomException($"The employee {oUser_db.Employee.FirstName} {oUser_db.Employee.LastName} already exist as a user!");


                // VALIDATE EMAIL IS AVAILABLE
                var isValidEmail = _userService.isEmailAvailable(userDTO);
                if (isValidEmail != null)
                    throw new CustomException($"This email is already in used by  {isValidEmail.Employee.FirstName} {isValidEmail.Employee.LastName}!");


                // VALIDATE USERNAME IS AVAILABLE
                var isValidUsername = _userService.isUsernameAvailable(userDTO);
                if (isValidUsername != null)
                    throw new CustomException($"This username is already in used by {isValidUsername.Employee.FirstName} {isValidUsername.Employee.LastName}!");



                // CREATE USER
                userDTO.Password = this.securityService.Hash("Spark", userDTO.Username).Hash.ToString();
                var user = mapper.Map<User>(userDTO);

                _userService.CreateEntity(user);
                await _userService.SaveChangesAsync();


                // CLEAR SENSITIVE DATA FOR CALLBACK
                oUser_db = null;
                oUser_db = await _userService.GetUserById(userDTO.Workday);
                oUser_db.Password = null;


                return Ok(new
                {   
                    IsCorrect = true,
                    message = "Success!",
                    data = mapper.Map<UserInfoDTO>(oUser_db)
                });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(new { IsCorrect = false, Message = message });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UserCreationDTO userDTO)
        {
            string message = string.Empty;

            try
            {
                // VALIDATE IF USER EXIST
                var oUser_db = await _userService.GetUserById(userDTO.Workday);

                if (oUser_db == null)
                    return NotFound();

                // VALIDATE EMAIL IS AVAILABLE
                var isValidEmail = _userService.isEmailAvailable(userDTO);
                if (isValidEmail != null)
                    throw new CustomException($"This email is already in used by  {isValidEmail.Employee.FirstName} {isValidEmail.Employee.LastName}!");


                // VALIDATE USERNAME IS AVAILABLE
                var isValidUsername = _userService.isUsernameAvailable(userDTO);
                if (isValidUsername != null)
                    throw new CustomException($"This username is already in used by {isValidUsername.Employee.FirstName} {isValidUsername.Employee.LastName}!");


                // KEEP DATA
                userDTO.Password = this.securityService.Hash("Spark", userDTO.Username).Hash.ToString();

                // UPDATE USER
                oUser_db = mapper.Map(userDTO, oUser_db);
                await _userService.SaveChangesAsync();


                return Ok(new
                {
                    IsCorrect = true,
                    message = "Success!",
                    data = mapper.Map<UserInfoDTO>(oUser_db)
                });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(new { IsCorrect = false, Message = message });
        }
    }
}
