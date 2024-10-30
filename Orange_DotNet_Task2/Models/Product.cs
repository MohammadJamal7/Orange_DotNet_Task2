using System.ComponentModel.DataAnnotations.Schema;

namespace Orange_DotNet_Task2.Models
{
	public class Product
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string imagePath { get; set; }

		public int CategoryId { get; set; }

		[ForeignKey("CategoryId")]
		public Category category { get; set; }


	}
}
