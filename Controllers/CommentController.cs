using InformationSystems.Server.DTO.Comment;
using InformationSystems.Server.Interfaces;
using InformationSystems.Server.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace InformationSystems.Server.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
        }
        [HttpGet]
        [Route("/api/comment")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            var commentsDTO = comments.Select(x => x.ToCommentDTO());
            return Ok(commentsDTO);
        }
        [HttpGet]
        [Route("/api/comment/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDTO());
        }
        [HttpPost]
        [Route("/api/comment/{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateComment request) {
             if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var comment = request.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDTO());
        }
        [HttpDelete]
        [Route("/api/comment/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
