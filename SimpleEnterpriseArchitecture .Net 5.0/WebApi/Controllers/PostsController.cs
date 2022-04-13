using Business.Abstract;
using Core.Extensions;
using Entities.Concrete;
using Entities.Dtos;
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
        [HttpPost("save")]
        public IActionResult Save(PostSave postSave)
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            postSave.UserId = int.Parse(claimsIdentity.Value);
            return Ok(this._postService.SavePost(postSave));
        }
        [HttpPost("un-save")]
        public IActionResult UnSave(PostSave postSave)
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            postSave.UserId = int.Parse(claimsIdentity.Value);
            return Ok(this._postService.UnSavePost(postSave));
        }
        [HttpPost("like")]
        public IActionResult Like(PostLike postLike)
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            postLike.UserId = int.Parse(claimsIdentity.Value);
            return Ok(this._postService.LikePost(postLike));
        }
        [HttpPost("un-like")]
        public IActionResult UnLike(PostLike postLike)
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            postLike.UserId = int.Parse(claimsIdentity.Value);
            return Ok(this._postService.UnLikePost(postLike));
        }
        [HttpPost("add")]
        public IActionResult Add(PostAddDto postAdd)
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            postAdd.UserId = int.Parse(claimsIdentity.Value);
            return Ok(_postService.Add(postAdd));
        }
        public IActionResult AddComment(PostComment postComment)
        {
            var claimsIdentity = (this.User.Identity as ClaimsIdentity).Claims.GetNameIdentifier();
            postComment.UserId = int.Parse(claimsIdentity.Value);
            return Ok(this._postService.AddComment(postComment));
        }
    }
}
