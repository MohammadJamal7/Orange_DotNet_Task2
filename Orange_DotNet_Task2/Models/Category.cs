using System.ComponentModel.DataAnnotations;

namespace Orange_DotNet_Task2.Models
{
	public class Category
	{

        public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public string imagePath { get; set; }

		public ICollection<Product> Products { get; set; }

    }
}
