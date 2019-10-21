
namespace Korea.Shared.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

		public static string Crumb(int id)
		{
			switch (id)
			{
				case 1: return "Shampoon";
				case 2: return "Masks";
				case 3: return "Test1";
				case 4: return "Test2";
				case 5: return "Test3";
			}

			return null;
		}
	}
}