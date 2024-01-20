using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class BookHire
    {
        [Key]
        [ValidateNever]
        public int Id { get; set; }
        [Required]
        [ValidateNever]
        public int StudentId { get; set; }
        [DisplayName("Kitaplar")]
        [ValidateNever]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book? Book { get; set; }
    }
}
