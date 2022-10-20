using AutoMapper;
using CoreFramework.Helper;
using DataLayer.DTOs;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace SparkCoreAPI.Controllers
{
    [ApiController]
    [Route("api/role")]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [HttpGet("init")]
        public async Task<IActionResult> Init()
        {
            string message = string.Empty;

            try
            {
                var roles = await roleService.Get();
                if (roles == null) return NoContent();

                return Ok(new
                {
                    isCorrect = true,
                    data =  mapper.Map<List<RoleShortDTO>>(roles)
                });
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {
                return BadRequest(new { isCorrect = false, message = e.Message });
            }

            return Ok(new { isCorrect = false, message });
        }
    }
}
