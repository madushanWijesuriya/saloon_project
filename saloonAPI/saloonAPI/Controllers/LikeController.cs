using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using saloonAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeRepository _sqlService;
        private readonly IMapper _mapper;

        public LikeController(ILikeRepository likeRepository, IMapper mapper)
        {
            _sqlService = likeRepository;
            _mapper = mapper;
        }

        [HttpPost("{productId}"), Authorize]
        public ActionResult<LikeDto> CreateLike(int productId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value;
            var alreadyHave = _sqlService.GetLikeByProductAndUser(productId, userId);
            LikeDto vmLike;

            if (alreadyHave is not null)
            {
                vmLike = _mapper.Map<LikeDto>(alreadyHave);
                return Created("", vmLike);
            }

            Like like = new();
            like.UserId = userId;
            like.ProductId = productId;
            like.CreatedDate = DateTime.Now;
            var SavedLike = _sqlService.SaveLike(like);

            vmLike = _mapper.Map<LikeDto>(SavedLike);

            return Created("", vmLike);
        }

        [HttpDelete("{likeId}"), Authorize]
        public IActionResult DeleteLike(int likeId)
        {
            var selectedLike = _sqlService.GetLike(likeId);

            if (selectedLike is null)
            {
                return NotFound();
            }

            _sqlService.DeleteLike(selectedLike);

            return NoContent();
        }
    }
}
