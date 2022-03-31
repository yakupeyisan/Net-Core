using Business.Abstract;
using Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostInformationService _postService;

        public PostsController(IPostInformationService postService)
        {
            _postService = postService;
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            return Ok(this._postService.GetAllViewPostInformations(int.Parse(claimsIdentity.Value)));
        }
    }
}
