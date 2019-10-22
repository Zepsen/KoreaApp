

namespace DB.Models
{
	public class Good
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }

		public Category Category { get; set; }
	}
}
