using System.ComponentModel.DataAnnotations;

namespace InformationSystems.Server.DTO.Comment
{
    public class CreateComment
    {
        [Required]
        [MinLength(5,ErrorMessage="Title must be at least 5 characters!")]
        [MaxLength(200, ErrorMessage = "Title cannot be over 200 characters!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters!")]
        [MaxLength(400, ErrorMessage = "Content cannot be over 400 characters!")]
        public string Content { get; set; } = string.Empty;
    }
}
