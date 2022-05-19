using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saloonAPI.Models;
using saloonAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saloonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _sqlService;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository dataAccessRepository, IMapper mapper)
        {
            _sqlService = dataAccessRepository;
            _mapper = mapper;
        }

        [HttpPost("{productId}"), Authorize]
        public ActionResult<ProductComment> CreateComment(int productId, Comment comment)
        {
            comment.CreatedDate = DateTime.Now;
            var SavedComment = _sqlService.SaveComment(comment);
            ProductComment productCommentAdded= _sqlService.SaveProductComment(new ProductComment
            {
                CommentId = SavedComment.Id,
                ProductId = productId,
                UserId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value
            });

            return Created("Comment", productCommentAdded);
        }

        [HttpPut("{commentId}"), Authorize]
        public IActionResult UpdateComment(int commentId, Comment comment)
        {
            var commentSelected = _sqlService.GetComment(commentId);

            if (commentSelected is null)
            {
                return NotFound();
            }

            commentSelected.CommentText = comment.CommentText;
            commentSelected.EditedDate = DateTime.Now;

            _sqlService.UpdateComment(commentSelected);

            return NoContent();
        }

        [HttpDelete("{commentId}"), Authorize]
        public IActionResult DeteleComment(int commentId)
        {
            var selectedComment = _sqlService.GetComment(commentId);
            var commentMapping = _sqlService.GetProductComment(commentId);
            if (selectedComment is null || commentMapping is null)
            {
                return NotFound();
            }

            _sqlService.DeleteProductComment(commentMapping);
            _sqlService.DeleteComment(selectedComment);

            return NoContent();
        }
    }
}
