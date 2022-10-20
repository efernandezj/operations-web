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
    [Route("api/employee")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        [HttpGet("init")]
        public async Task<ActionResult> Init()
        {
            string message = string.Empty;

            try
            {
                return Ok(new
                {
                    isCorrect = true,
                    data = new InitDataDTO()
                    {
                        Jobs = await employeeService.GetListDropDownList<JobTitle, DropDownItemDTO>(x => x.IsActive),
                        Banks = await employeeService.GetListDropDownList<Bank, BanksDropDownItemDTO>(x => x.IsActive),
                        Sites = await employeeService.GetListDropDownList<Site, DropDownItemDTO>(x => x.IsActive),
                        Sups = await employeeService.GetListDropDownList<Employee, DropDownItemDTO>(x => x.IsActive)
                    }
                });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {
                return BadRequest(new { isCorrect = false, message = e.Message });
            }

            return Ok(new { isCorrect = false, message });
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            var user_db = await employeeService.GetEmployeeByID(id);

            if (user_db == null)
                throw new CustomException($"This employee does not exist.");

            return Ok(new { isCorrect = true, data = mapper.Map<EmployeeLongDTO>(user_db) });
        }



        [HttpGet]
        public async Task<ActionResult<List<EmployeeShortDTO>>> Get([FromHeader(Name = "name")] string name, [FromHeader(Name = "workday")] string workday)
        {
            string message;

            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(workday))
                    throw new CustomException("Invalid Search criterias.");

                var users_db = await employeeService.GetEmployees(name, workday);

                if (users_db.Count() == 0)
                    throw new CustomException($"Employeee { name ?? workday } does not exist in system!");

                return Ok(new { isCorrect = true, data = mapper.Map<List<EmployeeShortDTO>>(users_db) });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(new { isCorrect = false, message = e.Message });
            }

            return Ok(new { isCorrect = false, message = message });
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeLongDTO employeeDTO)
        {
            string message = string.Empty;

            try
            {
                // VALIDATE IF USER EXIST
                var oUser_db = await employeeService.GetEmployeeByID(employeeDTO.Workday);

                if (oUser_db != null)
                    throw new CustomException($"There is already a user with workday number {employeeDTO.Workday}!");

                var employee = mapper.Map<Employee>(employeeDTO);

                employeeService.CreateEntity(employee);
                await employeeService.SaveChangesAsync();

                return Ok(new
                {
                    isCorrect = true,
                    data = mapper.Map<EmployeeShortDTO>(employee),
                    message = "Success!",
                });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(new { isCorrect = false, message = e.Message });
            }

            return Ok(new { isCorrect = false, message });
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, EmployeeLongDTO employeeDTO)
        {
            string message = string.Empty;

            try
            {
                var employeeDB = await employeeService.GetEmployeeByID(id);

                if (employeeDB == null)
                    return NotFound();

                employeeDB = mapper.Map(employeeDTO, employeeDB);
                await employeeService.SaveChangesAsync();


                return Ok(new
                {
                    isCorrect = true,
                    data = mapper.Map<EmployeeShortDTO>(employeeDTO),
                    message = "Success!",
                });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(new { isCorrect = false, message });

        }
    }
}
