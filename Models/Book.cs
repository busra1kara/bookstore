using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayName("Kitap Adı")]
		public string BookName { get; set; }

		[DisplayName("Açıklama")]
		public string BookDefine { get; set; }

		[Required]
		[DisplayName("Yazar")]
		public string Author { get; set; }

		[Required]
		[Range(10,5000)]
		[DisplayName("Fiyat")]
		public double Price { get; set; }

		[DisplayName("Kitap Türü")]
		public int BookTypeId { get; set; }
		[ForeignKey("BookTypeId")]
        public BookType? BookType { get; set; }

		[DisplayName("Resim URL")]
        public string? ImageUrl { get; set; }
	}
}
