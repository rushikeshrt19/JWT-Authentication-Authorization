using System.ComponentModel.DataAnnotations;

namespace Custom_JWT_Token.Models
{
    public class Book
    {
        [Key]
        public string BookId { get; set; } = new Guid().ToString();

        [Required]
        public string  BookName { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
